using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using ISKI.IBKS.Presentation.WinForms.Common.Helpers;
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

        // Background frames for animations
        private readonly Bitmap _autoFrame;
        private readonly Bitmap _autoFrame2;
        private readonly Bitmap _systemMaintenance;
        private readonly Bitmap _wash1;
        private readonly Bitmap _wash2;
        private readonly Bitmap _pump1Idle, _pump2Idle;
        private readonly Bitmap _pump1Animation, _pump2Animation;


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

            // Load background frames
            _autoFrame = Properties.Resources.system_auto1;
            _autoFrame2 = Properties.Resources.system_auto2;
            _systemMaintenance = Properties.Resources.system_wait;
            _wash1 = Properties.Resources.wash1;
            _wash2 = Properties.Resources.wash2;
            _pump1Idle = Properties.Resources.pump1_idle;
            _pump2Idle = Properties.Resources.pump2_idle;
            _pump1Animation = Properties.Resources.pump1_animation;
            _pump2Animation = Properties.Resources.pump2_animation;

            PictureBoxPump1.Image = _pump1Idle;
            PictureBoxPump2.Image = _pump2Idle;

            // Set initial background
            this.BackgroundImage = _autoFrame;
        }


        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            if (_stationSnapshotCache == null || _plcSettings?.Value == null) return;

            // Stop timer to prevent overlapping ticks
            _simulationTimer?.Stop();

            try
            {
                // Get snapshot synchronously using Task.Run to avoid blocking
                var snapshot = Task.Run(async () => 
                    await _stationSnapshotCache.Get(_plcSettings.Value.Station.StationId)).Result;

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

                // Update Pump Frequencies (Only Active Pump)
                if (snapshot.Pompa1Calisiyor == true)
                {
                    LabelFrekans.Text = $"{snapshot.Pompa1CalismaFrekansi:F1} Hz";
                }
                else if (snapshot.Pompa2Calisiyor == true)
                {
                    LabelFrekans.Text = $"{snapshot.Pompa2CalismaFrekansi:F1} Hz";
                }
                else 
                {
                    LabelFrekans.Text = "0,0 Hz";
                }

                // Update Active Pump Indicator
                if (snapshot.Pompa1Calisiyor == true) LabelAktifPompa.Text = "Pompa 1";
                else if (snapshot.Pompa2Calisiyor == true) LabelAktifPompa.Text = "Pompa 2";
                else LabelAktifPompa.Text = "Duruyor";

                // Perform Animations
                AnimateSystemStatus(snapshot);
                AnimatePumps(snapshot);
                AnimateDoor(snapshot);
                AnimateWaterTank(snapshot);

                // Color Coding Sensors
                LabelPh.ForeColor = (snapshot.Ph > 9 || snapshot.Ph < 6) ? Color.Red : Color.Lime;
                LabelIletkenlik.ForeColor = (snapshot.Iletkenlik > 2000) ? Color.Red : Color.Lime;

                // Update Status Overlay
                UpdateStatusOverlay(snapshot);
            }
            finally
            {
                // Restart timer
                _simulationTimer?.Start();
            }
        }

        private void AnimateSystemStatus(StationSnapshotDto snapshot)
        {
            // System Status Animation (Background Frame)
            if (snapshot.KabinOtoModu == true && 
                snapshot.KabinHaftalikYikamada == false && 
                snapshot.KabinSaatlikYikamada == false)
            {
                // Auto mode - alternate between two frames
                FrameOperations.ChangeControlFrame(this, _autoFrame, _autoFrame2);
            }
            else if (snapshot.KabinBakimModu == true || snapshot.KabinKalibrasyonModu == true)
            {
                // Maintenance/Calibration mode - static maintenance image
                if (this.BackgroundImage != _systemMaintenance)
                {
                    this.BackgroundImage = _systemMaintenance;
                }
            }
            else if (snapshot.KabinSaatlikYikamada == true || snapshot.KabinHaftalikYikamada == true)
            {
                // Washing mode - alternate between wash frames
                FrameOperations.ChangeControlFrame(this, _wash1, _wash2);
            }
        }

        private void AnimatePumps(StationSnapshotDto snapshot)
        {
            // Pump 1 Animation
            if (snapshot.Pompa1Calisiyor == true)
            {
                FrameOperations.ChangePictureBoxFrame(PictureBoxPump1, _pump1Animation, _pump1Idle, PumpState.Working);
            }
            else
            {
                FrameOperations.ChangePictureBoxFrame(PictureBoxPump1, _pump1Animation, _pump1Idle, PumpState.Idle);
            }

            // Pump 2 Animation
            if (snapshot.Pompa2Calisiyor == true)
            {
                FrameOperations.ChangePictureBoxFrame(PictureBoxPump2, _pump2Animation, _pump2Idle, PumpState.Working);
            }
            else
            {
                FrameOperations.ChangePictureBoxFrame(PictureBoxPump2, _pump2Animation, _pump2Idle, PumpState.Idle);
            }
        }

        private void AnimateDoor(StationSnapshotDto snapshot)
        {
            // Door Status
            if (snapshot.KabinKapiAlarmi == true)
            {
                FrameOperations.ChangePanelFrame(PanelDoor, Properties.Resources.door_opened);
            }
            else
            {
                FrameOperations.ChangePanelFrame(PanelDoor, Properties.Resources.door_closed);
            }
        }

        private void AnimateWaterTank(StationSnapshotDto snapshot)
        {
            // Water Tank Level
            if (snapshot.YikamaTankiBosAlarmi == true)
            {
                FrameOperations.ChangePanelFrame(PanelWaterTank, Properties.Resources.water_tank_empty);
            }
            else
            {
                FrameOperations.ChangePanelFrame(PanelWaterTank, Properties.Resources.water_tank_full);
            }
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
