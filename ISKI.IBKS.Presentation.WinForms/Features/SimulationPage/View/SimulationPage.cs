using ISKI.IBKS.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.Presenter;
using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.Presentation.WinForms.Common.Helpers;

namespace ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.View;

public partial class SimulationPage : UserControl, ISimulationPageView
{
        private Label _statusOverlay;
        private readonly Bitmap _autoFrame;
        private readonly Bitmap _autoFrame2;
        private readonly Bitmap _systemMaintenance;
        private readonly Bitmap _wash1;
        private readonly Bitmap _wash2;
        private readonly Bitmap _pump1Idle, _pump2Idle;
        private readonly Bitmap _pump1Animation, _pump2Animation;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParms = base.CreateParams;
                handleParms.ExStyle |= 0x02000000;
                return handleParms;
            }
        }
    public SimulationPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();

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

        this.BackgroundImage = _autoFrame;
        
        InitializeLocalization();
        ActivatorUtilities.CreateInstance<SimulationPagePresenter>(serviceProvider, this);
    }

        private void InitializeLocalization()
        {
            label1.Text = Strings.Sensor_Akm;
            label3.Text = Strings.Sensor_Koi;
            label5.Text = Strings.Sensor_Ph;
            label7.Text = Strings.Sensor_Iletkenlik;
            label9.Text = Strings.Sensor_DO;
            label11.Text = Strings.Sim_FlowVelocity;
            label13.Text = Strings.Sensor_Flow;
            label15.Text = Strings.Sim_DischargeFlow;
            label17.Text = Strings.Sim_ExtFlow;
            label19.Text = Strings.Sim_ExtFlow2;
            label21.Text = Strings.Sim_ActivePump;
            label23.Text = Strings.Sim_Frequency;
            label25.Text = Strings.Sensor_Sicaklik;
            label27.Text = Strings.Sim_Humidity;
        }

        public void UpdateDisplay(PlcDataSnapshot snapshot, bool blinkState)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateDisplay(snapshot, blinkState)));
                return;
            }

            LabelPh.Text = snapshot.Ph.ToString("F2");
            LabelIletkenlik.Text = snapshot.Iletkenlik.ToString("F2");
            LabelOksijen.Text = snapshot.CozunmusOksijen.ToString("F2");
            LabelKoi.Text = snapshot.Koi.ToString("F2");
            LabelAkm.Text = snapshot.Akm.ToString("F2");

            LabelDebi.Text = snapshot.TesisDebi.ToString("F2");
            LabelDesarjDebi.Text = snapshot.DesarjDebi.ToString("F2");
            LabelHariciDebi.Text = snapshot.HariciDebi.ToString("F2");
            LabelHariciDebi2.Text = snapshot.HariciDebi2.ToString("F2");
            LabelAkisHizi.Text = snapshot.OlcumCihaziAkisHizi.ToString("F2");

            LabelSicaklik.Text = snapshot.KabinSicakligi.ToString("F1");
            LabelNem.Text = snapshot.KabinNemi.ToString("F1");

            if (snapshot.Pompa1Calisiyor == true) LabelFrekans.Text = $"{snapshot.Pompa1CalismaFrekansi:F1} Hz";
            else if (snapshot.Pompa2Calisiyor == true) LabelFrekans.Text = $"{snapshot.Pompa2CalismaFrekansi:F1} Hz";
            else LabelFrekans.Text = "0,0 Hz";

            if (snapshot.Pompa1Calisiyor == true) LabelAktifPompa.Text = Strings.Pump_One;
            else if (snapshot.Pompa2Calisiyor == true) LabelAktifPompa.Text = Strings.Pump_Two;
            else LabelAktifPompa.Text = Strings.Sim_Stopped;

            AnimateSystemStatus(snapshot);
            AnimatePumps(snapshot);
            AnimateDoor(snapshot);
            AnimateWaterTank(snapshot);

            LabelPh.ForeColor = (snapshot.Ph > 9 || snapshot.Ph < 6) ? Color.Red : Color.Lime;
            LabelIletkenlik.ForeColor = (snapshot.Iletkenlik > 2000) ? Color.Red : Color.Lime;

            UpdateStatusOverlay(snapshot, blinkState);
        }

        public void ShowError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void AnimateSystemStatus(PlcDataSnapshot snapshot)
        {
            if (snapshot.KabinOtoModu == true && snapshot.KabinHaftalikYikamada == false && snapshot.KabinSaatlikYikamada == false)
                FrameOperations.ChangeControlFrame(this, _autoFrame, _autoFrame2);
            else if (snapshot.KabinBakimModu == true || snapshot.KabinKalibrasyonModu == true)
            {
                if (this.BackgroundImage != _systemMaintenance) this.BackgroundImage = _systemMaintenance;
            }
            else if (snapshot.KabinSaatlikYikamada == true || snapshot.KabinHaftalikYikamada == true)
                FrameOperations.ChangeControlFrame(this, _wash1, _wash2);
        }

        private void AnimatePumps(PlcDataSnapshot snapshot)
        {
            FrameOperations.ChangePictureBoxFrame(PictureBoxPump1, _pump1Animation, _pump1Idle, snapshot.Pompa1Calisiyor == true ? PumpState.Working : PumpState.Idle);
            FrameOperations.ChangePictureBoxFrame(PictureBoxPump2, _pump2Animation, _pump2Idle, snapshot.Pompa2Calisiyor == true ? PumpState.Working : PumpState.Idle);
        }

        private void AnimateDoor(PlcDataSnapshot snapshot) => 
            FrameOperations.ChangePanelFrame(PanelDoor, snapshot.KabinKapiAlarmi == true ? Properties.Resources.door_opened : Properties.Resources.door_closed);

        private void AnimateWaterTank(PlcDataSnapshot snapshot) => 
            FrameOperations.ChangePanelFrame(PanelWaterTank, snapshot.YikamaTankiBosAlarmi == true ? Properties.Resources.water_tank_empty : Properties.Resources.water_tank_full);

        private void UpdateStatusOverlay(PlcDataSnapshot s, bool blinkState)
        {
            if (_statusOverlay == null) return;
            var alerts = new List<string>();
            if (s.KabinSuBaskiniAlarmi == true) alerts.Add(Strings.Sim_Flood);
            if (s.KabinDumanAlarmi == true) alerts.Add(Strings.Sim_Smoke);
            if (s.KabinEnerjiAlarmi == true) alerts.Add(Strings.Sim_NoEnergy);
            if (s.KabinAcilStopBasiliAlarmi == true) alerts.Add(Strings.Sim_EmergencyStop);

            if (alerts.Count > 0)
            {
                _statusOverlay.Visible = blinkState;
                _statusOverlay.BackColor = Color.Red;
                _statusOverlay.ForeColor = Color.White;
                _statusOverlay.Text = string.Join(" - ", alerts);
                return;
            }

            if (s.KabinBakimModu == true) { _statusOverlay.Visible = true; _statusOverlay.BackColor = Color.Orange; _statusOverlay.ForeColor = Color.Black; _statusOverlay.Text = Strings.Sim_Maintenance; return; }
            if (s.KabinKalibrasyonModu == true) { _statusOverlay.Visible = true; _statusOverlay.BackColor = Color.Yellow; _statusOverlay.ForeColor = Color.Black; _statusOverlay.Text = Strings.Sim_Calibration; return; }
            if (s.KabinSaatlikYikamada == true || s.KabinHaftalikYikamada == true) { _statusOverlay.Visible = true; _statusOverlay.BackColor = Color.Cyan; _statusOverlay.ForeColor = Color.Black; _statusOverlay.Text = Strings.Sim_Washing; return; }
            if (s.AkmNumuneTetik == true || s.KoiNumuneTetik == true || s.PhNumuneTetik == true || s.ManuelTetik == true || s.SimNumuneTetik == true)
            { _statusOverlay.Visible = true; _statusOverlay.BackColor = Color.LightGreen; _statusOverlay.ForeColor = Color.Black; _statusOverlay.Text = Strings.Sim_Sampling; return; }

            _statusOverlay.Visible = false;
        }

        private void SimulationPage_Load(object sender, EventArgs e)
        {
            string customBg = Path.Combine(System.Windows.Forms.Application.StartupPath, "scada_bg.png");
            if (File.Exists(customBg)) { this.BackgroundImage = Image.FromFile(customBg); this.BackgroundImageLayout = ImageLayout.Stretch; }

            _statusOverlay = new Label { AutoSize = false, Size = new Size(400, 40), Location = new Point((this.Width - 400) / 2, 10), Anchor = AnchorStyles.Top, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Arial", 16, FontStyle.Bold), BackColor = Color.Red, ForeColor = Color.White, Visible = false };
            this.Controls.Add(_statusOverlay);
            _statusOverlay.BringToFront();
        }
    }

