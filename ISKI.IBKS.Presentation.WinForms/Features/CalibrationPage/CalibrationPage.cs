using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Application.Features.Plc.Abstractions;

namespace ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage
{
    public partial class CalibrationPage : UserControl
    {
        private enum CalibrationState
        {
            Idle,
            ZeroRunning,
            WaitingForSpan,
            SpanRunning
        }

        private readonly ISKI.IBKS.Application.Services.DataCollection.IDataCollectionService _dataCollectionService;
        private System.Windows.Forms.Timer _calibrationTimer;
        
        private CalibrationState _currentState = CalibrationState.Idle;
        private string _currentSensor = "";
        private int _remainingSeconds;
        
        // Results for Zero step
        private List<double> _zeroValues = new();
        private double _zeroRef;
        private double _zeroFormattedAvg;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IPlcClient _plcClient;
        private readonly Microsoft.Extensions.Logging.ILogger<CalibrationPage> _logger;
        private double _zeroFormattedStd;
        private double _zeroFormattedDiff;
        private bool _zeroSuccess;
        
        // Results for Span step
        private List<double> _spanValues = new();
        private double _spanRef;
          
        public CalibrationPage(ISKI.IBKS.Application.Services.DataCollection.IDataCollectionService dataCollectionService, IServiceScopeFactory scopeFactory, IPlcClient plcClient, Microsoft.Extensions.Logging.ILogger<CalibrationPage> logger)
        {
            _dataCollectionService = dataCollectionService;
            _scopeFactory = scopeFactory;
            _plcClient = plcClient;
            _logger = logger;
            InitializeComponent();
            InitializeCalibrationTimer();
            InitializeChart();
            AttachButtonEvents();
        }

        private void InitializeCalibrationTimer()
        {
            _calibrationTimer = new System.Windows.Forms.Timer();
            _calibrationTimer.Interval = 1000;
            _calibrationTimer.Tick += CalibrationTimer_Tick;
        }

        private void InitializeChart()
        {
            if (ChartCalibration != null)
            {
                ChartCalibration.Series.Clear();
                
                var series = new Series("Ölçüm Değeri");
                series.ChartType = SeriesChartType.Line;
                series.Color = Color.FromArgb(0, 131, 200);
                series.BorderWidth = 2;
                ChartCalibration.Series.Add(series);

                var refSeries = new Series("Referans Değer");
                refSeries.ChartType = SeriesChartType.Line;
                refSeries.Color = Color.FromArgb(200, 50, 50);
                refSeries.BorderWidth = 2;
                refSeries.BorderDashStyle = ChartDashStyle.Dash;
                ChartCalibration.Series.Add(refSeries);
            }
        }

        private void AttachButtonEvents()
        {
            // pH buttons
            if (ButtonPhZero != null) ButtonPhZero.Click += (s, e) => StartZeroCalibration("pH");
            if (ButtonPhSpan != null) ButtonPhSpan.Click += (s, e) => StartSpanCalibration("pH");

            // Conductivity buttons
            if (ButtonIletkenlikZero != null) ButtonIletkenlikZero.Click += (s, e) => StartZeroCalibration("İletkenlik");
            if (ButtonIletkenlikSpan != null) ButtonIletkenlikSpan.Click += (s, e) => StartSpanCalibration("İletkenlik");

            // AKM / KOI (Usually defined as single step or similar flow, applying same logic if they have buttons)
            if (ButtonAkmZero != null) ButtonAkmZero.Click += (s, e) => StartZeroCalibration("AKM");
            if (ButtonKoiZero != null) ButtonKoiZero.Click += (s, e) => StartZeroCalibration("KOI");
        }

        private double GetDefaultReference(string sensor, string step)
        {
            if (sensor == "pH") return step == "Zero" ? 7.0 : 4.0;
            if (sensor == "İletkenlik") return step == "Zero" ? 0.0 : 1413.0; // uS/cm
            return 0.0;
        }

        private async Task<double> GetReferenceFromSettings(string sensor, string step)
        {
            try 
            {
                 var settings = await _dataCollectionService.GetStationSettingsAsync();
                 if (settings == null) return GetDefaultReference(sensor, step);

                 if (sensor == "pH") 
                    return step == "Zero" ? settings.PhZeroReference : settings.PhSpanReference;
                 if (sensor == "İletkenlik") 
                    return step == "Zero" ? settings.ConductivityZeroReference : settings.ConductivitySpanReference;
                 
                 return 0.0;
            }
            catch 
            {
                return GetDefaultReference(sensor, step);
            }
        }

        private async void StartZeroCalibration(string sensor)
        {
            if (_currentState != CalibrationState.Idle)
            {
                if (_currentState == CalibrationState.WaitingForSpan && _currentSensor == sensor)
                {
                    // User clicked Zero again instead of Span? Allow restart?
                    // For safety, warn user.
                     var restart = MessageBox.Show(
                        "Şu an Span kalibrasyonu bekleniyor. Sıfırdan başlamak ister misiniz?",
                        "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                     if (restart != DialogResult.Yes) return;
                }
                else
                {
                    MessageBox.Show("Başka bir kalibrasyon işlemi devam ediyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            _logger.LogInformation("Zero kalibrasyonu başlatıldı: Sensör={Sensor}", sensor);
            _currentSensor = sensor;
            _zeroRef = await GetReferenceFromSettings(sensor, "Zero");

            var result = MessageBox.Show(
                $"{sensor} sensörü için ZERO kalibrasyonu başlatılsın mı?\n\nReferans değer: {_zeroRef}",
                "Kalibrasyon Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                _logger.LogInformation("Zero kalibrasyonu kullanıcı tarafından iptal edildi: Sensör={Sensor}", sensor);
                return;
            }

            // Reset State
            _currentState = CalibrationState.ZeroRunning;
            _zeroValues.Clear();
            _spanValues.Clear();
            _remainingSeconds = 60;

            UpdateUIForRunningState($"{sensor} - ZERO Kalibrasyonu");
            _calibrationTimer.Start();
            _logger.LogInformation("Zero kalibrasyon sayacı başlatıldı: Sensör={Sensor}, Referans={ZeroRef}", sensor, _zeroRef);
        }

        private async void StartSpanCalibration(string sensor)
        {
            if (_currentState != CalibrationState.WaitingForSpan)
            {
                // Just running Span without Zero?
                // Spec implies Zero completes -> Wait -> Span.
                // If user clicks Span first, maybe we can allow standalone Span?
                // For now, let's strictly follow flow OR allow standalone but warn.
                // Assuming standalone is allowed for flexibility but usually follows Zero.
                
                 var r = MessageBox.Show(
                    "Normal akışta önce Zero kalibrasyonu yapılması önerilir. Sadece SPAN kalibrasyonu yapmak istiyor musunuz?", 
                    "Süreç Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (r != DialogResult.Yes)
                 {
                    _logger.LogInformation("Span kalibrasyonu kullanıcı tarafından iptal edildi (Zero bekleniyordu): Sensör={Sensor}", sensor);
                    return;
                 }

                 _currentState = CalibrationState.Idle; // Reset to allow span only
            }
            else if (_currentSensor != sensor)
            {
                MessageBox.Show($"Lütfen {_currentSensor} için Span kalibrasyonunu tamamlayın veya iptal edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogWarning("Span kalibrasyonu başlatılamadı: Yanlış sensör seçimi. Beklenen: {ExpectedSensor}, Seçilen: {SelectedSensor}", _currentSensor, sensor);
                return;
            }

            _logger.LogInformation("Span kalibrasyonu başlatıldı: Sensör={Sensor}", sensor);
            _currentSensor = sensor;
            _spanRef = await GetReferenceFromSettings(sensor, "Span");

            var result = MessageBox.Show(
                $"{sensor} sensörü için SPAN kalibrasyonu başlatılsın mı?\n\nReferans değer: {_spanRef}",
                "Kalibrasyon Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                _logger.LogInformation("Span kalibrasyonu kullanıcı tarafından iptal edildi: Sensör={Sensor}", sensor);
                return;
            }

            _currentState = CalibrationState.SpanRunning;
            _spanValues.Clear();
            _remainingSeconds = 60;

            UpdateUIForRunningState($"{sensor} - SPAN Kalibrasyonu");
            _calibrationTimer.Start();
            _logger.LogInformation("Span kalibrasyon sayacı başlatıldı: Sensör={Sensor}, Referans={SpanRef}", sensor, _spanRef);
        }

        private void UpdateUIForRunningState(string title)
        {
            if (TitleBarControlActiveCalibration != null)
                TitleBarControlActiveCalibration.TitleBarText = title;

            // Clear chart for new run
            if (ChartCalibration != null)
            {
                ChartCalibration.Series["Ölçüm Değeri"].Points.Clear();
                ChartCalibration.Series["Referans Değer"].Points.Clear();
            }
            _logger.LogDebug("UI güncellendi: Aktif kalibrasyon başlığı '{Title}'", title);
        }

        private async void CalibrationTimer_Tick(object? sender, EventArgs e)
        {
            _remainingSeconds--;

            // Read Value
            double currentValue = await GetCurrentSensorValueAsync();
            double refValue = (_currentState == CalibrationState.ZeroRunning) ? _zeroRef : _spanRef;
            _logger.LogTrace("Kalibrasyon ölçümü: Sensör={Sensor}, Durum={State}, Değer={CurrentValue:F3}, Referans={RefValue:F3}, KalanSüre={RemainingSeconds}", _currentSensor, _currentState, currentValue, refValue, _remainingSeconds);

            // Store Value
            if (_currentState == CalibrationState.ZeroRunning) _zeroValues.Add(currentValue);
            else if (_currentState == CalibrationState.SpanRunning) _spanValues.Add(currentValue);

            // Chart Update
            if (ChartCalibration != null)
            {
                ChartCalibration.Series["Ölçüm Değeri"].Points.AddY(currentValue);
                ChartCalibration.Series["Referans Değer"].Points.AddY(refValue);
            }

            // Time Logic
            if (TitleBarControlTimeRemain != null)
                TitleBarControlTimeRemain.TitleBarText = $"Kalan Süre: {_remainingSeconds} sn";

            if (_remainingSeconds <= 0)
            {
                _calibrationTimer.Stop();
                _logger.LogInformation("Kalibrasyon sayacı durduruldu. Adım tamamlanıyor: Sensör={Sensor}, Durum={State}", _currentSensor, _currentState);
                await ProcessStepCompletion();
            }
        }

        private async Task ProcessStepCompletion()
        {
            if (_currentState == CalibrationState.ZeroRunning)
            {
                // Process Zero Results
                double avg = _zeroValues.Any() ? _zeroValues.Average() : 0;
                double std = _zeroValues.Any() ? CalculateStdDev(_zeroValues, avg) : 0;
                double diff = Math.Abs(avg - _zeroRef);

                _zeroFormattedAvg = avg;
                _zeroFormattedStd = std;
                _zeroFormattedDiff = diff;
                _zeroSuccess = (diff < 0.5 && std < 0.2); // Basic tolerance

                string msg = $"Zero Kalibrasyonu Tamamlandı.\n\n" +
                             $"Ortalama: {avg:F3}\nStd Sapma: {std:F3}\nFark: {diff:F3}\n" +
                             $"Sonuç: {(_zeroSuccess ? "Başarılı" : "Başarısız")}\n\n" +
                             "Span kalibrasyonuna geçmek için Span butonuna basınız.";

                MessageBox.Show(msg, "Zero Bitti", MessageBoxButtons.OK, _zeroSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                _logger.LogInformation("Zero kalibrasyon sonuçları: Sensör={Sensor}, Avg={Avg:F3}, Std={Std:F3}, Diff={Diff:F3}, Success={Success}", _currentSensor, avg, std, diff, _zeroSuccess);

                _currentState = CalibrationState.WaitingForSpan;
                if (TitleBarControlActiveCalibration != null)
                    TitleBarControlActiveCalibration.TitleBarText = $"{_currentSensor} - Span Bekleniyor...";
            }
            else if (_currentState == CalibrationState.SpanRunning)
            {
                // Process Span Results
                double avg = _spanValues.Any() ? _spanValues.Average() : 0;
                double std = _spanValues.Any() ? CalculateStdDev(_spanValues, avg) : 0;
                double diff = Math.Abs(avg - _spanRef);
                bool spanSuccess = (diff < 0.5 && std < 0.2);
                _logger.LogInformation("Span kalibrasyon sonuçları: Sensör={Sensor}, Avg={Avg:F3}, Std={Std:F3}, Diff={Diff:F3}, Success={Success}", _currentSensor, avg, std, diff, spanSuccess);

                // Combine with Zero results (if they exist, otherwise 0)
                await SaveAndCompleteCalibration(avg, std, diff, spanSuccess);
            }
        }

        private async Task SaveAndCompleteCalibration(double spanAvg, double spanStd, double spanDiff, bool spanSuccess)
        {
            bool finalResult = (_zeroSuccess || _zeroValues.Count == 0) && spanSuccess;

             string msg = $"Kalibrasyon Süreci Tamamlandı.\n\n" +
                         $"Zero Sonuç: {(_zeroSuccess ? "OK" : "NOK")}\n" +
                         $"Span Sonuç: {(spanSuccess ? "OK" : "NOK")}\n\n" +
                         $"Station'a ve SAIS'e kaydediliyor...";
            
            MessageBox.Show(msg, "Tamamlandı", MessageBoxButtons.OK, finalResult ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            _logger.LogInformation("Kalibrasyon tamamlandı ve kaydediliyor: Sensör={Sensor}, FinalResult={FinalResult}", _currentSensor, finalResult);

            try
            {
                var stationId = await _dataCollectionService.GetStationIdAsync();
                
                var calibration = new ISKI.IBKS.Domain.Entities.Calibration(
                    stationId,
                    _currentSensor, 
                    DateTime.Now,
                    _zeroRef,
                    _zeroFormattedAvg,
                    _zeroFormattedDiff,
                    _zeroFormattedStd,
                    _spanRef,
                    spanAvg,
                    spanDiff,
                    spanStd,
                    1.0, // ResultFactor TBD
                    _zeroSuccess,
                    spanSuccess,
                    finalResult
                )
                {
                    Id = Guid.NewGuid()
                };

                await _dataCollectionService.SaveAndSendCalibrationAsync(calibration);
                UpdateLastCalibrationLabel();
                _logger.LogInformation("Kalibrasyon verileri başarıyla kaydedildi ve gönderildi. Kalibrasyon ID: {CalibrationId}", calibration.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogError(ex, "Kalibrasyon verileri kaydedilirken veya gönderilirken hata oluştu: Sensör={Sensor}", _currentSensor);
            }

            // Reset
            _currentState = CalibrationState.Idle;
            if (TitleBarControlActiveCalibration != null)
                TitleBarControlActiveCalibration.TitleBarText = "Aktif Kalibrasyon Yok";
            if (TitleBarControlTimeRemain != null)
                 TitleBarControlTimeRemain.TitleBarText = "Kalan Süre: -";
            _logger.LogInformation("Kalibrasyon süreci sıfırlandı. Durum: Idle");
        }

        private double CalculateStdDev(List<double> values, double avg)
        {
            if (values.Count <= 1) return 0;
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            return Math.Sqrt(sum / (values.Count - 1));
        }

        private async Task<double> GetCurrentSensorValueAsync()
        {
            try 
            {
                var data = await _dataCollectionService.ReadCurrentDataAsync();
                if (data == null) return 0;
                
                switch (_currentSensor)
                {
                    case "pH": return data.Ph;
                    case "İletkenlik": return data.Iletkenlik;
                    case "AKM": return data.Akm;
                    case "KOI": return data.Koi;
                    default: return 0;
                }
            }
            catch { return 0; }
        }

        protected override async void OnLoad(EventArgs e)
        {
             base.OnLoad(e);
             if (!DesignMode)
             {
                 // ensure settings loaded
                 await GetReferenceFromSettings("pH", "Zero"); 
             }
        }
        
        private void UpdateLastCalibrationLabel()
        {
            string dateText = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            switch (_currentSensor)
            {
                case "pH": if (LabelPhLastCalibration != null) LabelPhLastCalibration.Text = dateText; break;
                case "İletkenlik": if (LabelIletkenlikLastCalibration != null) LabelIletkenlikLastCalibration.Text = dateText; break;
                case "KOI": if (LabelKoiLastCalibration != null) LabelKoiLastCalibration.Text = dateText; break;
                case "AKM": if (LabelAkmLastCalibration != null) LabelAkmLastCalibration.Text = dateText; break;
            }
        }
    }
}

