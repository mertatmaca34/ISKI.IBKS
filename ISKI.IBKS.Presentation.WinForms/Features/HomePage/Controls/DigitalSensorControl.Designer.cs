namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;

partial class DigitalSensorControl
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
        LabelSensorName = new Label();
        PanelStatusIndicator = new Panel();
        tableLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.White;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(LabelSensorName, 1, 0);
        tableLayoutPanel1.Controls.Add(PanelStatusIndicator, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(1, 1);
        tableLayoutPanel1.Margin = new Padding(1);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(184, 38);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // LabelSensorName
        // 
        LabelSensorName.Anchor = AnchorStyles.None;
        LabelSensorName.AutoSize = true;
        LabelSensorName.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
        LabelSensorName.Location = new Point(89, 10);
        LabelSensorName.Name = "LabelSensorName";
        LabelSensorName.Size = new Size(13, 18);
        LabelSensorName.TabIndex = 3;
        LabelSensorName.Text = "-";
        // 
        // PanelStatusIndicator
        // 
        PanelStatusIndicator.BackColor = Color.Gray;
        PanelStatusIndicator.Dock = DockStyle.Fill;
        PanelStatusIndicator.Location = new Point(0, 0);
        PanelStatusIndicator.Margin = new Padding(0);
        PanelStatusIndicator.Name = "PanelStatusIndicator";
        PanelStatusIndicator.Size = new Size(8, 38);
        PanelStatusIndicator.TabIndex = 0;
        // 
        // DigitalSensorControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(235, 235, 235);
        Controls.Add(tableLayoutPanel1);
        Name = "DigitalSensorControl";
        Padding = new Padding(1);
        Size = new Size(186, 40);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Panel PanelStatusIndicator;
    private Label LabelSensorName;
}
