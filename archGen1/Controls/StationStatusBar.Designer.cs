namespace ISKI.IBKS.Presentation.WinForms.Controls
{
    partial class StationStatusBar
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
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            LabelConnectionStatus = new Label();
            LabelUpTime = new Label();
            LabelDailyWashRemainingTime = new Label();
            LabelWeeklyWashRemainingTime = new Label();
            LabelSystemTime = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(panel2, 3, 0);
            tableLayoutPanel1.Controls.Add(panel3, 5, 0);
            tableLayoutPanel1.Controls.Add(panel4, 7, 0);
            tableLayoutPanel1.Controls.Add(LabelConnectionStatus, 0, 0);
            tableLayoutPanel1.Controls.Add(LabelUpTime, 2, 0);
            tableLayoutPanel1.Controls.Add(LabelDailyWashRemainingTime, 4, 0);
            tableLayoutPanel1.Controls.Add(LabelWeeklyWashRemainingTime, 6, 0);
            tableLayoutPanel1.Controls.Add(LabelSystemTime, 8, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1142, 28);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = Color.FromArgb(235, 235, 235);
            panel1.Location = new Point(224, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1, 22);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.FromArgb(235, 235, 235);
            panel2.Location = new Point(454, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 22);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.BackColor = Color.FromArgb(235, 235, 235);
            panel3.Location = new Point(684, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(1, 22);
            panel3.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.None;
            panel4.BackColor = Color.FromArgb(235, 235, 235);
            panel4.Location = new Point(914, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(1, 22);
            panel4.TabIndex = 0;
            // 
            // LabelConnectionStatus
            // 
            LabelConnectionStatus.Anchor = AnchorStyles.None;
            LabelConnectionStatus.AutoSize = true;
            LabelConnectionStatus.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelConnectionStatus.Location = new Point(110, 7);
            LabelConnectionStatus.Name = "LabelConnectionStatus";
            LabelConnectionStatus.Size = new Size(0, 13);
            LabelConnectionStatus.TabIndex = 1;
            // 
            // LabelUpTime
            // 
            LabelUpTime.Anchor = AnchorStyles.None;
            LabelUpTime.AutoSize = true;
            LabelUpTime.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelUpTime.Location = new Point(340, 7);
            LabelUpTime.Name = "LabelUpTime";
            LabelUpTime.Size = new Size(0, 13);
            LabelUpTime.TabIndex = 1;
            // 
            // LabelDailyWashRemainingTime
            // 
            LabelDailyWashRemainingTime.Anchor = AnchorStyles.None;
            LabelDailyWashRemainingTime.AutoSize = true;
            LabelDailyWashRemainingTime.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelDailyWashRemainingTime.Location = new Point(570, 7);
            LabelDailyWashRemainingTime.Name = "LabelDailyWashRemainingTime";
            LabelDailyWashRemainingTime.Size = new Size(0, 13);
            LabelDailyWashRemainingTime.TabIndex = 1;
            // 
            // LabelWeeklyWashRemainingTime
            // 
            LabelWeeklyWashRemainingTime.Anchor = AnchorStyles.None;
            LabelWeeklyWashRemainingTime.AutoSize = true;
            LabelWeeklyWashRemainingTime.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelWeeklyWashRemainingTime.Location = new Point(800, 7);
            LabelWeeklyWashRemainingTime.Name = "LabelWeeklyWashRemainingTime";
            LabelWeeklyWashRemainingTime.Size = new Size(0, 13);
            LabelWeeklyWashRemainingTime.TabIndex = 1;
            // 
            // LabelSystemTime
            // 
            LabelSystemTime.Anchor = AnchorStyles.None;
            LabelSystemTime.AutoSize = true;
            LabelSystemTime.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelSystemTime.Location = new Point(1031, 7);
            LabelSystemTime.Name = "LabelSystemTime";
            LabelSystemTime.Size = new Size(0, 13);
            LabelSystemTime.TabIndex = 1;
            // 
            // StationStatusBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(235, 235, 235);
            Controls.Add(tableLayoutPanel1);
            Name = "StationStatusBar";
            Padding = new Padding(1);
            Size = new Size(1144, 30);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label LabelConnectionStatus;
        private Label LabelUpTime;
        private Label LabelDailyWashRemainingTime;
        private Label LabelWeeklyWashRemainingTime;
        private Label LabelSystemTime;
    }
}
