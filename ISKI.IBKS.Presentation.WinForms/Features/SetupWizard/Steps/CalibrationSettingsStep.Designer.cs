namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

partial class CalibrationSettingsStep
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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        mainPanel = new TableLayoutPanel();
        lblPh = new Label();
        lblPhZero = new Label();
        _numPhZeroRef = new NumericUpDown();
        lblPhSpan = new Label();
        _numPhSpanRef = new NumericUpDown();
        lblPhDur = new Label();
        _numPhDuration = new NumericUpDown();
        lblIlet = new Label();
        lblIletZero = new Label();
        _numIletZeroRef = new NumericUpDown();
        lblIletSpan = new Label();
        _numIletSpanRef = new NumericUpDown();
        lblIletDur = new Label();
        _numIletDuration = new NumericUpDown();
        mainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numPhZeroRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numPhSpanRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numPhDuration).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numIletZeroRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numIletSpanRef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numIletDuration).BeginInit();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.ColumnCount = 2;
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        mainPanel.Controls.Add(lblPh, 0, 0);
        mainPanel.Controls.Add(lblPhZero, 0, 1);
        mainPanel.Controls.Add(_numPhZeroRef, 1, 1);
        mainPanel.Controls.Add(lblPhSpan, 0, 2);
        mainPanel.Controls.Add(_numPhSpanRef, 1, 2);
        mainPanel.Controls.Add(lblPhDur, 0, 3);
        mainPanel.Controls.Add(_numPhDuration, 1, 3);
        mainPanel.Controls.Add(lblIlet, 0, 4);
        mainPanel.Controls.Add(lblIletZero, 0, 5);
        mainPanel.Controls.Add(_numIletZeroRef, 1, 5);
        mainPanel.Controls.Add(lblIletSpan, 0, 6);
        mainPanel.Controls.Add(_numIletSpanRef, 1, 6);
        mainPanel.Controls.Add(lblIletDur, 0, 7);
        mainPanel.Controls.Add(_numIletDuration, 1, 7);
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
        mainPanel.Size = new Size(500, 400);
        mainPanel.TabIndex = 0;
        // 
        // lblPh
        // 
        mainPanel.SetColumnSpan(lblPh, 2);
        lblPh.Dock = DockStyle.Fill;
        lblPh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblPh.Location = new Point(23, 20);
        lblPh.Name = "lblPh";
        lblPh.Size = new Size(454, 25);
        lblPh.TabIndex = 0;
        lblPh.Text = "pH Kalibrasyon Ayarları";
        // 
        // lblPhZero
        // 
        lblPhZero.Dock = DockStyle.Fill;
        lblPhZero.Location = new Point(23, 45);
        lblPhZero.Name = "lblPhZero";
        lblPhZero.Size = new Size(178, 29);
        lblPhZero.TabIndex = 1;
        lblPhZero.Text = "Zero Referans Değeri:";
        lblPhZero.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numPhZeroRef
        // 
        _numPhZeroRef.DecimalPlaces = 2;
        _numPhZeroRef.Dock = DockStyle.Fill;
        _numPhZeroRef.Location = new Point(207, 48);
        _numPhZeroRef.Maximum = new decimal(new int[] { 14, 0, 0, 0 });
        _numPhZeroRef.Name = "_numPhZeroRef";
        _numPhZeroRef.Size = new Size(270, 23);
        _numPhZeroRef.TabIndex = 2;
        _numPhZeroRef.Value = new decimal(new int[] { 7, 0, 0, 0 });
        // 
        // lblPhSpan
        // 
        lblPhSpan.Dock = DockStyle.Fill;
        lblPhSpan.Location = new Point(23, 74);
        lblPhSpan.Name = "lblPhSpan";
        lblPhSpan.Size = new Size(178, 29);
        lblPhSpan.TabIndex = 3;
        lblPhSpan.Text = "Span Referans Değeri:";
        lblPhSpan.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numPhSpanRef
        // 
        _numPhSpanRef.DecimalPlaces = 2;
        _numPhSpanRef.Dock = DockStyle.Fill;
        _numPhSpanRef.Location = new Point(207, 77);
        _numPhSpanRef.Maximum = new decimal(new int[] { 14, 0, 0, 0 });
        _numPhSpanRef.Name = "_numPhSpanRef";
        _numPhSpanRef.Size = new Size(270, 23);
        _numPhSpanRef.TabIndex = 4;
        _numPhSpanRef.Value = new decimal(new int[] { 4, 0, 0, 0 });
        // 
        // lblPhDur
        // 
        lblPhDur.Dock = DockStyle.Fill;
        lblPhDur.Location = new Point(23, 103);
        lblPhDur.Name = "lblPhDur";
        lblPhDur.Size = new Size(178, 29);
        lblPhDur.TabIndex = 5;
        lblPhDur.Text = "Kalibrasyon Süresi (sn):";
        lblPhDur.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numPhDuration
        // 
        _numPhDuration.Dock = DockStyle.Fill;
        _numPhDuration.Location = new Point(207, 106);
        _numPhDuration.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        _numPhDuration.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        _numPhDuration.Name = "_numPhDuration";
        _numPhDuration.Size = new Size(270, 23);
        _numPhDuration.TabIndex = 6;
        _numPhDuration.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // lblIlet
        // 
        mainPanel.SetColumnSpan(lblIlet, 2);
        lblIlet.Dock = DockStyle.Fill;
        lblIlet.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblIlet.Location = new Point(23, 132);
        lblIlet.Name = "lblIlet";
        lblIlet.Padding = new Padding(0, 15, 0, 0);
        lblIlet.Size = new Size(454, 40);
        lblIlet.TabIndex = 7;
        lblIlet.Text = "İletkenlik Kalibrasyon Ayarları";
        // 
        // lblIletZero
        // 
        lblIletZero.Dock = DockStyle.Fill;
        lblIletZero.Location = new Point(23, 172);
        lblIletZero.Name = "lblIletZero";
        lblIletZero.Size = new Size(178, 29);
        lblIletZero.TabIndex = 8;
        lblIletZero.Text = "Zero Referans Değeri:";
        lblIletZero.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numIletZeroRef
        // 
        _numIletZeroRef.DecimalPlaces = 2;
        _numIletZeroRef.Dock = DockStyle.Fill;
        _numIletZeroRef.Location = new Point(207, 175);
        _numIletZeroRef.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        _numIletZeroRef.Name = "_numIletZeroRef";
        _numIletZeroRef.Size = new Size(270, 23);
        _numIletZeroRef.TabIndex = 9;
        // 
        // lblIletSpan
        // 
        lblIletSpan.Dock = DockStyle.Fill;
        lblIletSpan.Location = new Point(23, 201);
        lblIletSpan.Name = "lblIletSpan";
        lblIletSpan.Size = new Size(178, 29);
        lblIletSpan.TabIndex = 10;
        lblIletSpan.Text = "Span Referans Değeri:";
        lblIletSpan.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numIletSpanRef
        // 
        _numIletSpanRef.DecimalPlaces = 2;
        _numIletSpanRef.Dock = DockStyle.Fill;
        _numIletSpanRef.Location = new Point(207, 204);
        _numIletSpanRef.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        _numIletSpanRef.Name = "_numIletSpanRef";
        _numIletSpanRef.Size = new Size(270, 23);
        _numIletSpanRef.TabIndex = 11;
        _numIletSpanRef.Value = new decimal(new int[] { 1413, 0, 0, 0 });
        // 
        // lblIletDur
        // 
        lblIletDur.Dock = DockStyle.Fill;
        lblIletDur.Location = new Point(23, 230);
        lblIletDur.Name = "lblIletDur";
        lblIletDur.Size = new Size(178, 29);
        lblIletDur.TabIndex = 12;
        lblIletDur.Text = "Kalibrasyon Süresi (sn):";
        lblIletDur.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numIletDuration
        // 
        _numIletDuration.Dock = DockStyle.Fill;
        _numIletDuration.Location = new Point(207, 233);
        _numIletDuration.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        _numIletDuration.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        _numIletDuration.Name = "_numIletDuration";
        _numIletDuration.Size = new Size(270, 23);
        _numIletDuration.TabIndex = 13;
        _numIletDuration.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // CalibrationSettingsStep
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(mainPanel);
        Name = "CalibrationSettingsStep";
        Size = new Size(500, 400);
        mainPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numPhZeroRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numPhSpanRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numPhDuration).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numIletZeroRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numIletSpanRef).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numIletDuration).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label lblPh;
    private System.Windows.Forms.Label lblPhZero;
    private System.Windows.Forms.NumericUpDown _numPhZeroRef;
    private System.Windows.Forms.Label lblPhSpan;
    private System.Windows.Forms.NumericUpDown _numPhSpanRef;
    private System.Windows.Forms.Label lblPhDur;
    private System.Windows.Forms.NumericUpDown _numPhDuration;
    private System.Windows.Forms.Label lblIlet;
    private System.Windows.Forms.Label lblIletZero;
    private System.Windows.Forms.NumericUpDown _numIletZeroRef;
    private System.Windows.Forms.Label lblIletSpan;
    private System.Windows.Forms.NumericUpDown _numIletSpanRef;
    private System.Windows.Forms.Label lblIletDur;
    private System.Windows.Forms.NumericUpDown _numIletDuration;
}
