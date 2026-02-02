using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.Presenter;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.View;

public partial class PlcSettingsPage : UserControl, IPlcSettingsPageView
{
    public event EventHandler SaveRequested;
    private readonly List<string> AvailableSensors = new()
    {
        "TesisDebi", "OlcumCihaziAkisHizi", "Ph", "Iletkenlik", "CozunmusOksijen",
        "Koi", "Akm", "KabinSicakligi",
        "DesarjDebi", "HariciDebi", "HariciDebi2"
    };

        public string PlcIp
        {
            get => PlcSettingsControlIp.AyarDegeri;
            set => PlcSettingsControlIp.AyarDegeri = value;
        }

        public List<string> SelectedSensors
        {
            get
            {
                var selected = new List<string>();
                foreach (Control control in FlowLayoutPanelSensors.Controls)
                {
                    if (control is CheckBox cb && cb.Checked && cb.Tag is string sensorKey)
                        selected.Add(sensorKey);
                }
                return selected;
            }
            set
            {
                foreach (Control control in FlowLayoutPanelSensors.Controls)
                {
                    if (control is CheckBox cb && cb.Tag is string sensorKey)
                        cb.Checked = value.Contains(sensorKey);
                }
            }
        }

        public PlcSettingsPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            InitializeLocalization();
            PopulateSensorsList();
            ButtonSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);

            ActivatorUtilities.CreateInstance<PlcSettingsPresenter>(serviceProvider, this);
        }

        private void InitializeLocalization()
        {
            titleBarControl1.TitleBarText = Strings.Settings_Plc;
            PlcSettingsControlIp.AyarAdi = Strings.Plc_Ip_Label;
            LabelSensors.Text = Strings.Plc_Sensors;
            ButtonSave.Text = Strings.Common_Save;
        }

        private void PopulateSensorsList()
        {
            FlowLayoutPanelSensors.Controls.Clear();
            foreach (var sensor in AvailableSensors)
                FlowLayoutPanelSensors.Controls.Add(CreateSensorCheckBox(sensor));
        }

        private CheckBox CreateSensorCheckBox(string sensorName)
        {
            var cb = new CheckBox
            {
                Text = GetFriendlyName(sensorName),
                Tag = sensorName,
                AutoSize = false,
                Size = new Size(180, 45),
                Appearance = Appearance.Button,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Margin = new Padding(5),
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.DimGray,
                Cursor = Cursors.Hand
            };

            cb.FlatAppearance.BorderSize = 1;
            cb.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
            cb.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 131, 200);
            cb.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);

            cb.CheckedChanged += (s, e) =>
            {
                cb.ForeColor = cb.Checked ? Color.White : Color.DimGray;
                cb.FlatAppearance.BorderColor = cb.Checked ? Color.FromArgb(0, 131, 200) : Color.FromArgb(220, 220, 220);
            };

            return cb;
        }

        private string GetFriendlyName(string sensorKey) => sensorKey switch
        {
            "TesisDebi" => Strings.Sensor_TesisDebi,
            "OlcumCihaziAkisHizi" => Strings.Sensor_AkisHizi,
            "Ph" => Strings.Sensor_Ph,
            "Iletkenlik" => Strings.Sensor_Iletkenlik,
            "CozunmusOksijen" => Strings.Sensor_CozunmusOksijen,
            "Koi" => Strings.Sensor_Koi,
            "Akm" => Strings.Sensor_Akm,
            "KabinSicakligi" => Strings.Sensor_KabinSicakligi,
            "Pompa1Hz" => "Pompa 1 Hz",
            "Pompa2Hz" => "Pompa 2 Hz",
            "DesarjDebi" => Strings.Sensor_DesarjDebi,
            "HariciDebi" => Strings.Sensor_HariciDebi1,
            "HariciDebi2" => Strings.Sensor_HariciDebi2,
            _ => sensorKey
        };

        public void ShowInfo(string message) => MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void ShowError(string message) => MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

