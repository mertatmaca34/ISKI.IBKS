namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage
{
    partial class HomePage
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            TableLayoutPanelDigitalSensors = new TableLayoutPanel();
            analogSensorHeaderControl2 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.AnalogSensorHeaderControl();
            TableLayoutPanelAnalogSensors = new TableLayoutPanel();
            analogSensorHeaderControl1 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.AnalogSensorHeaderControl();
            stationStatusBar1 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.StationStatusBar();
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
            tableLayoutPanel1.Controls.Add(stationStatusBar1, 0, 2);
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
            TableLayoutPanelDigitalSensors.Controls.Add(analogSensorHeaderControl2, 0, 0);
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
            // analogSensorHeaderControl2
            // 
            analogSensorHeaderControl2.BackColor = Color.FromArgb(235, 235, 235);
            TableLayoutPanelDigitalSensors.SetColumnSpan(analogSensorHeaderControl2, 3);
            analogSensorHeaderControl2.Dock = DockStyle.Fill;
            analogSensorHeaderControl2.HeaderTitle = "Dijital Sensörler";
            analogSensorHeaderControl2.HeaderTitle2 = "";
            analogSensorHeaderControl2.HeaderTitle3 = "";
            analogSensorHeaderControl2.Location = new Point(3, 3);
            analogSensorHeaderControl2.Name = "analogSensorHeaderControl2";
            analogSensorHeaderControl2.Padding = new Padding(1);
            analogSensorHeaderControl2.Size = new Size(568, 63);
            analogSensorHeaderControl2.TabIndex = 0;
            // 
            // TableLayoutPanelAnalogSensors
            // 
            TableLayoutPanelAnalogSensors.ColumnCount = 1;
            TableLayoutPanelAnalogSensors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelAnalogSensors.Controls.Add(analogSensorHeaderControl1, 0, 0);
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
            // analogSensorHeaderControl1
            // 
            analogSensorHeaderControl1.BackColor = Color.FromArgb(235, 235, 235);
            analogSensorHeaderControl1.Dock = DockStyle.Fill;
            analogSensorHeaderControl1.HeaderTitle = "Analog Sensörler";
            analogSensorHeaderControl1.HeaderTitle2 = "Anlık Veri";
            analogSensorHeaderControl1.HeaderTitle3 = "Saatlik Veri";
            analogSensorHeaderControl1.Location = new Point(3, 3);
            analogSensorHeaderControl1.Name = "analogSensorHeaderControl1";
            analogSensorHeaderControl1.Padding = new Padding(1);
            analogSensorHeaderControl1.Size = new Size(568, 63);
            analogSensorHeaderControl1.TabIndex = 1;
            // 
            // stationStatusBar1
            // 
            stationStatusBar1.BackColor = Color.FromArgb(235, 235, 235);
            tableLayoutPanel1.SetColumnSpan(stationStatusBar1, 2);
            stationStatusBar1.DailyWashRemainingTime = TimeSpan.Parse("00:00:00");
            stationStatusBar1.Dock = DockStyle.Fill;
            stationStatusBar1.IsConnected = false;
            stationStatusBar1.Location = new Point(3, 634);
            stationStatusBar1.Name = "stationStatusBar1";
            stationStatusBar1.Padding = new Padding(1);
            stationStatusBar1.Size = new Size(1154, 30);
            stationStatusBar1.SystemTime = new DateTime(0L);
            stationStatusBar1.TabIndex = 3;
            stationStatusBar1.UpTime = TimeSpan.Parse("00:00:00");
            stationStatusBar1.WeeklyWashRemainingTime = TimeSpan.Parse("00:00:00");
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
        private Controls.AnalogSensorHeaderControl analogSensorHeaderControl2;
        private Controls.AnalogSensorHeaderControl analogSensorHeaderControl1;
        private TableLayoutPanel TableLayoutPanelAnalogSensors;
        private Controls.StationStatusBar stationStatusBar1;
        private TableLayoutPanel TableLayoutPanelHealthSummaryCard;
    }
}
