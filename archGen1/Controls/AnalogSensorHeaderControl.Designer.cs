namespace archGen1.Controls
{
    partial class AnalogSensorHeaderControl
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
            TableLayoutPanelBg = new TableLayoutPanel();
            LabelHeaderTitle2 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            LabelHeaderTitle3 = new Label();
            LabelHeaderTitle = new Label();
            TableLayoutPanelBg.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelBg
            // 
            TableLayoutPanelBg.BackColor = Color.White;
            TableLayoutPanelBg.ColumnCount = 6;
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelBg.Controls.Add(LabelHeaderTitle2, 3, 0);
            TableLayoutPanelBg.Controls.Add(panel1, 0, 0);
            TableLayoutPanelBg.Controls.Add(panel2, 2, 0);
            TableLayoutPanelBg.Controls.Add(panel3, 4, 0);
            TableLayoutPanelBg.Controls.Add(LabelHeaderTitle3, 5, 0);
            TableLayoutPanelBg.Controls.Add(LabelHeaderTitle, 1, 0);
            TableLayoutPanelBg.Dock = DockStyle.Fill;
            TableLayoutPanelBg.Location = new Point(1, 1);
            TableLayoutPanelBg.Name = "TableLayoutPanelBg";
            TableLayoutPanelBg.RowCount = 1;
            TableLayoutPanelBg.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelBg.Size = new Size(577, 61);
            TableLayoutPanelBg.TabIndex = 0;
            // 
            // LabelHeaderTitle2
            // 
            LabelHeaderTitle2.Anchor = AnchorStyles.None;
            LabelHeaderTitle2.AutoSize = true;
            LabelHeaderTitle2.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            LabelHeaderTitle2.Location = new Point(286, 21);
            LabelHeaderTitle2.Name = "LabelHeaderTitle2";
            LabelHeaderTitle2.Size = new Size(13, 18);
            LabelHeaderTitle2.TabIndex = 2;
            LabelHeaderTitle2.Text = "-";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 131, 200);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(8, 61);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.FromArgb(235, 235, 235);
            panel2.Location = new Point(195, 12);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 37);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.BackColor = Color.FromArgb(235, 235, 235);
            panel3.Location = new Point(388, 12);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1, 37);
            panel3.TabIndex = 0;
            // 
            // LabelHeaderTitle3
            // 
            LabelHeaderTitle3.Anchor = AnchorStyles.None;
            LabelHeaderTitle3.AutoSize = true;
            LabelHeaderTitle3.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelHeaderTitle3.Location = new Point(479, 21);
            LabelHeaderTitle3.Name = "LabelHeaderTitle3";
            LabelHeaderTitle3.Size = new Size(13, 18);
            LabelHeaderTitle3.TabIndex = 2;
            LabelHeaderTitle3.Text = "-";
            // 
            // LabelHeaderTitle
            // 
            LabelHeaderTitle.Anchor = AnchorStyles.None;
            LabelHeaderTitle.AutoSize = true;
            LabelHeaderTitle.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            LabelHeaderTitle.Location = new Point(93, 21);
            LabelHeaderTitle.Name = "LabelHeaderTitle";
            LabelHeaderTitle.Size = new Size(13, 18);
            LabelHeaderTitle.TabIndex = 2;
            LabelHeaderTitle.Text = "-";
            // 
            // AnalogSensorHeaderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(235, 235, 235);
            Controls.Add(TableLayoutPanelBg);
            Name = "AnalogSensorHeaderControl";
            Padding = new Padding(1);
            Size = new Size(579, 63);
            TableLayoutPanelBg.ResumeLayout(false);
            TableLayoutPanelBg.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel TableLayoutPanelBg;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label LabelHeaderTitle2;
        private Label LabelHeaderTitle3;
        private Label LabelHeaderTitle;
    }
}
