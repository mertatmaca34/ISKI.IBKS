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
            tableLayoutPanel3 = new TableLayoutPanel();
            analogSensorHeaderControl2 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.AnalogSensorHeaderControl();
            digitalSensorControl1 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl2 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl3 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl4 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl5 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl6 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl7 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl8 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            digitalSensorControl9 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.DigitalSensorControl();
            tableLayoutPanel2 = new TableLayoutPanel();
            analogSensorHeaderControl1 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.AnalogSensorHeaderControl();
            TableLayoutPanelAnalogSensors = new TableLayoutPanel();
            stationParameterStatusControl1 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.StationParameterStatusControl();
            stationStatusBar1 = new ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls.StationStatusBar();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(stationParameterStatusControl1, 1, 1);
            tableLayoutPanel1.Controls.Add(stationStatusBar1, 0, 2);
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
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Controls.Add(analogSensorHeaderControl2, 0, 0);
            tableLayoutPanel3.Controls.Add(digitalSensorControl1, 0, 1);
            tableLayoutPanel3.Controls.Add(digitalSensorControl2, 0, 2);
            tableLayoutPanel3.Controls.Add(digitalSensorControl3, 0, 3);
            tableLayoutPanel3.Controls.Add(digitalSensorControl4, 1, 3);
            tableLayoutPanel3.Controls.Add(digitalSensorControl5, 1, 2);
            tableLayoutPanel3.Controls.Add(digitalSensorControl6, 1, 1);
            tableLayoutPanel3.Controls.Add(digitalSensorControl7, 2, 1);
            tableLayoutPanel3.Controls.Add(digitalSensorControl8, 2, 2);
            tableLayoutPanel3.Controls.Add(digitalSensorControl9, 2, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(583, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Size = new Size(574, 205);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // analogSensorHeaderControl2
            // 
            analogSensorHeaderControl2.BackColor = Color.FromArgb(235, 235, 235);
            tableLayoutPanel3.SetColumnSpan(analogSensorHeaderControl2, 3);
            analogSensorHeaderControl2.Dock = DockStyle.Fill;
            analogSensorHeaderControl2.HeaderTitle = "-";
            analogSensorHeaderControl2.HeaderTitle2 = "-";
            analogSensorHeaderControl2.HeaderTitle3 = "-";
            analogSensorHeaderControl2.Location = new Point(3, 3);
            analogSensorHeaderControl2.Name = "analogSensorHeaderControl2";
            analogSensorHeaderControl2.Padding = new Padding(1);
            analogSensorHeaderControl2.Size = new Size(568, 63);
            analogSensorHeaderControl2.TabIndex = 0;
            // 
            // digitalSensorControl1
            // 
            digitalSensorControl1.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl1.Dock = DockStyle.Fill;
            digitalSensorControl1.Location = new Point(3, 72);
            digitalSensorControl1.Name = "digitalSensorControl1";
            digitalSensorControl1.Padding = new Padding(1);
            digitalSensorControl1.SensorName = "-";
            digitalSensorControl1.Size = new Size(185, 39);
            digitalSensorControl1.StatusIndicator = Color.Gray;
            digitalSensorControl1.TabIndex = 1;
            // 
            // digitalSensorControl2
            // 
            digitalSensorControl2.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl2.Dock = DockStyle.Fill;
            digitalSensorControl2.Location = new Point(3, 117);
            digitalSensorControl2.Name = "digitalSensorControl2";
            digitalSensorControl2.Padding = new Padding(1);
            digitalSensorControl2.SensorName = "-";
            digitalSensorControl2.Size = new Size(185, 39);
            digitalSensorControl2.StatusIndicator = Color.Gray;
            digitalSensorControl2.TabIndex = 1;
            // 
            // digitalSensorControl3
            // 
            digitalSensorControl3.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl3.Dock = DockStyle.Fill;
            digitalSensorControl3.Location = new Point(3, 162);
            digitalSensorControl3.Name = "digitalSensorControl3";
            digitalSensorControl3.Padding = new Padding(1);
            digitalSensorControl3.SensorName = "-";
            digitalSensorControl3.Size = new Size(185, 40);
            digitalSensorControl3.StatusIndicator = Color.Gray;
            digitalSensorControl3.TabIndex = 1;
            // 
            // digitalSensorControl4
            // 
            digitalSensorControl4.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl4.Dock = DockStyle.Fill;
            digitalSensorControl4.Location = new Point(194, 162);
            digitalSensorControl4.Name = "digitalSensorControl4";
            digitalSensorControl4.Padding = new Padding(1);
            digitalSensorControl4.SensorName = "-";
            digitalSensorControl4.Size = new Size(185, 40);
            digitalSensorControl4.StatusIndicator = Color.Gray;
            digitalSensorControl4.TabIndex = 1;
            // 
            // digitalSensorControl5
            // 
            digitalSensorControl5.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl5.Dock = DockStyle.Fill;
            digitalSensorControl5.Location = new Point(194, 117);
            digitalSensorControl5.Name = "digitalSensorControl5";
            digitalSensorControl5.Padding = new Padding(1);
            digitalSensorControl5.SensorName = "-";
            digitalSensorControl5.Size = new Size(185, 39);
            digitalSensorControl5.StatusIndicator = Color.Gray;
            digitalSensorControl5.TabIndex = 1;
            // 
            // digitalSensorControl6
            // 
            digitalSensorControl6.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl6.Dock = DockStyle.Fill;
            digitalSensorControl6.Location = new Point(194, 72);
            digitalSensorControl6.Name = "digitalSensorControl6";
            digitalSensorControl6.Padding = new Padding(1);
            digitalSensorControl6.SensorName = "-";
            digitalSensorControl6.Size = new Size(185, 39);
            digitalSensorControl6.StatusIndicator = Color.Gray;
            digitalSensorControl6.TabIndex = 1;
            // 
            // digitalSensorControl7
            // 
            digitalSensorControl7.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl7.Dock = DockStyle.Fill;
            digitalSensorControl7.Location = new Point(385, 72);
            digitalSensorControl7.Name = "digitalSensorControl7";
            digitalSensorControl7.Padding = new Padding(1);
            digitalSensorControl7.SensorName = "-";
            digitalSensorControl7.Size = new Size(186, 39);
            digitalSensorControl7.StatusIndicator = Color.Gray;
            digitalSensorControl7.TabIndex = 1;
            // 
            // digitalSensorControl8
            // 
            digitalSensorControl8.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl8.Dock = DockStyle.Fill;
            digitalSensorControl8.Location = new Point(385, 117);
            digitalSensorControl8.Name = "digitalSensorControl8";
            digitalSensorControl8.Padding = new Padding(1);
            digitalSensorControl8.SensorName = "-";
            digitalSensorControl8.Size = new Size(186, 39);
            digitalSensorControl8.StatusIndicator = Color.Gray;
            digitalSensorControl8.TabIndex = 1;
            // 
            // digitalSensorControl9
            // 
            digitalSensorControl9.BackColor = Color.FromArgb(235, 235, 235);
            digitalSensorControl9.Dock = DockStyle.Fill;
            digitalSensorControl9.Location = new Point(385, 162);
            digitalSensorControl9.Name = "digitalSensorControl9";
            digitalSensorControl9.Padding = new Padding(1);
            digitalSensorControl9.SensorName = "-";
            digitalSensorControl9.Size = new Size(186, 40);
            digitalSensorControl9.StatusIndicator = Color.Gray;
            digitalSensorControl9.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(analogSensorHeaderControl1, 0, 0);
            tableLayoutPanel2.Controls.Add(TableLayoutPanelAnalogSensors, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(574, 625);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // analogSensorHeaderControl1
            // 
            analogSensorHeaderControl1.BackColor = Color.FromArgb(235, 235, 235);
            analogSensorHeaderControl1.Dock = DockStyle.Fill;
            analogSensorHeaderControl1.HeaderTitle = "-";
            analogSensorHeaderControl1.HeaderTitle2 = "-";
            analogSensorHeaderControl1.HeaderTitle3 = "-";
            analogSensorHeaderControl1.Location = new Point(3, 3);
            analogSensorHeaderControl1.Name = "analogSensorHeaderControl1";
            analogSensorHeaderControl1.Padding = new Padding(1);
            analogSensorHeaderControl1.Size = new Size(568, 63);
            analogSensorHeaderControl1.TabIndex = 1;
            // 
            // TableLayoutPanelAnalogSensors
            // 
            TableLayoutPanelAnalogSensors.ColumnCount = 1;
            TableLayoutPanelAnalogSensors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelAnalogSensors.Dock = DockStyle.Fill;
            TableLayoutPanelAnalogSensors.Location = new Point(0, 69);
            TableLayoutPanelAnalogSensors.Margin = new Padding(0);
            TableLayoutPanelAnalogSensors.Name = "TableLayoutPanelAnalogSensors";
            TableLayoutPanelAnalogSensors.RowCount = 16;
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            TableLayoutPanelAnalogSensors.Size = new Size(574, 556);
            TableLayoutPanelAnalogSensors.TabIndex = 2;
            // 
            // stationParameterStatusControl1
            // 
            stationParameterStatusControl1.Dock = DockStyle.Fill;
            stationParameterStatusControl1.Location = new Point(583, 214);
            stationParameterStatusControl1.Name = "stationParameterStatusControl1";
            stationParameterStatusControl1.Padding = new Padding(1);
            stationParameterStatusControl1.Size = new Size(574, 414);
            stationParameterStatusControl1.TabIndex = 2;
            // 
            // stationStatusBar1
            // 
            stationStatusBar1.BackColor = Color.FromArgb(235, 235, 235);
            tableLayoutPanel1.SetColumnSpan(stationStatusBar1, 2);
            stationStatusBar1.DailyWashRemainingTime = "G. Yıkamaya Kalan: G. Yıkamaya Kalan: G. Yıkamaya Kalan: G. Yıkamaya Kalan: G. Yıkamaya Kalan: ";
            stationStatusBar1.Dock = DockStyle.Fill;
            stationStatusBar1.IsConnected = "Bağlantı Durumu: Bağlantı Durumu: Bağlantı Durumu: Bağlantı Durumu: Bağlantı Durumu: ";
            stationStatusBar1.Location = new Point(3, 634);
            stationStatusBar1.Name = "stationStatusBar1";
            stationStatusBar1.Padding = new Padding(1);
            stationStatusBar1.Size = new Size(1154, 30);
            stationStatusBar1.SystemTime = "Sistem Saati: Sistem Saati: Sistem Saati: Sistem Saati: Sistem Saati: ";
            stationStatusBar1.TabIndex = 3;
            stationStatusBar1.UpTime = "Bağlantı Süresi: Bağlantı Süresi: Bağlantı Süresi: Bağlantı Süresi: Bağlantı Süresi: ";
            stationStatusBar1.WeeklyWashRemainingTime = "H. Yıkama Kalan: H. Yıkama Kalan: H. Yıkama Kalan: H. Yıkama Kalan: H. Yıkama Kalan: ";
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
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private Controls.AnalogSensorHeaderControl analogSensorHeaderControl2;
        private Controls.DigitalSensorControl digitalSensorControl1;
        private Controls.DigitalSensorControl digitalSensorControl2;
        private Controls.DigitalSensorControl digitalSensorControl3;
        private Controls.DigitalSensorControl digitalSensorControl4;
        private Controls.DigitalSensorControl digitalSensorControl5;
        private Controls.DigitalSensorControl digitalSensorControl6;
        private Controls.DigitalSensorControl digitalSensorControl7;
        private Controls.DigitalSensorControl digitalSensorControl8;
        private Controls.DigitalSensorControl digitalSensorControl9;
        private TableLayoutPanel tableLayoutPanel2;
        private Controls.AnalogSensorHeaderControl analogSensorHeaderControl1;
        private Controls.StationParameterStatusControl stationParameterStatusControl1;
        private Controls.StationStatusBar stationStatusBar1;
        private TableLayoutPanel TableLayoutPanelAnalogSensors;
    }
}
