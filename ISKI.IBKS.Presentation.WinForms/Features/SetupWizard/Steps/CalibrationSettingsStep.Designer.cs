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

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        
        var mainPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 8,
            Padding = new Padding(20),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        
        // Add AutoSize rows
        for (int i = 0; i < 8; i++)
        {
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        // pH Section Header
        var lblPh = new Label { Text = "pH Kalibrasyon Ayarları", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Dock = DockStyle.Fill };
        mainPanel.Controls.Add(lblPh, 0, 0);
        mainPanel.SetColumnSpan(lblPh, 2);

        // pH Zero Ref
        var lblPhZero = new Label { Text = "Zero Referans Değeri:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numPhZeroRef = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Minimum = 0, Maximum = 14, Value = 7.0M };
        mainPanel.Controls.Add(lblPhZero, 0, 1);
        mainPanel.Controls.Add(_numPhZeroRef, 1, 1);

        // pH Span Ref
        var lblPhSpan = new Label { Text = "Span Referans Değeri:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numPhSpanRef = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Minimum = 0, Maximum = 14, Value = 4.0M };
        mainPanel.Controls.Add(lblPhSpan, 0, 2);
        mainPanel.Controls.Add(_numPhSpanRef, 1, 2);

        // pH Duration
        var lblPhDur = new Label { Text = "Kalibrasyon Süresi (sn):", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numPhDuration = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 10, Maximum = 300, Value = 60 };
        mainPanel.Controls.Add(lblPhDur, 0, 3);
        mainPanel.Controls.Add(_numPhDuration, 1, 3);

        // Iletkenlik Section Header
        var lblIlet = new Label { Text = "İletkenlik Kalibrasyon Ayarları", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Dock = DockStyle.Fill, Padding = new Padding(0, 15, 0, 0) };
        mainPanel.Controls.Add(lblIlet, 0, 4);
        mainPanel.SetColumnSpan(lblIlet, 2);

        // Iletkenlik Zero Ref
        var lblIletZero = new Label { Text = "Zero Referans Değeri:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numIletZeroRef = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Minimum = 0, Maximum = 10000, Value = 0M };
        mainPanel.Controls.Add(lblIletZero, 0, 5);
        mainPanel.Controls.Add(_numIletZeroRef, 1, 5);

        // Iletkenlik Span Ref
        var lblIletSpan = new Label { Text = "Span Referans Değeri:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numIletSpanRef = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Minimum = 0, Maximum = 10000, Value = 1413M };
        mainPanel.Controls.Add(lblIletSpan, 0, 6);
        mainPanel.Controls.Add(_numIletSpanRef, 1, 6);

        // Iletkenlik Duration
        var lblIletDur = new Label { Text = "Kalibrasyon Süresi (sn):", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numIletDuration = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 10, Maximum = 300, Value = 60 };
        mainPanel.Controls.Add(lblIletDur, 0, 7);
        mainPanel.Controls.Add(_numIletDuration, 1, 7);

        this.Controls.Add(mainPanel);
        this.Dock = DockStyle.Fill;
    }

    private NumericUpDown _numPhZeroRef;
    private NumericUpDown _numPhSpanRef;
    private NumericUpDown _numPhDuration;
    private NumericUpDown _numIletZeroRef;
    private NumericUpDown _numIletSpanRef;
    private NumericUpDown _numIletDuration;
}
