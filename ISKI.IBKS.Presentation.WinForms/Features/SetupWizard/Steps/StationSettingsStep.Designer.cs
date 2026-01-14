namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

partial class StationSettingsStep
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
            RowCount = 6,
            Padding = new Padding(20)
        };
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        for (int i = 0; i < 6; i++)
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

        // Station ID
        var lblId = new Label { Text = "İstasyon ID (GUID):", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtStationId = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblId, 0, 0);
        mainPanel.Controls.Add(_txtStationId, 1, 0);

        // Station Name
        var lblName = new Label { Text = "İstasyon Adı:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtStationName = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblName, 0, 1);
        mainPanel.Controls.Add(_txtStationName, 1, 1);

        // Local API Host
        var lblHost = new Label { Text = "Local API Host/IP:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtLocalApiHost = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblHost, 0, 2);
        mainPanel.Controls.Add(_txtLocalApiHost, 1, 2);

        // Local API Port
        var lblPort = new Label { Text = "Local API Port:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtLocalApiPort = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblPort, 0, 3);
        mainPanel.Controls.Add(_txtLocalApiPort, 1, 3);

        // Local API User
        var lblUser = new Label { Text = "Local API Kullanıcı:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtLocalApiUser = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblUser, 0, 4);
        mainPanel.Controls.Add(_txtLocalApiUser, 1, 4);

        // Local API Password
        var lblPass = new Label { Text = "Local API Şifre:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtLocalApiPassword = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), UseSystemPasswordChar = true };
        mainPanel.Controls.Add(lblPass, 0, 5);
        mainPanel.Controls.Add(_txtLocalApiPassword, 1, 5);

        this.Controls.Add(mainPanel);
        this.Dock = DockStyle.Fill;
    }

    private TextBox _txtStationId;
    private TextBox _txtStationName;
    private TextBox _txtLocalApiHost;
    private TextBox _txtLocalApiPort;
    private TextBox _txtLocalApiUser;
    private TextBox _txtLocalApiPassword;
}
