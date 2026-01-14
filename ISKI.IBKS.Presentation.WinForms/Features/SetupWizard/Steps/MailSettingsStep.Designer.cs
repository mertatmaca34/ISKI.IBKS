namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

partial class MailSettingsStep
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

        // SMTP Host
        var lblHost = new Label { Text = "SMTP Sunucu:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtSmtpHost = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblHost, 0, 0);
        mainPanel.Controls.Add(_txtSmtpHost, 1, 0);

        // SMTP Port
        var lblPort = new Label { Text = "Port:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _numSmtpPort = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 1, Maximum = 65535, Value = 587 };
        mainPanel.Controls.Add(lblPort, 0, 1);
        mainPanel.Controls.Add(_numSmtpPort, 1, 1);

        // SMTP Username
        var lblUser = new Label { Text = "Kullanıcı Adı:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtSmtpUser = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblUser, 0, 2);
        mainPanel.Controls.Add(_txtSmtpUser, 1, 2);

        // SMTP Password
        var lblPass = new Label { Text = "Şifre:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtSmtpPassword = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), UseSystemPasswordChar = true };
        mainPanel.Controls.Add(lblPass, 0, 3);
        mainPanel.Controls.Add(_txtSmtpPassword, 1, 3);

        // SSL
        var lblSsl = new Label { Text = "SSL/TLS Kullan:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _chkSsl = new CheckBox { Dock = DockStyle.Fill, Checked = true };
        mainPanel.Controls.Add(lblSsl, 0, 4);
        mainPanel.Controls.Add(_chkSsl, 1, 4);

        // Test Button
        _btnTestMail = new Button { Text = "Test Maili Gönder", Dock = DockStyle.Right, Width = 180, Height = 35 };
        mainPanel.Controls.Add(_btnTestMail, 1, 5);

        this.Controls.Add(mainPanel);
        this.Dock = DockStyle.Fill;
    }

    private TextBox _txtSmtpHost;
    private NumericUpDown _numSmtpPort;
    private TextBox _txtSmtpUser;
    private TextBox _txtSmtpPassword;
    private CheckBox _chkSsl;
    private Button _btnTestMail;
}
