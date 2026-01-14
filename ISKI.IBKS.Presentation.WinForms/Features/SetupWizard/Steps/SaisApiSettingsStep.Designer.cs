namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

partial class SaisApiSettingsStep
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
            RowCount = 4,
            Padding = new Padding(20)
        };
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        // API URL
        var lblUrl = new Label { Text = "SAIS API Adresi:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtApiUrl = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblUrl, 0, 0);
        mainPanel.Controls.Add(_txtApiUrl, 1, 0);

        // Username
        var lblUser = new Label { Text = "Kullanıcı Adı:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtUserName = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
        mainPanel.Controls.Add(lblUser, 0, 1);
        mainPanel.Controls.Add(_txtUserName, 1, 1);

        // Password
        var lblPass = new Label { Text = "Şifre:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        _txtPassword = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), UseSystemPasswordChar = true };
        mainPanel.Controls.Add(lblPass, 0, 2);
        mainPanel.Controls.Add(_txtPassword, 1, 2);

        // Note
        var lblNote = new Label 
        { 
            Text = "NOT: Şifre SAIS sisteminden alınan 2xMD5 hashlenmiş şifredir.",
            Dock = DockStyle.Top,
            ForeColor = Color.Gray,
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            Padding = new Padding(0, 10, 0, 0)
        };
        mainPanel.Controls.Add(lblNote, 0, 3);
        mainPanel.SetColumnSpan(lblNote, 2);

        this.Controls.Add(mainPanel);
        this.Dock = DockStyle.Fill;
    }

    private TextBox _txtApiUrl;
    private TextBox _txtUserName;
    private TextBox _txtPassword;
}
