using ISKI.IBKS.Shared.Localization;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls
{
    public partial class HealthSummaryCardControl : UserControl
    {
        public HealthSummaryCardControl()
        {
            InitializeComponent();
            InitializeLocalization();
        }

        private void InitializeLocalization()
        {
            // Placeholder for any structural localization if needed
            UpdateText(PlcConnected, ApiHealthy, LastPhCalibration, LastIletkenlikCalibration);
        }

        public string Title
        {
            get => LabelKey.Text ?? string.Empty;
            set => LabelKey.Text = value ?? string.Empty;
        }

        public string Value
        {
            get => LabelValue.Text ?? string.Empty;
            set => LabelValue.Text = value ?? string.Empty;
        }

        public Image? StatusImage
        {
            get => PictureStatus.Image;
            set
            {
                PictureStatus.Image = value;
                PictureStatus.Visible = value is not null;
            }
        }

        public bool PlcConnected
        {
            get => _plcConnected;
            set
            {
                _plcConnected = value;
                UpdateText(_plcConnected, ApiHealthy, LastPhCalibration, LastIletkenlikCalibration);
            }
        }
        private bool _plcConnected;

        private bool _apiHealthy;
        public bool ApiHealthy
        {
            get => _apiHealthy;
            set
            {
                _apiHealthy = value;
                UpdateText(PlcConnected, _apiHealthy, LastPhCalibration, LastIletkenlikCalibration);
            }
        }

        private DateTime? _lastPhCalibration;
        public DateTime? LastPhCalibration
        {
            get => _lastPhCalibration;
            set
            {
                _lastPhCalibration = value;
                UpdateText(PlcConnected, ApiHealthy, _lastPhCalibration, LastIletkenlikCalibration);
            }
        }

        private DateTime? _lastIletkenlikCalibration;
        public DateTime? LastIletkenlikCalibration
        {
            get => _lastIletkenlikCalibration;
            set
            {
                _lastIletkenlikCalibration = value;
                UpdateText(PlcConnected, ApiHealthy, LastPhCalibration, _lastIletkenlikCalibration);
            }
        }

        private void UpdateText(bool plcConnected, bool apiHealthy, DateTime? phCalib, DateTime? iletCalib)
        {
            LabelKey.Text = $"{Strings.Health_PlcConnection}:\r\n{Strings.Health_ApiConnection}:\r\n{Strings.Health_LastPhCalib}:\r\n{Strings.Health_LastCondCalib}:";

            var plcText = plcConnected ? Strings.Common_Healthy : Strings.Common_Problematic;
            var apiText = apiHealthy ? Strings.Common_Healthy : Strings.Common_Problematic;
            var phText = phCalib.HasValue ? phCalib.Value.ToString("dd.MM.yyyy") : "-";
            var iletText = iletCalib.HasValue ? iletCalib.Value.ToString("dd.MM.yyyy") : "-";

            LabelValue.Text = $"[{plcText}]\r\n[{apiText}]\r\n{phText}\r\n{iletText}";
        }
    }
}

