using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
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
    /// Uses IStationSnapshotCache for real-time PLC data reading.
    /// </summary>
    public class CalibrationOps
    {
        private readonly IPlcClient _plcClient;
        private readonly IDataCollectionService _dataCollectionService;
        private readonly ISaisApiClient _saisApiClient;
        private readonly IStationSnapshotCache _snapshotCache;
        private readonly ILogger _logger;

        public bool isCalibrationInProgress;
        private readonly double _tolerance = 1.10;

        // Calibration data
        private double _zeroRef, _zeroMeas, _zeroDiff, _zeroStd;
        private double _spanRef, _spanMeas, _spanDiff, _spanStd;
        private double _resultFactor;
        private bool _resultZero, _resultSpan;
        private string _currentCalibrationType = string.Empty; // "Zero" or "Span"

        // Cached settings to avoid async calls during timer
        private StationSettings? _cachedSettings;
        private Guid _stationId;
        
        // Cached snapshot for real-time calibration data (updated during InitializeAsync)
        private ISKI.IBKS.Application.Features.StationSnapshots.Dtos.StationSnapshotDto? _cachedSnapshot;

        public CalibrationOps(
            IPlcClient plcClient,
            IDataCollectionService dataCollectionService,
            ISaisApiClient saisApiClient,
            IStationSnapshotCache snapshotCache,
            ILogger logger)
        {
            _plcClient = plcClient;
            _dataCollectionService = dataCollectionService;
            _saisApiClient = saisApiClient;
            _snapshotCache = snapshotCache;
            _logger = logger;
        }

        /// <summary>
        /// Initialize settings before starting calibration. Call this async before StartCalibration.
        /// Also caches the current snapshot from StationSnapshotCache.
        /// </summary>
        public async Task InitializeAsync()
        {
            _cachedSettings = await _dataCollectionService.GetStationSettingsAsync();
            _stationId = await _dataCollectionService.GetStationIdAsync();
            
            // Cache the current snapshot for calibration
            _cachedSnapshot = await _snapshotCache.Get(_stationId);
        }
        
        /// <summary>
        /// Checks if data is available to start calibration.
        /// Returns error message if not ready, null if ready.
        /// </summary>
        public string? CheckDataAvailability()
        {
            if (_stationId == Guid.Empty)
            {
                return "İstasyon ayarları yüklenemedi. Lütfen ayarları kontrol edin.";
            }
            
            // Check if we have cached snapshot OR PLC connection
            if (_cachedSnapshot == null && !_plcClient.IsConnected)
            {
                return "PLC bağlantısı yok ve önbellekte veri bulunamadı.\n\nKalibrasyon için aktif bir PLC bağlantısı veya önbellekte veri gereklidir.";
            }
            
            return null; // Ready to start
        }

        public void StartCalibration(string calibrationName, string calibrationType, int calibrationTime, List<Control> controls)
        {
            if (isCalibrationInProgress)
            {
                MessageBox.Show("Kalibrasyon zaten devam ediyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Check if data is available before starting
            var dataError = CheckDataAvailability();
            if (dataError != null)
            {
                MessageBox.Show(dataError, "Kalibrasyon Başlatılamadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Store the calibration type for saving
            _currentCalibrationType = calibrationType;

            // Check calibration order for pH and Iletkenlik (2-point calibration)
            if (calibrationName == "pH" || calibrationName == "Iletkenlik")
            {
                double zeroRef = 0, spanRef = 0;
                
                if (calibrationName == "pH")
                {
                    zeroRef = _cachedSettings?.PhZeroReference ?? 7.0;
                    spanRef = _cachedSettings?.PhSpanReference ?? 4.0;
                }
                else // Iletkenlik
                {
                    zeroRef = _cachedSettings?.ConductivityZeroReference ?? 0;
                    spanRef = _cachedSettings?.ConductivitySpanReference ?? 1413.0;
                }

                // Warn if user is starting with the higher reference value
                if (calibrationType == "Zero" && spanRef < zeroRef)
                {
                    var result = MessageBox.Show(
                        $"Span referans değeri ({spanRef:F2}) Zero referans değerinden ({zeroRef:F2}) daha küçük.\n" +
                        "Önce Span kalibrasyonu yapmanız önerilir.\n\n" +
                        "Yine de Zero kalibrasyonuna devam etmek istiyor musunuz?",
                        "Kalibrasyon Sırası Uyarısı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.No)
                        return;
                }
                else if (calibrationType == "Span" && zeroRef < spanRef)
                {
                    var result = MessageBox.Show(
                        $"Zero referans değeri ({zeroRef:F2}) Span referans değerinden ({spanRef:F2}) daha küçük.\n" +
                        "Önce Zero kalibrasyonu yapmanız önerilir.\n\n" +
                        "Yine de Span kalibrasyonuna devam etmek istiyor musunuz?",
                        "Kalibrasyon Sırası Uyarısı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.No)
                        return;
                }
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
                // Prepare calibration data based on which point was calibrated
                double zeroRef, zeroMeas, zeroDiff, zeroStd;
                double spanRef, spanMeas, spanDiff, spanStd;
                bool resultZero, resultSpan;

                if (_currentCalibrationType == "Zero")
                {
                    // Zero calibration was performed - use actual Zero values, default Span values
                    zeroRef = _zeroRef;
                    zeroMeas = _zeroMeas;
                    zeroDiff = _zeroDiff;
                    zeroStd = _zeroStd;
                    resultZero = _resultZero;

                    // Default values for Span (not performed)
                    spanRef = 0;
                    spanMeas = 0;
                    spanDiff = 0;
                    spanStd = 0;
                    resultSpan = false;
                }
                else // Span calibration
                {
                    // Default values for Zero (not performed)
                    zeroRef = 0;
                    zeroMeas = 0;
                    zeroDiff = 0;
                    zeroStd = 0;
                    resultZero = false;

                    // Span calibration was performed - use actual Span values
                    spanRef = _spanRef;
                    spanMeas = _spanMeas;
                    spanDiff = _spanDiff;
                    spanStd = _spanStd;
                    resultSpan = _resultSpan;
                }

                // Create calibration with the appropriate values
                var calibration = new Calibration(
                    _stationId,
                    calibrationName,
                    DateTime.Now,
                    zeroRef, zeroMeas, zeroDiff, zeroStd,
                    spanRef, spanMeas, spanDiff, spanStd,
                    _resultFactor, resultZero, resultSpan, resultZero || resultSpan
                )
                {
                    Id = Guid.NewGuid()
                };

                await _dataCollectionService.SaveAndSendCalibrationAsync(calibration);
                
                string calibrationType = _currentCalibrationType == "Zero" ? "Zero" : "Span";
                MessageBox.Show($"{calibrationName} {calibrationType} kalibrasyonu başarıyla kaydedildi ve gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// Refreshes the cached snapshot from StationSnapshotCache.
        /// Call this at the start of each timer tick to get updated values.
        /// </summary>
        private void RefreshCachedSnapshot()
        {
            // Use Task.Run to avoid blocking and get the snapshot
            try
            {
                var task = _snapshotCache.Get(_stationId);
                if (task.IsCompletedSuccessfully)
                {
                    _cachedSnapshot = task.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Snapshot cache'den okunamadı");
            }
        }

        private void RefreshData(string calibrationName, string calibrationType)
        {
            // Refresh the cached snapshot (non-blocking since Task should be complete)
            RefreshCachedSnapshot();
            
            // Use the cached snapshot (already loaded, no blocking)
            if (_cachedSnapshot != null)
            {
                // Read from cached snapshot
                switch (calibrationName)
                {
                    case "AKM":
                        _zeroMeas = _cachedSnapshot.Akm ?? 0;
                        break;
                    case "KOi":
                        _zeroMeas = _cachedSnapshot.Koi ?? 0;
                        break;
                    case "pH" when calibrationType == "Zero":
                        _zeroMeas = _cachedSnapshot.Ph ?? 0;
                        break;
                    case "pH":
                        _spanMeas = _cachedSnapshot.Ph ?? 0;
                        break;
                    case "Iletkenlik" when calibrationType == "Zero":
                        _zeroMeas = _cachedSnapshot.Iletkenlik ?? 0;
                        break;
                    case "Iletkenlik":
                        _spanMeas = _cachedSnapshot.Iletkenlik ?? 0;
                        break;
                }
                _logger.LogDebug("Kalibrasyon verisi cache'den okundu: {CalibrationName} {CalibrationType}", calibrationName, calibrationType);
                return;
            }

            // Fallback: Use synchronous PLC read if cache is empty
            _logger.LogWarning("StationSnapshotCache boş, doğrudan PLC'den okuma deneniyor");
            
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
                // Calculate diff as percentage: (Meas - Ref) / Ref * 100
                _zeroDiff = _zeroRef != 0 ? Math.Round((_zeroMeas - _zeroRef) / _zeroRef * 100, 2) : 0;
                _zeroStd = Math.Round(CalculateStandardDeviation(measValues), 2);
                // ResultFactor: ratio of measured to reference value
                _resultFactor = _zeroRef != 0 ? Math.Round(_zeroMeas / _zeroRef, 4) : 0;
            }
            else
            {
                // Calculate diff as percentage: (Meas - Ref) / Ref * 100
                _spanDiff = _spanRef != 0 ? Math.Round((_spanMeas - _spanRef) / _spanRef * 100, 2) : 0;
                _spanStd = Math.Round(CalculateStandardDeviation(measValues), 2);
                // ResultFactor: ratio of measured to reference value
                _resultFactor = _spanRef != 0 ? Math.Round(_spanMeas / _spanRef, 4) : 0;
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
