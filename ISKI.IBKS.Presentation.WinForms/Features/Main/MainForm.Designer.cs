using ISKI.IBKS.Presentation.WinForms.Features.Main.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            HomePageButton = new NavigationBarButton();
            SimulationPageButton = new NavigationBarButton();
            CalibrationPageButton = new NavigationBarButton();
            MailPageButton = new NavigationBarButton();
            ReportingPageButton = new NavigationBarButton();
            SettingsPageButton = new NavigationBarButton();
            ModeSwitcherButton = new NavigationBarButton();
            panel1 = new Panel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(HomePageButton, 0, 0);
            tableLayoutPanel1.Controls.Add(SimulationPageButton, 0, 1);
            tableLayoutPanel1.Controls.Add(CalibrationPageButton, 0, 2);
            tableLayoutPanel1.Controls.Add(MailPageButton, 0, 3);
            tableLayoutPanel1.Controls.Add(ReportingPageButton, 0, 4);
            tableLayoutPanel1.Controls.Add(SettingsPageButton, 0, 5);
            tableLayoutPanel1.Controls.Add(ModeSwitcherButton, 0, 7);
            tableLayoutPanel1.Dock = DockStyle.Left;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.MaximumSize = new Size(90, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.Size = new Size(90, 681);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // HomePageButton
            // 
            HomePageButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            HomePageButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            HomePageButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            HomePageButton.BackColor = Color.White;
            HomePageButton.CornerRadius = 10;
            HomePageButton.FlatAppearance.BorderSize = 0;
            HomePageButton.FlatStyle = FlatStyle.Flat;
            HomePageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            HomePageButton.ForeColor = Color.DimGray;
            HomePageButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            HomePageButton.Image = Properties.Resources.home_24px;
            HomePageButton.InactiveBackColor = Color.White;
            HomePageButton.InactiveForeColor = Color.DimGray;
            HomePageButton.IsActive = false;
            HomePageButton.Location = new Point(8, 8);
            HomePageButton.Margin = new Padding(8);
            HomePageButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            HomePageButton.Name = "HomePageButton";
            HomePageButton.Size = new Size(74, 68);
            HomePageButton.TabIndex = 1;
            HomePageButton.Text = "Anasayfa";
            HomePageButton.TextAlign = ContentAlignment.BottomCenter;
            HomePageButton.UseVisualStyleBackColor = false;
            // 
            // SimulationPageButton
            // 
            SimulationPageButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            SimulationPageButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            SimulationPageButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            SimulationPageButton.BackColor = Color.White;
            SimulationPageButton.CornerRadius = 10;
            SimulationPageButton.FlatAppearance.BorderSize = 0;
            SimulationPageButton.FlatStyle = FlatStyle.Flat;
            SimulationPageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            SimulationPageButton.ForeColor = Color.DimGray;
            SimulationPageButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            SimulationPageButton.Image = Properties.Resources.monitor_24px;
            SimulationPageButton.InactiveBackColor = Color.White;
            SimulationPageButton.InactiveForeColor = Color.DimGray;
            SimulationPageButton.IsActive = false;
            SimulationPageButton.Location = new Point(8, 92);
            SimulationPageButton.Margin = new Padding(8);
            SimulationPageButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            SimulationPageButton.Name = "SimulationPageButton";
            SimulationPageButton.Size = new Size(74, 68);
            SimulationPageButton.TabIndex = 1;
            SimulationPageButton.Text = "Simulasyon";
            SimulationPageButton.TextAlign = ContentAlignment.BottomCenter;
            SimulationPageButton.UseVisualStyleBackColor = false;
            // 
            // CalibrationPageButton
            // 
            CalibrationPageButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            CalibrationPageButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            CalibrationPageButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            CalibrationPageButton.BackColor = Color.White;
            CalibrationPageButton.CornerRadius = 10;
            CalibrationPageButton.FlatAppearance.BorderSize = 0;
            CalibrationPageButton.FlatStyle = FlatStyle.Flat;
            CalibrationPageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            CalibrationPageButton.ForeColor = Color.DimGray;
            CalibrationPageButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            CalibrationPageButton.Image = Properties.Resources.azimuth_24px;
            CalibrationPageButton.InactiveBackColor = Color.White;
            CalibrationPageButton.InactiveForeColor = Color.DimGray;
            CalibrationPageButton.IsActive = false;
            CalibrationPageButton.Location = new Point(8, 176);
            CalibrationPageButton.Margin = new Padding(8);
            CalibrationPageButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            CalibrationPageButton.Name = "CalibrationPageButton";
            CalibrationPageButton.Size = new Size(74, 68);
            CalibrationPageButton.TabIndex = 1;
            CalibrationPageButton.Text = "Kalibrasyon";
            CalibrationPageButton.TextAlign = ContentAlignment.BottomCenter;
            CalibrationPageButton.UseVisualStyleBackColor = false;
            // 
            // MailPageButton
            // 
            MailPageButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            MailPageButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            MailPageButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            MailPageButton.BackColor = Color.White;
            MailPageButton.CornerRadius = 10;
            MailPageButton.FlatAppearance.BorderSize = 0;
            MailPageButton.FlatStyle = FlatStyle.Flat;
            MailPageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            MailPageButton.ForeColor = Color.DimGray;
            MailPageButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            MailPageButton.Image = Properties.Resources.alarm_24px;
            MailPageButton.InactiveBackColor = Color.White;
            MailPageButton.InactiveForeColor = Color.DimGray;
            MailPageButton.IsActive = false;
            MailPageButton.Location = new Point(8, 260);
            MailPageButton.Margin = new Padding(8);
            MailPageButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            MailPageButton.Name = "MailPageButton";
            MailPageButton.Size = new Size(74, 68);
            MailPageButton.TabIndex = 1;
            MailPageButton.Text = "Mail";
            MailPageButton.TextAlign = ContentAlignment.BottomCenter;
            MailPageButton.UseVisualStyleBackColor = false;
            // 
            // ReportingPageButton
            // 
            ReportingPageButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            ReportingPageButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            ReportingPageButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            ReportingPageButton.BackColor = Color.White;
            ReportingPageButton.CornerRadius = 10;
            ReportingPageButton.FlatAppearance.BorderSize = 0;
            ReportingPageButton.FlatStyle = FlatStyle.Flat;
            ReportingPageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            ReportingPageButton.ForeColor = Color.DimGray;
            ReportingPageButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            ReportingPageButton.Image = Properties.Resources.save_24px;
            ReportingPageButton.InactiveBackColor = Color.White;
            ReportingPageButton.InactiveForeColor = Color.DimGray;
            ReportingPageButton.IsActive = false;
            ReportingPageButton.Location = new Point(8, 344);
            ReportingPageButton.Margin = new Padding(8);
            ReportingPageButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            ReportingPageButton.Name = "ReportingPageButton";
            ReportingPageButton.Size = new Size(74, 68);
            ReportingPageButton.TabIndex = 1;
            ReportingPageButton.Text = "Raporlama";
            ReportingPageButton.TextAlign = ContentAlignment.BottomCenter;
            ReportingPageButton.UseVisualStyleBackColor = false;
            // 
            // SettingsPageButton
            // 
            SettingsPageButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            SettingsPageButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            SettingsPageButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            SettingsPageButton.BackColor = Color.White;
            SettingsPageButton.CornerRadius = 10;
            SettingsPageButton.FlatAppearance.BorderSize = 0;
            SettingsPageButton.FlatStyle = FlatStyle.Flat;
            SettingsPageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            SettingsPageButton.ForeColor = Color.DimGray;
            SettingsPageButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            SettingsPageButton.Image = Properties.Resources.settings_24px;
            SettingsPageButton.InactiveBackColor = Color.White;
            SettingsPageButton.InactiveForeColor = Color.DimGray;
            SettingsPageButton.IsActive = false;
            SettingsPageButton.Location = new Point(8, 428);
            SettingsPageButton.Margin = new Padding(8);
            SettingsPageButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            SettingsPageButton.Name = "SettingsPageButton";
            SettingsPageButton.Size = new Size(74, 68);
            SettingsPageButton.TabIndex = 1;
            SettingsPageButton.Text = "Ayarlar";
            SettingsPageButton.TextAlign = ContentAlignment.BottomCenter;
            SettingsPageButton.UseVisualStyleBackColor = false;
            // 
            // ModeSwitcherButton
            // 
            ModeSwitcherButton.ActiveBackColor = Color.FromArgb(230, 240, 255);
            ModeSwitcherButton.ActiveForeColor = Color.FromArgb(0, 102, 204);
            ModeSwitcherButton.ActiveHoverBackColor = Color.FromArgb(210, 230, 255);
            ModeSwitcherButton.BackColor = Color.White;
            ModeSwitcherButton.CornerRadius = 10;
            ModeSwitcherButton.FlatAppearance.BorderSize = 0;
            ModeSwitcherButton.FlatStyle = FlatStyle.Flat;
            ModeSwitcherButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            ModeSwitcherButton.ForeColor = Color.DimGray;
            ModeSwitcherButton.HoverBackColor = Color.FromArgb(230, 230, 230);
            ModeSwitcherButton.Image = Properties.Resources.black_and_white_24px;
            ModeSwitcherButton.InactiveBackColor = Color.White;
            ModeSwitcherButton.InactiveForeColor = Color.DimGray;
            ModeSwitcherButton.IsActive = false;
            ModeSwitcherButton.Location = new Point(8, 596);
            ModeSwitcherButton.Margin = new Padding(8);
            ModeSwitcherButton.MouseDownColor = Color.FromArgb(210, 210, 210);
            ModeSwitcherButton.Name = "ModeSwitcherButton";
            ModeSwitcherButton.Size = new Size(74, 68);
            ModeSwitcherButton.TabIndex = 1;
            ModeSwitcherButton.Text = "Gece Modu";
            ModeSwitcherButton.TextAlign = ContentAlignment.BottomCenter;
            ModeSwitcherButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(90, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1174, 681);
            panel1.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1264, 681);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "İSKİ - İSKİ BAKANLIK KABİNİ SİSTEMİ";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private NavigationBarButton HomePageButton;
        private Panel panel1;
        private NavigationBarButton SimulationPageButton;
        private NavigationBarButton CalibrationPageButton;
        private NavigationBarButton MailPageButton;
        private NavigationBarButton ReportingPageButton;
        private NavigationBarButton SettingsPageButton;
        private NavigationBarButton ModeSwitcherButton;
    }
}