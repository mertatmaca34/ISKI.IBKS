using ISKI.IBKS.Application.Features.Calibrations.Abstractions;
using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Application.Services.DataCollection;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Presentation.WinForms.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage
{
    public partial class CalibrationPage : UserControl
    {
        private readonly IDataCollectionService _dataCollectionService;
        private readonly IPlcClient _plcClient;
        private readonly ISaisApiClient _saisApiClient;
        private readonly ILogger<CalibrationPage> _logger;

        private readonly CalibrationOps _calibrationOps;
        private readonly List<Control> _controls;

        public CalibrationPage(
            IDataCollectionService dataCollectionService,
            IPlcClient plcClient,
            ISaisApiClient saisApiClient,
            ILogger<CalibrationPage> logger)
        {
            _dataCollectionService = dataCollectionService;
            _plcClient = plcClient;
            _saisApiClient = saisApiClient;
            _logger = logger;
            _controls = new List<Control>();

            InitializeComponent();

            _calibrationOps = new CalibrationOps(_plcClient, _dataCollectionService, _saisApiClient, logger);
        }

        private void CalibrationPage_Load(object sender, EventArgs e)
        {
            AssignRandomStartValues();

            _controls.Add(CalibrationStatusBarZero);
            _controls.Add(CalibrationStatusBarSpan);
            _controls.Add(ChartCalibration);
            _controls.Add(TitleBarControlActiveCalibration);
            _controls.Add(TitleBarControlTimeRemain);
            
            // Set default labels
            LabelPhLastCalibration.Text = "Henüz yapılmadı";
            LabelIletkenlikLastCalibration.Text = "Henüz yapılmadı";
            LabelAkmLastCalibration.Text = "Henüz yapılmadı";
            LabelKoiLastCalibration.Text = "Henüz yapılmadı";
        }

        private void AssignRandomStartValues()
        {
            Random random = new();

            for (double x = 0; x <= 3; x += 0.5)
            {
                double calibrationValue = Math.Sin(x) + random.NextDouble();
                double referenceValue = 0;

                ChartCalibration.Series["Kalibrasyon Değeri"].Points.AddXY(x, calibrationValue);
                ChartCalibration.Series["Referans Değeri"].Points.AddXY(x, referenceValue);
            }
        }

        private async void ButtonAkmZero_Click(object sender, EventArgs e)
        {
            try
            {
                await _calibrationOps.InitializeAsync();
                
                var settings = await _dataCollectionService.GetStationSettingsAsync();
                if (settings == null)
                {
                    MessageBox.Show("İstasyon ayarları yüklenemedi", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int calibrationTime = settings.AkmZeroDuration;
                _calibrationOps.StartCalibration("AKM", "Zero", calibrationTime, _controls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AKM Zero kalibrasyonu başlatılırken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonKoiZero_Click(object sender, EventArgs e)
        {
            try
            {
                await _calibrationOps.InitializeAsync();
                
                var settings = await _dataCollectionService.GetStationSettingsAsync();
                if (settings == null)
                {
                    MessageBox.Show("İstasyon ayarları yüklenemedi", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int calibrationTime = settings.KoiZeroDuration;
                _calibrationOps.StartCalibration("KOi", "Zero", calibrationTime, _controls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KOI Zero kalibrasyonu başlatılırken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonPhZero_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize calibration ops with cached settings
                await _calibrationOps.InitializeAsync();
                
                var settings = await _dataCollectionService.GetStationSettingsAsync();
                if (settings == null)
                {
                    MessageBox.Show("İstasyon ayarları yüklenemedi", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int calibrationTime = settings.PhZeroDuration;
                _calibrationOps.StartCalibration("pH", "Zero", calibrationTime, _controls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "pH Zero kalibrasyonu başlatılırken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonPhSpan_Click(object sender, EventArgs e)
        {
            try
            {
                await _calibrationOps.InitializeAsync();
                
                var settings = await _dataCollectionService.GetStationSettingsAsync();
                if (settings == null)
                {
                    MessageBox.Show("İstasyon ayarları yüklenemedi", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int calibrationTime = settings.PhSpanDuration;
                _calibrationOps.StartCalibration("pH", "Span", calibrationTime, _controls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "pH Span kalibrasyonu başlatılırken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonIletkenlikZero_Click(object sender, EventArgs e)
        {
            try
            {
                await _calibrationOps.InitializeAsync();
                
                var settings = await _dataCollectionService.GetStationSettingsAsync();
                if (settings == null)
                {
                    MessageBox.Show("İstasyon ayarları yüklenemedi", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int calibrationTime = settings.ConductivityZeroDuration;
                _calibrationOps.StartCalibration("Iletkenlik", "Zero", calibrationTime, _controls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletkenlik Zero kalibrasyonu başlatılırken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonIletkenlikSpan_Click(object sender, EventArgs e)
        {
            try
            {
                await _calibrationOps.InitializeAsync();
                
                var settings = await _dataCollectionService.GetStationSettingsAsync();
                if (settings == null)
                {
                    MessageBox.Show("İstasyon ayarları yüklenemedi", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int calibrationTime = settings.ConductivitySpanDuration;
                _calibrationOps.StartCalibration("Iletkenlik", "Span", calibrationTime, _controls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletkenlik Span kalibrasyonu başlatılırken hata oluştu");
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
