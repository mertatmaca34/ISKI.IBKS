namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

partial class PlcSettingsStep
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
        
        // Main layout
        var mainPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 6,
            Padding = new Padding(20)
        };
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // IP
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Rack
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Slot
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Sensör başlık
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Sensör listesi
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Test butonu

        // IP Address
        var lblIp = new Label { Text = "PLC IP Adresi:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtIpAddress = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblIp, 0, 0);
        mainPanel.Controls.Add(_txtIpAddress, 1, 0);

        // Rack
        var lblRack = new Label { Text = "Rack:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numRack = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 0, Maximum = 10, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblRack, 0, 1);
        mainPanel.Controls.Add(_numRack, 1, 1);

        // Slot
        var lblSlot = new Label { Text = "Slot:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numSlot = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 0, Maximum = 10, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblSlot, 0, 2);
        mainPanel.Controls.Add(_numSlot, 1, 2);

        // Sensör başlık
        var lblSensors = new Label { Text = "Kullanılacak Sensörler:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft };
        mainPanel.Controls.Add(lblSensors, 0, 3);
        mainPanel.SetColumnSpan(lblSensors, 2);

        // Sensör listesi
        _sensorList = new CheckedListBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), CheckOnClick = true };
        mainPanel.Controls.Add(_sensorList, 0, 4);
        mainPanel.SetColumnSpan(_sensorList, 2);

        // Test butonu
        _btnTest = new Button { Text = "PLC Bağlantısını Test Et", Dock = DockStyle.Right, Width = 200, Height = 35 };
        mainPanel.Controls.Add(_btnTest, 1, 5);

        this.Controls.Add(mainPanel);
        this.Dock = DockStyle.Fill;
    }

    private TextBox _txtIpAddress;
    private NumericUpDown _numRack;
    private NumericUpDown _numSlot;
    private CheckedListBox _sensorList;
    private Button _btnTest;
}
