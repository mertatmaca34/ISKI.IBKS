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
            get => LabelKey.Text?.Contains("PLC İletişimi") ?? false; // not exact reverse mapping; mainly read-only
            set => UpdateText(value, ApiHealthy, LastPhCalibration, LastIletkenlikCalibration);
        }

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
            // Left column: labels
            LabelKey.Text = "PLC İletişimi:\r\nAPI İletişimi:\r\nSon Kalibrasyon (pH):\r\nSon Kalibrasyon (İletkenlik):";

            // Right column: values
            var plcText = plcConnected ? "Sağlıklı ✅" : "Problemli ❌";
            var apiText = apiHealthy ? "Sağlıklı ✅" : "Problemli ❌";
            var phText = phCalib.HasValue ? phCalib.Value.ToString("dd.MM.yyyy") : "-";
            var iletText = iletCalib.HasValue ? iletCalib.Value.ToString("dd.MM.yyyy") : "-";

            LabelValue.Text = $"[{plcText}]\r\n[{apiText}]\r\n{phText}\r\n{iletText}";
        }
    }
}
