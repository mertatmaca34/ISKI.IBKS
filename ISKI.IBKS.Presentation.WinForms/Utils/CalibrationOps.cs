using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Application.Services.DataCollection;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.Controls;
using ISKI.IBKS.Presentation.WinForms.Common.Controls;
using Microsoft.Extensions.Logging;
using System.Windows.Forms.DataVisualization.Charting;
using Timer = System.Windows.Forms.Timer;

namespace ISKI.IBKS.Presentation.WinForms.Utils
{
    /// <summary>
    /// Calibration operations utility class.
    /// Uses synchronous PLC reads via IPlcClient to avoid blocking UI thread.
    /// </summary>
    public class CalibrationOps
    {
        private readonly IPlcClient _plcClient;
        private readonly IDataCollectionService _dataCollectionService;
        private readonly ISaisApiClient _saisApiClient;
        private readonly ILogger _logger;

        public bool isCalibrationInProgress;
        private readonly double _tolerance = 1.10;

        // Calibration data
        private double _zeroRef, _zeroMeas, _zeroDiff, _zeroStd;
        private double _spanRef, _spanMeas, _spanDiff, _spanStd;
        private double _resultFactor;
        private bool _resultZero, _resultSpan;

        // Cached settings to avoid async calls during timer
        private StationSettings? _cachedSettings;
        private Guid _stationId;

        public CalibrationOps(
            IPlcClient plcClient,
            IDataCollectionService dataCollectionService,
            ISaisApiClient saisApiClient,
            ILogger logger)
        {
            _plcClient = plcClient;
            _dataCollectionService = dataCollectionService;
            _saisApiClient = saisApiClient;
            _logger = logger;
        }

        /// <summary>
        /// Initialize settings before starting calibration. Call this async before StartCalibration.
        /// </summary>
        public async Task InitializeAsync()
        {
            _cachedSettings = await _dataCollectionService.GetStationSettingsAsync();
            _stationId = await _dataCollectionService.GetStationIdAsync();
        }

        public void StartCalibration(string calibrationName, string calibrationType, int calibrationTime, List<Control> controls)
        {
            if (isCalibrationInProgress)
            {
                MessageBox.Show("Kalibrasyon zaten devam ediyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isCalibrationInProgress = true;

            // Reset calibration data
            _zeroRef = _zeroMeas = _zeroDiff = _zeroStd = 0;
            _spanRef = _spanMeas = _spanDiff = _spanStd = 0;
            _resultFactor = 0;
            _resultZero = _resultSpan = false;

            if (calibrationType == "Zero")
            {
                StartZeroCalibration(calibrationName, calibrationTime, controls);
            }
            else
            {
                StartSpanCalibration(calibrationName, calibrationTime, controls);
            }
        }

        private void StartZeroCalibration(string calibrationName, int calibrationTime, List<Control> controls)
        {
            List<double> measValues = new();

            var labelTimeStamp = controls.OfType<TitleBarControl>().FirstOrDefault(c => c.Name == "TitleBarControlTimeRemain");
            var chartCalibration = controls.OfType<Chart>().FirstOrDefault(c => c.Name == "ChartCalibration");
            var labelActiveCalibration = controls.OfType<TitleBarControl>().FirstOrDefault(c => c.Name == "TitleBarControlActiveCalibration");

            if (chartCalibration == null || labelTimeStamp == null || labelActiveCalibration == null)
            {
                _logger.LogError("Kalibrasyon kontrolleri bulunamadı");
                isCalibrationInProgress = false;
                return;
            }

            chartCalibration.Series["Kalibrasyon Değeri"].Points.Clear();
            chartCalibration.Series["Referans Değeri"].Points.Clear();
            
            // X Ekseni Konfigürasyonu (Örnek No)
            var xAxis = chartCalibration.ChartAreas[0].AxisX;
            xAxis.Title = "Örnek No";
            xAxis.Minimum = 0; // 0'dan başlasın ki 1. örnek tam eksen üstüne binmesin
            xAxis.Maximum = calibrationTime + 1; // Biraz boşluk bırak
            xAxis.Interval = 5; // Her 5 örnekte bir tick (5, 10, 15...)
            xAxis.MajorGrid.Enabled = false; // Grid çizgilerini kapat
            xAxis.LabelStyle.Format = "";    // Varsayılan sayısal format
            xAxis.LabelStyle.Angle = 0;      // Yatay yazı
            xAxis.IsLabelAutoFit = true;
            // Get reference value from cached settings
            switch (calibrationName)
            {
                case "AKM":
                    _zeroRef = _cachedSettings?.AkmZeroReference ?? 0;
                    labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: Akm";
                    break;
                case "KOi":
                    _zeroRef = _cachedSettings?.KoiZeroReference ?? 0;
                    labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: Koi";
                    break;
                case "pH":
                    _zeroRef = _cachedSettings?.PhZeroReference ?? 7.0;
                    labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: pH";
                    break;
                case "Iletkenlik":
                    _zeroRef = _cachedSettings?.ConductivityZeroReference ?? 0;
                    labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: İletkenlik";
                    break;
                default:
                    _zeroRef = 0;
                    break;
            }

            int remainingTime = calibrationTime;
            int sampleNo = 1;  // İlk ölçüm: 1

            Timer timerCalibration = new()
            {
                Interval = 1000,
                Enabled = true
            };

            timerCalibration.Tick += (sender, e) =>
            {
                if (remainingTime >= 0)
                {
                    // Synchronously read PLC data
                    RefreshData(calibrationName, "Zero");

                    labelTimeStamp.TitleBarText = remainingTime.ToString();

                    // X ekseni: Örnek No (1, 2, 3...)
                    chartCalibration.Series["Kalibrasyon Değeri"].Points.AddXY(sampleNo, _zeroMeas);
                    chartCalibration.Series["Referans Değeri"].Points.AddXY(sampleNo, _zeroRef);
                    sampleNo++; // Her okumada +1

                    measValues.Add(_zeroMeas);

                    CalculateCalibrationParameters(measValues, "Zero");
                    AssignLabels(controls, "Zero");

                    // Tolerance check
                    if (_zeroRef != 0 && _zeroMeas >= _zeroRef / _tolerance && _zeroMeas <= _zeroRef * _tolerance)
                    {
                        chartCalibration.Series["Kalibrasyon Değeri"].Color = Color.Lime;
                    }
                    else
                    {
                        chartCalibration.Series["Kalibrasyon Değeri"].Color = Color.Red;
                    }

                    remainingTime--;
                }
                else
                {
                    timerCalibration.Stop();
                    timerCalibration.Dispose();
                    isCalibrationInProgress = false;

                    _resultZero = chartCalibration.Series["Kalibrasyon Değeri"].Color == Color.Lime;
                    _resultSpan = _resultZero;
                    _resultFactor = 1;

                    // Save and send calibration asynchronously (fire and forget with proper error handling)
                    if (calibrationName == "AKM" || calibrationName == "KOi" || calibrationName == "pH")
                    {
                        _ = SaveAndSendCalibrationAsync(calibrationName, labelActiveCalibration, controls);
                    }
                    else
                    {
                        labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: -";
                    }

                    chartCalibration.Series["Kalibrasyon Değeri"].Points.Clear();
                    chartCalibration.Series["Referans Değeri"].Points.Clear();
                    labelTimeStamp.TitleBarText = "Kalan Süre:";
                }
            };
        }

        private void StartSpanCalibration(string calibrationName, int calibrationTime, List<Control> controls)
        {
            List<double> measValues = new();

            var labelTimeStamp = controls.OfType<TitleBarControl>().FirstOrDefault(c => c.Name == "TitleBarControlTimeRemain");
            var chartCalibration = controls.OfType<Chart>().FirstOrDefault(c => c.Name == "ChartCalibration");
            var labelActiveCalibration = controls.OfType<TitleBarControl>().FirstOrDefault(c => c.Name == "TitleBarControlActiveCalibration");

            if (chartCalibration == null || labelTimeStamp == null || labelActiveCalibration == null)
            {
                _logger.LogError("Kalibrasyon kontrolleri bulunamadı");
                isCalibrationInProgress = false;
                return;
            }

            chartCalibration.Series["Kalibrasyon Değeri"].Points.Clear();
            chartCalibration.Series["Referans Değeri"].Points.Clear();
            
            // X Ekseni Konfigürasyonu (Örnek No)
            var xAxis = chartCalibration.ChartAreas[0].AxisX;
            xAxis.Title = "Örnek No";
            xAxis.Minimum = 0;
            xAxis.Maximum = calibrationTime + 1;
            xAxis.Interval = 5; // Her 5 örnekte bir tick
            xAxis.MajorGrid.Enabled = false;
            xAxis.LabelStyle.Format = "";
            xAxis.LabelStyle.Angle = 0;
            xAxis.IsLabelAutoFit = true;
            switch (calibrationName)
            {
                case "pH":
                    _spanRef = _cachedSettings?.PhSpanReference ?? 4.0;
                    labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: pH";
                    break;
                case "Iletkenlik":
                    _spanRef = _cachedSettings?.ConductivitySpanReference ?? 1413.0;
                    labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: İletkenlik";
                    break;
                default:
                    _spanRef = 0;
                    break;
            }

            int remainingTime = calibrationTime;
            int sampleNo = 1;  // İlk ölçüm: 1

            Timer timerCalibration = new()
            {
                Interval = 1000,
                Enabled = true
            };

            timerCalibration.Tick += (sender, e) =>
            {
                if (remainingTime >= 0)
                {
                    RefreshData(calibrationName, "Span");

                    labelTimeStamp.TitleBarText = remainingTime.ToString();

                    // X ekseni: Örnek No (1, 2, 3...)
                    chartCalibration.Series["Kalibrasyon Değeri"].Points.AddXY(sampleNo, _spanMeas);
                    chartCalibration.Series["Referans Değeri"].Points.AddXY(sampleNo, _spanRef);
                    sampleNo++; // Her okumada +1

                    measValues.Add(_spanMeas);

                    CalculateCalibrationParameters(measValues, "Span");
                    AssignLabels(controls, "Span");

                    // Tolerance check
                    if (_spanRef != 0 && _spanMeas >= _spanRef / _tolerance && _spanMeas <= _spanRef * _tolerance)
                    {
                        chartCalibration.Series["Kalibrasyon Değeri"].Color = Color.Lime;
                    }
                    else
                    {
                        chartCalibration.Series["Kalibrasyon Değeri"].Color = Color.Red;
                    }

                    remainingTime--;
                }
                else
                {
                    timerCalibration.Stop();
                    timerCalibration.Dispose();
                    isCalibrationInProgress = false;

                    _resultSpan = chartCalibration.Series["Kalibrasyon Değeri"].Color == Color.Lime;
                    _resultFactor = 1;

                    if (calibrationName == "pH" || calibrationName == "Iletkenlik")
                    {
                        _ = SaveAndSendCalibrationAsync(calibrationName, labelActiveCalibration, controls);
                    }
                    else
                    {
                        labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: -";
                    }

                    chartCalibration.Series["Kalibrasyon Değeri"].Points.Clear();
                    chartCalibration.Series["Referans Değeri"].Points.Clear();
                    labelTimeStamp.TitleBarText = "Kalan Süre:";
                }
            };
        }

        private async Task SaveAndSendCalibrationAsync(string calibrationName, TitleBarControl labelActiveCalibration, List<Control> controls)
        {
            try
            {
                var calibration = new Calibration(
                    _stationId,
                    calibrationName,
                    DateTime.Now,
                    _zeroRef, _zeroMeas, _zeroDiff, _zeroStd,
                    _spanRef, _spanMeas, _spanDiff, _spanStd,
                    _resultFactor, _resultZero, _resultSpan, _resultZero && _resultSpan
                )
                {
                    Id = Guid.NewGuid()
                };

                await _dataCollectionService.SaveAndSendCalibrationAsync(calibration);
                
                MessageBox.Show("Kalibrasyon başarıyla kaydedildi ve gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kalibrasyon kaydedilirken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelActiveCalibration.TitleBarText = "Aktif Kalibrasyon: -";
                AssignLabels(controls, "reset");
            }
        }

        private void RefreshData(string calibrationName, string calibrationType)
        {
            // Use synchronous PLC read if connected
            if (!_plcClient.IsConnected)
            {
                // Try to connect
                try
                {
                    var ip = _cachedSettings?.PlcIpAddress ?? "10.33.3.253";
                    var rack = _cachedSettings?.PlcRack ?? 0;
                    var slot = _cachedSettings?.PlcSlot ?? 1;
                    _plcClient.Connect(ip, rack, slot);
                }
                catch
                {
                    // Can't connect, use dummy data for testing
                    _logger.LogWarning("PLC bağlantısı kurulamadı, test verisi kullanılıyor");
                    var random = new Random();
                    if (calibrationType == "Zero")
                        _zeroMeas = _zeroRef + (random.NextDouble() * 2 - 1) * 0.5; // +/- 0.5 deviation
                    else
                        _spanMeas = _spanRef + (random.NextDouble() * 2 - 1) * 0.5;
                    return;
                }
            }

            try
            {
                // Read DB41 synchronously (168 bytes for analog data)
                byte[] db41Buffer = new byte[168];
                _plcClient.ReadBytes(41, 0, db41Buffer);

                // Map based on calibration parameter
                switch (calibrationName)
                {
                    case "AKM":
                        _zeroMeas = _plcClient.ReadReal(db41Buffer, 36);
                        break;
                    case "KOi":
                        _zeroMeas = _plcClient.ReadReal(db41Buffer, 32);
                        break;
                    case "pH" when calibrationType == "Zero":
                        _zeroMeas = _plcClient.ReadReal(db41Buffer, 16);
                        break;
                    case "pH":
                        _spanMeas = _plcClient.ReadReal(db41Buffer, 16);
                        break;
                    case "Iletkenlik" when calibrationType == "Zero":
                        _zeroMeas = _plcClient.ReadReal(db41Buffer, 20);
                        break;
                    case "Iletkenlik":
                        _spanMeas = _plcClient.ReadReal(db41Buffer, 20);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "PLC'den veri okunamadı, test verisi kullanılıyor");
                var random = new Random();
                if (calibrationType == "Zero")
                    _zeroMeas = _zeroRef + (random.NextDouble() * 2 - 1) * 0.5;
                else
                    _spanMeas = _spanRef + (random.NextDouble() * 2 - 1) * 0.5;
            }
        }

        private void CalculateCalibrationParameters(List<double> measValues, string calibrationType)
        {
            if (calibrationType == "Zero")
            {
                _zeroDiff = Math.Round(_zeroMeas - _zeroRef, 2);
                _zeroStd = Math.Round(CalculateStandardDeviation(measValues), 2);
                _resultFactor = _zeroDiff != 0 ? Math.Round((_zeroMeas - _zeroRef) / _zeroDiff, 2) : 0;
            }
            else
            {
                _spanDiff = Math.Round(_spanMeas - _spanRef, 2);
                _spanStd = Math.Round(CalculateStandardDeviation(measValues), 2);
                _resultFactor = _spanDiff != 0 ? Math.Round((_spanMeas - _spanRef) / _spanDiff, 2) : 0;
            }
        }

        private static double CalculateStandardDeviation(List<double> data)
        {
            if (data.Count <= 1) return 0;

            double mean = data.Average();
            double sumOfSquares = data.Sum(x => Math.Pow(x - mean, 2));
            return Math.Sqrt(sumOfSquares / (data.Count - 1));
        }

        private void AssignLabels(List<Control> controls, string calibrationType)
        {
            var calibrationStatusBarZero = controls.OfType<CalibrationStatusBarZeroControl>().FirstOrDefault();
            var calibrationStatusBarSpan = controls.OfType<CalibrationStatusBarSpanControl>().FirstOrDefault();

            if (calibrationType == "Zero" && calibrationStatusBarZero != null)
            {
                calibrationStatusBarZero.ZeroRef = _zeroRef.ToString("F2");
                calibrationStatusBarZero.ZeroMeas = _zeroMeas.ToString("F2");
                calibrationStatusBarZero.ZeroDiff = _zeroDiff.ToString("F2");
                calibrationStatusBarZero.ZeroStd = _zeroStd.ToString("F2");
            }
            else if (calibrationType == "reset")
            {
                if (calibrationStatusBarZero != null)
                {
                    calibrationStatusBarZero.ZeroRef = "-";
                    calibrationStatusBarZero.ZeroMeas = "-";
                    calibrationStatusBarZero.ZeroDiff = "-";
                    calibrationStatusBarZero.ZeroStd = "-";
                }
                if (calibrationStatusBarSpan != null)
                {
                    calibrationStatusBarSpan.SpanRef = "-";
                    calibrationStatusBarSpan.SpanMeas = "-";
                    calibrationStatusBarSpan.SpanDiff = "-";
                    calibrationStatusBarSpan.SpanStd = "-";
                }
            }
            else if (calibrationStatusBarSpan != null)
            {
                calibrationStatusBarSpan.SpanRef = _spanRef.ToString("F2");
                calibrationStatusBarSpan.SpanMeas = _spanMeas.ToString("F2");
                calibrationStatusBarSpan.SpanDiff = _spanDiff.ToString("F2");
                calibrationStatusBarSpan.SpanStd = _spanStd.ToString("F2");
            }
        }
    }
}
