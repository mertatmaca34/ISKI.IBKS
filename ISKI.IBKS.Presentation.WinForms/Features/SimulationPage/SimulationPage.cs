using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;

namespace ISKI.IBKS.Presentation.WinForms.Features.SimulationPage
{
    public partial class SimulationPage : UserControl
    {
        IOptions<PlcSettings> _plcSettings;
        IStationSnapshotCache _stationSnapshotCache;

        // Custom Overlay Label for Status
        private Label _statusOverlay;

        System.Windows.Forms.Timer _simulationTimer;
        private bool _blinkState;

        #region No Image-Flick

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParms = base.CreateParams;
                handleParms.ExStyle |= 0x02000000;
                return handleParms;
            }
        }

        #endregion

        public SimulationPage(IStationSnapshotCache stationSnapshotCache, IOptions<PlcSettings> plcSettings)
        {
            InitializeComponent();

            _stationSnapshotCache = stationSnapshotCache;
            _plcSettings = plcSettings;
        }

        private async void SimulationTimer_Tick(object sender, EventArgs e)
        {
            if (_stationSnapshotCache == null || _plcSettings?.Value == null) return;

            var snapshot = await _stationSnapshotCache.Get(_plcSettings.Value.Station.StationId);
            if (snapshot == null)
                return;

            _blinkState = !_blinkState; // Toggle blink

            // Update Analog Labels
            LabelPh.Text = snapshot.Ph?.ToString("F2") ?? "-";
            LabelIletkenlik.Text = snapshot.Iletkenlik?.ToString("F2") ?? "-";
            LabelOksijen.Text = snapshot.CozunmusOksijen?.ToString("F2") ?? "-";
            LabelKoi.Text = snapshot.Koi?.ToString("F2") ?? "-";
            LabelAkm.Text = snapshot.Akm?.ToString("F2") ?? "-";
            
            LabelDebi.Text = snapshot.TesisDebi?.ToString("F2") ?? "-";
            LabelDesarjDebi.Text = snapshot.DesarjDebi?.ToString("F2") ?? "-";
            LabelHariciDebi.Text = snapshot.HariciDebi?.ToString("F2") ?? "-";
            LabelHariciDebi2.Text = snapshot.HariciDebi2?.ToString("F2") ?? "-";
            LabelAkisHizi.Text = snapshot.OlcumCihaziAkisHizi?.ToString("F2") ?? "-";

            LabelSicaklik.Text = snapshot.KabinSicakligi?.ToString("F1") ?? "-";
            LabelNem.Text = snapshot.KabinNemi?.ToString("F1") ?? "-";

            // Update Pump Frequencies
            LabelFrekans.Text = $"P1: {snapshot.Pompa1CalismaFrekansi:F1} / P2: {snapshot.Pompa2CalismaFrekansi:F1}";

            // Update Active Pump Indicator
            if (snapshot.Pompa1Calisiyor == true) LabelAktifPompa.Text = "Pompa 1";
            else if (snapshot.Pompa2Calisiyor == true) LabelAktifPompa.Text = "Pompa 2";
            else LabelAktifPompa.Text = "Duruyor";

            // Update Pump Visuals
            PictureBoxPump1.Image = (snapshot.Pompa1Calisiyor == true) ? Properties.Resources.pump1_animation : Properties.Resources.pump1_idle;
            PictureBoxPump2.Image = (snapshot.Pompa2Calisiyor == true) ? Properties.Resources.pump2_animation : Properties.Resources.pump2_idle;

            // Update Door
            PanelDoor.BackgroundImage = (snapshot.KabinKapiAlarmi == true) ? Properties.Resources.door_opened : Properties.Resources.door_closed;

            // Water Tank Level
            PanelWaterTank.BackgroundImage = (snapshot.YikamaTankiBosAlarmi == true) ? Properties.Resources.water_tank_empty : Properties.Resources.water_tank_full;

            // Color Coding Sensors
            LabelPh.ForeColor = (snapshot.Ph > 9 || snapshot.Ph < 6) ? Color.Red : Color.Lime;
            LabelIletkenlik.ForeColor = (snapshot.Iletkenlik > 2000) ? Color.Red : Color.Lime;

            // Update Status Overlay
            UpdateStatusOverlay(snapshot);
        }

        private void UpdateStatusOverlay(StationSnapshotDto s)
        {
            if (_statusOverlay == null) return;

            var alerts = new List<string>();

            // Critical Alarms (Blinking Red)
            if (s.KabinSuBaskiniAlarmi == true) alerts.Add("SU BASKINI!");
            if (s.KabinDumanAlarmi == true) alerts.Add("DUMAN ALARMI!");
            if (s.KabinEnerjiAlarmi == true) alerts.Add("ENERJİ YOK!");
            if (s.KabinAcilStopBasiliAlarmi == true) alerts.Add("ACİL STOP BASILI!");

            if (alerts.Count > 0)
            {
                _statusOverlay.Visible = _blinkState; // Blink
                _statusOverlay.BackColor = Color.Red;
                _statusOverlay.ForeColor = Color.White;
                _statusOverlay.Text = string.Join(" - ", alerts);
                return;
            }

            // Operational Modes (Solid Color)
            if (s.KabinBakimModu == true)
            {
                _statusOverlay.Visible = true;
                _statusOverlay.BackColor = Color.Orange;
                _statusOverlay.ForeColor = Color.Black;
                _statusOverlay.Text = "BAKIM MODU";
                return;
            }

            if (s.KabinKalibrasyonModu == true)
            {
                _statusOverlay.Visible = true;
                _statusOverlay.BackColor = Color.Yellow;
                _statusOverlay.ForeColor = Color.Black;
                _statusOverlay.Text = "KALİBRASYON MODU";
                return;
            }

            if (s.KabinSaatlikYikamada == true || s.KabinHaftalikYikamada == true)
            {
                 _statusOverlay.Visible = true;
                _statusOverlay.BackColor = Color.Cyan;
                _statusOverlay.ForeColor = Color.Black;
                _statusOverlay.Text = "SİSTEM YIKAMADA";
                return;
            }

            // Sampling Status
            if (s.AkmNumuneTetik == true || s.KoiNumuneTetik == true || s.PhNumuneTetik == true || s.ManuelTetik == true || s.SimNumuneTetik == true)
            {
                _statusOverlay.Visible = true;
                _statusOverlay.BackColor = Color.LightGreen;
                _statusOverlay.ForeColor = Color.Black;
                _statusOverlay.Text = "NUMUNE ALINIYOR...";
                return;
            }

            // Normal Operation
            _statusOverlay.Visible = false;
        }

        private void SimulationPage_Load(object sender, EventArgs e)
        {
             // Optional: Load custom background if exists
             string customBg = Path.Combine(System.Windows.Forms.Application.StartupPath, "scada_bg.png");
             if (File.Exists(customBg))
             {
                 this.BackgroundImage = Image.FromFile(customBg);
                 this.BackgroundImageLayout = ImageLayout.Stretch;
             }
             
             // Create Status Overlay Programmatically
             _statusOverlay = new Label();
             _statusOverlay.AutoSize = false;
             _statusOverlay.Size = new Size(400, 40);
             _statusOverlay.Location = new Point((this.Width - 400) / 2, 10); // Center Top
             _statusOverlay.Anchor = AnchorStyles.Top;
             _statusOverlay.TextAlign = ContentAlignment.MiddleCenter;
             _statusOverlay.Font = new Font("Arial", 16, FontStyle.Bold);
             _statusOverlay.BackColor = Color.Red;
             _statusOverlay.ForeColor = Color.White;
             _statusOverlay.Visible = false;
             this.Controls.Add(_statusOverlay);
             _statusOverlay.BringToFront();

             _simulationTimer = new System.Windows.Forms.Timer();
             _simulationTimer.Interval = 1000;
             _simulationTimer.Tick += SimulationTimer_Tick;
             _simulationTimer.Start();
        }
    }
}
