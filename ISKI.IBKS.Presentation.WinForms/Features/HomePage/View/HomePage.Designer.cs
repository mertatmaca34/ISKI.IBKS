namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.View
{
    partial class HomePage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region BileÅŸen TasarÄ±mcÄ±sÄ± Ã¼retimi kod

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            TableLayoutPanelDigitalSensors = new TableLayoutPanel();
            DigitalSensorsHeaderControl = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.SensorHeaderControl();
            TableLayoutPanelAnalogSensors = new TableLayoutPanel();
            AnalogSensorsHeaderControl = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.SensorHeaderControl();
            StationStatusBarControl = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.StatusBarControl();
            TableLayoutPanelHealthSummaryCard = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            TableLayoutPanelDigitalSensors.SuspendLayout();
            TableLayoutPanelAnalogSensors.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(TableLayoutPanelDigitalSensors, 1, 0);
            tableLayoutPanel1.Controls.Add(TableLayoutPanelAnalogSensors, 0, 0);
            tableLayoutPanel1.Controls.Add(StationStatusBarControl, 0, 2);
            tableLayoutPanel1.Controls.Add(TableLayoutPanelHealthSummaryCard, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(5, 5);
            tableLayoutPanel1.Margin = new Padding(1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.43F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 66.57F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(1160, 667);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // TableLayoutPanelDigitalSensors
            // 
            TableLayoutPanelDigitalSensors.ColumnCount = 3;
            TableLayoutPanelDigitalSensors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelDigitalSensors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            TableLayoutPanelDigitalSensors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            TableLayoutPanelDigitalSensors.Controls.Add(DigitalSensorsHeaderControl, 0, 0);
            TableLayoutPanelDigitalSensors.Dock = DockStyle.Fill;
            TableLayoutPanelDigitalSensors.Location = new Point(583, 3);
            TableLayoutPanelDigitalSensors.Name = "TableLayoutPanelDigitalSensors";
            TableLayoutPanelDigitalSensors.RowCount = 4;
            TableLayoutPanelDigitalSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            TableLayoutPanelDigitalSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelDigitalSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelDigitalSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelDigitalSensors.Size = new Size(574, 205);
            TableLayoutPanelDigitalSensors.TabIndex = 1;
            // 
            // DigitalSensorsHeaderControl
            // 
            DigitalSensorsHeaderControl.BackColor = Color.FromArgb(235, 235, 235);
            TableLayoutPanelDigitalSensors.SetColumnSpan(DigitalSensorsHeaderControl, 3);
            DigitalSensorsHeaderControl.Dock = DockStyle.Fill;
            DigitalSensorsHeaderControl.HeaderTitle = "Dijital Sensörler";
            DigitalSensorsHeaderControl.HeaderTitle2 = "";
            DigitalSensorsHeaderControl.HeaderTitle3 = "";
            DigitalSensorsHeaderControl.Location = new Point(3, 3);
            DigitalSensorsHeaderControl.Name = "DigitalSensorsHeaderControl";
            DigitalSensorsHeaderControl.Padding = new Padding(1);
            DigitalSensorsHeaderControl.Size = new Size(568, 63);
            DigitalSensorsHeaderControl.TabIndex = 0;
            // 
            // TableLayoutPanelAnalogSensors
            // 
            TableLayoutPanelAnalogSensors.ColumnCount = 1;
            TableLayoutPanelAnalogSensors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelAnalogSensors.Controls.Add(AnalogSensorsHeaderControl, 0, 0);
            TableLayoutPanelAnalogSensors.Dock = DockStyle.Fill;
            TableLayoutPanelAnalogSensors.Location = new Point(3, 3);
            TableLayoutPanelAnalogSensors.Name = "TableLayoutPanelAnalogSensors";
            TableLayoutPanelAnalogSensors.RowCount = 17;
            tableLayoutPanel1.SetRowSpan(TableLayoutPanelAnalogSensors, 2);
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            TableLayoutPanelAnalogSensors.Size = new Size(574, 625);
            TableLayoutPanelAnalogSensors.TabIndex = 0;
            // 
            // AnalogSensorsHeaderControl
            // 
            AnalogSensorsHeaderControl.BackColor = Color.FromArgb(235, 235, 235);
            AnalogSensorsHeaderControl.Dock = DockStyle.Fill;
            AnalogSensorsHeaderControl.HeaderTitle = "Analog Sensörler";
            AnalogSensorsHeaderControl.HeaderTitle2 = "Anlık Veri";
            AnalogSensorsHeaderControl.HeaderTitle3 = "Saatlik Veri";
            AnalogSensorsHeaderControl.Location = new Point(3, 3);
            AnalogSensorsHeaderControl.Name = "AnalogSensorsHeaderControl";
            AnalogSensorsHeaderControl.Padding = new Padding(1);
            AnalogSensorsHeaderControl.Size = new Size(568, 63);
            AnalogSensorsHeaderControl.TabIndex = 1;
            // 
            // StationStatusBarControl
            // 
            StationStatusBarControl.BackColor = Color.FromArgb(235, 235, 235);
            tableLayoutPanel1.SetColumnSpan(StationStatusBarControl, 2);
            StationStatusBarControl.DailyWashRemainingTime = TimeSpan.Parse("00:00:00");
            StationStatusBarControl.Dock = DockStyle.Fill;
            StationStatusBarControl.IsConnected = false;
            StationStatusBarControl.Location = new Point(3, 634);
            StationStatusBarControl.Name = "StationStatusBarControl";
            StationStatusBarControl.Padding = new Padding(1);
            StationStatusBarControl.Size = new Size(1154, 30);
            StationStatusBarControl.SystemTime = new DateTime(0L);
            StationStatusBarControl.TabIndex = 3;
            StationStatusBarControl.UpTime = TimeSpan.Parse("00:00:00");
            StationStatusBarControl.WeeklyWashRemainingTime = TimeSpan.Parse("00:00:00");
            // 
            // TableLayoutPanelHealthSummaryCard
            // 
            TableLayoutPanelHealthSummaryCard.ColumnCount = 1;
            TableLayoutPanelHealthSummaryCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelHealthSummaryCard.Dock = DockStyle.Fill;
            TableLayoutPanelHealthSummaryCard.Location = new Point(583, 214);
            TableLayoutPanelHealthSummaryCard.Name = "TableLayoutPanelHealthSummaryCard";
            TableLayoutPanelHealthSummaryCard.RowCount = 8;
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            TableLayoutPanelHealthSummaryCard.Size = new Size(574, 414);
            TableLayoutPanelHealthSummaryCard.TabIndex = 4;
            // 
            // HomePage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(tableLayoutPanel1);
            Name = "HomePage";
            Padding = new Padding(5);
            Size = new Size(1170, 677);
            tableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanelDigitalSensors.ResumeLayout(false);
            TableLayoutPanelAnalogSensors.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel TableLayoutPanelDigitalSensors;
        private Controls.SensorHeaderControl DigitalSensorsHeaderControl;
        private Controls.SensorHeaderControl AnalogSensorsHeaderControl;
        private TableLayoutPanel TableLayoutPanelAnalogSensors;
        private Controls.StatusBarControl StationStatusBarControl;
        private TableLayoutPanel TableLayoutPanelHealthSummaryCard;
    }
}

