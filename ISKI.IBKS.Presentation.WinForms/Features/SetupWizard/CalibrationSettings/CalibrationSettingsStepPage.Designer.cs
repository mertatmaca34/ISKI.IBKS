namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.CalibrationSettings;

partial class CalibrationSettingsStepPage
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

    private void InitializeComponent()
    {
        mainPanel = new TableLayoutPanel();
        LabelPh = new Label();
        LabelPhZero = new Label();
        NumericUpDownPhZeroRef = new NumericUpDown();
        LabelPhSpan = new Label();
        NumericUpDownPhSpanRef = new NumericUpDown();
        LabelPhDur = new Label();
        NumericUpDownPhDuration = new NumericUpDown();
        LabelIletkenlik = new Label();
        LabelIletkenlikZero = new Label();
        NumericUpDownIletkenlikZeroRef = new NumericUpDown();
        LabelIletkenlikSpan = new Label();
        NumericUpDownIletkenlikSpanRef = new NumericUpDown();
        NumericUpDownIletkenlikDuration = new NumericUpDown();
        LabelIletkenlikDur = new Label();
        mainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownPhZeroRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownPhSpanRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownPhDuration).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownIletkenlikZeroRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownIletkenlikSpanRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownIletkenlikDuration).BeginInit();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.ColumnCount = 2;
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        mainPanel.Controls.Add(LabelPh, 0, 0);
        mainPanel.Controls.Add(LabelPhZero, 0, 1);
        mainPanel.Controls.Add(NumericUpDownPhZeroRef, 1, 1);
        mainPanel.Controls.Add(LabelPhSpan, 0, 2);
        mainPanel.Controls.Add(NumericUpDownPhSpanRef, 1, 2);
        mainPanel.Controls.Add(LabelPhDur, 0, 3);
        mainPanel.Controls.Add(NumericUpDownPhDuration, 1, 3);
        mainPanel.Controls.Add(LabelIletkenlik, 0, 4);
        mainPanel.Controls.Add(LabelIletkenlikZero, 0, 5);
        mainPanel.Controls.Add(NumericUpDownIletkenlikZeroRef, 1, 5);
        mainPanel.Controls.Add(LabelIletkenlikSpan, 0, 6);
        mainPanel.Controls.Add(NumericUpDownIletkenlikSpanRef, 1, 6);
        mainPanel.Controls.Add(NumericUpDownIletkenlikDuration, 1, 7);
        mainPanel.Controls.Add(LabelIletkenlikDur, 0, 7);
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.Location = new Point(0, 0);
        mainPanel.Name = "mainPanel";
        mainPanel.Padding = new Padding(20);
        mainPanel.RowCount = 9;
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle());
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        mainPanel.Size = new Size(600, 450);
        mainPanel.TabIndex = 0;
        // 
        // LabelPh
        // 
        LabelPh.AutoSize = true;
        mainPanel.SetColumnSpan(LabelPh, 2);
        LabelPh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        LabelPh.Location = new Point(23, 20);
        LabelPh.Margin = new Padding(3, 0, 3, 10);
        LabelPh.Name = "LabelPh";
        LabelPh.Size = new Size(174, 20);
        LabelPh.TabIndex = 0;
        LabelPh.Text = "pH Kalibrasyon Ayarları";
        // 
        // LabelPhZero
        // 
        LabelPhZero.Anchor = AnchorStyles.Left;
        LabelPhZero.AutoSize = true;
        LabelPhZero.Location = new Point(23, 57);
        LabelPhZero.Name = "LabelPhZero";
        LabelPhZero.Size = new Size(119, 15);
        LabelPhZero.TabIndex = 1;
        LabelPhZero.Text = "Zero Referans Değeri:";
        // 
        // NumericUpDownPhZeroRef
        // 
        NumericUpDownPhZeroRef.DecimalPlaces = 2;
        NumericUpDownPhZeroRef.Dock = DockStyle.Fill;
        NumericUpDownPhZeroRef.Location = new Point(247, 53);
        NumericUpDownPhZeroRef.Maximum = new decimal(new int[] { 14, 0, 0, 0 });
        NumericUpDownPhZeroRef.Name = "NumericUpDownPhZeroRef";
        NumericUpDownPhZeroRef.Size = new Size(330, 23);
        NumericUpDownPhZeroRef.TabIndex = 2;
        NumericUpDownPhZeroRef.Value = new decimal(new int[] { 7, 0, 0, 0 });
        // 
        // LabelPhSpan
        // 
        LabelPhSpan.Anchor = AnchorStyles.Left;
        LabelPhSpan.AutoSize = true;
        LabelPhSpan.Location = new Point(23, 86);
        LabelPhSpan.Name = "LabelPhSpan";
        LabelPhSpan.Size = new Size(121, 15);
        LabelPhSpan.TabIndex = 3;
        LabelPhSpan.Text = "Span Referans Değeri:";
        // 
        // NumericUpDownPhSpanRef
        // 
        NumericUpDownPhSpanRef.DecimalPlaces = 2;
        NumericUpDownPhSpanRef.Dock = DockStyle.Fill;
        NumericUpDownPhSpanRef.Location = new Point(247, 82);
        NumericUpDownPhSpanRef.Maximum = new decimal(new int[] { 14, 0, 0, 0 });
        NumericUpDownPhSpanRef.Name = "NumericUpDownPhSpanRef";
        NumericUpDownPhSpanRef.Size = new Size(330, 23);
        NumericUpDownPhSpanRef.TabIndex = 4;
        NumericUpDownPhSpanRef.Value = new decimal(new int[] { 4, 0, 0, 0 });
        // 
        // LabelPhDur
        // 
        LabelPhDur.Anchor = AnchorStyles.Left;
        LabelPhDur.AutoSize = true;
        LabelPhDur.Location = new Point(23, 115);
        LabelPhDur.Name = "LabelPhDur";
        LabelPhDur.Size = new Size(105, 15);
        LabelPhDur.TabIndex = 5;
        LabelPhDur.Text = "Kalibrasyon Süresi;";
        // 
        // NumericUpDownPhDuration
        // 
        NumericUpDownPhDuration.Dock = DockStyle.Fill;
        NumericUpDownPhDuration.Location = new Point(247, 111);
        NumericUpDownPhDuration.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        NumericUpDownPhDuration.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        NumericUpDownPhDuration.Name = "NumericUpDownPhDuration";
        NumericUpDownPhDuration.Size = new Size(330, 23);
        NumericUpDownPhDuration.TabIndex = 6;
        NumericUpDownPhDuration.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // LabelIletkenlik
        // 
        LabelIletkenlik.AutoSize = true;
        mainPanel.SetColumnSpan(LabelIletkenlik, 2);
        LabelIletkenlik.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        LabelIletkenlik.Location = new Point(23, 147);
        LabelIletkenlik.Margin = new Padding(3, 10, 3, 10);
        LabelIletkenlik.Name = "LabelIletkenlik";
        LabelIletkenlik.Size = new Size(218, 20);
        LabelIletkenlik.TabIndex = 7;
        LabelIletkenlik.Text = "İletkenlik Kalibrasyon Ayarları";
        // 
        // LabelIletkenlikZero
        // 
        LabelIletkenlikZero.Anchor = AnchorStyles.Left;
        LabelIletkenlikZero.AutoSize = true;
        LabelIletkenlikZero.Location = new Point(23, 184);
        LabelIletkenlikZero.Name = "LabelIletkenlikZero";
        LabelIletkenlikZero.Size = new Size(119, 15);
        LabelIletkenlikZero.TabIndex = 8;
        LabelIletkenlikZero.Text = "Zero Referans Değeri:";
        // 
        // NumericUpDownIletkenlikZeroRef
        // 
        NumericUpDownIletkenlikZeroRef.DecimalPlaces = 2;
        NumericUpDownIletkenlikZeroRef.Dock = DockStyle.Fill;
        NumericUpDownIletkenlikZeroRef.Location = new Point(247, 180);
        NumericUpDownIletkenlikZeroRef.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        NumericUpDownIletkenlikZeroRef.Name = "NumericUpDownIletkenlikZeroRef";
        NumericUpDownIletkenlikZeroRef.Size = new Size(330, 23);
        NumericUpDownIletkenlikZeroRef.TabIndex = 9;
        // 
        // LabelIletkenlikSpan
        // 
        LabelIletkenlikSpan.Anchor = AnchorStyles.Left;
        LabelIletkenlikSpan.AutoSize = true;
        LabelIletkenlikSpan.Location = new Point(23, 213);
        LabelIletkenlikSpan.Name = "LabelIletkenlikSpan";
        LabelIletkenlikSpan.Size = new Size(121, 15);
        LabelIletkenlikSpan.TabIndex = 10;
        LabelIletkenlikSpan.Text = "Span Referans Değeri:";
        // 
        // NumericUpDownIletkenlikSpanRef
        // 
        NumericUpDownIletkenlikSpanRef.DecimalPlaces = 2;
        NumericUpDownIletkenlikSpanRef.Dock = DockStyle.Fill;
        NumericUpDownIletkenlikSpanRef.Location = new Point(247, 209);
        NumericUpDownIletkenlikSpanRef.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        NumericUpDownIletkenlikSpanRef.Name = "NumericUpDownIletkenlikSpanRef";
        NumericUpDownIletkenlikSpanRef.Size = new Size(330, 23);
        NumericUpDownIletkenlikSpanRef.TabIndex = 11;
        NumericUpDownIletkenlikSpanRef.Value = new decimal(new int[] { 1413, 0, 0, 0 });
        // 
        // NumericUpDownIletkenlikDuration
        // 
        NumericUpDownIletkenlikDuration.Dock = DockStyle.Fill;
        NumericUpDownIletkenlikDuration.Location = new Point(247, 238);
        NumericUpDownIletkenlikDuration.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        NumericUpDownIletkenlikDuration.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        NumericUpDownIletkenlikDuration.Name = "NumericUpDownIletkenlikDuration";
        NumericUpDownIletkenlikDuration.Size = new Size(330, 23);
        NumericUpDownIletkenlikDuration.TabIndex = 13;
        NumericUpDownIletkenlikDuration.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // LabelIletkenlikDur
        // 
        LabelIletkenlikDur.Anchor = AnchorStyles.Left;
        LabelIletkenlikDur.AutoSize = true;
        LabelIletkenlikDur.Location = new Point(23, 242);
        LabelIletkenlikDur.Name = "LabelIletkenlikDur";
        LabelIletkenlikDur.Size = new Size(105, 15);
        LabelIletkenlikDur.TabIndex = 12;
        LabelIletkenlikDur.Text = "Kalibrasyon Süresi:";
        // 
        // CalibrationSettingsStepPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(mainPanel);
        Name = "CalibrationSettingsStepPage";
        Size = new Size(600, 450);
        mainPanel.ResumeLayout(false);
        mainPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownPhZeroRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownPhSpanRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownPhDuration).EndInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownIletkenlikZeroRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownIletkenlikSpanRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownIletkenlikDuration).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label LabelPh;
    private System.Windows.Forms.Label LabelPhZero;
    private System.Windows.Forms.NumericUpDown NumericUpDownPhZeroRef;
    private System.Windows.Forms.Label LabelPhSpan;
    private System.Windows.Forms.NumericUpDown NumericUpDownPhSpanRef;
    private System.Windows.Forms.Label LabelPhDur;
    private System.Windows.Forms.NumericUpDown NumericUpDownPhDuration;
    private System.Windows.Forms.Label LabelIletkenlik;
    private System.Windows.Forms.Label LabelIletkenlikZero;
    private System.Windows.Forms.NumericUpDown NumericUpDownIletkenlikZeroRef;
    private System.Windows.Forms.Label LabelIletkenlikSpan;
    private System.Windows.Forms.NumericUpDown NumericUpDownIletkenlikSpanRef;
    private System.Windows.Forms.Label LabelIletkenlikDur;
    private System.Windows.Forms.NumericUpDown NumericUpDownIletkenlikDuration;
}

