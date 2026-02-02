namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.SaisApiSettings;

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
        mainPanel = new TableLayoutPanel();
        lblUrl = new Label();
        _txtApiUrl = new TextBox();
        lblUser = new Label();
        _txtUserName = new TextBox();
        lblPass = new Label();
        _txtPassword = new TextBox();
        mainPanel.SuspendLayout();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.ColumnCount = 2;
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        mainPanel.Controls.Add(lblUrl, 0, 0);
        mainPanel.Controls.Add(_txtApiUrl, 1, 0);
        mainPanel.Controls.Add(lblUser, 0, 1);
        mainPanel.Controls.Add(_txtUserName, 1, 1);
        mainPanel.Controls.Add(lblPass, 0, 2);
        mainPanel.Controls.Add(_txtPassword, 1, 2);
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.Location = new Point(0, 0);
        mainPanel.Name = "mainPanel";
        mainPanel.Padding = new Padding(20);
        mainPanel.RowCount = 4;
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainPanel.Size = new Size(500, 300);
        mainPanel.TabIndex = 0;
        // 
        // lblUrl
        // 
        lblUrl.Anchor = AnchorStyles.Left;
        lblUrl.AutoSize = true;
        lblUrl.Location = new Point(23, 32);
        lblUrl.Name = "lblUrl";
        lblUrl.Size = new Size(90, 15);
        lblUrl.TabIndex = 0;
        lblUrl.Text = "SAIS API Adresi:";
        // 
        // _txtApiUrl
        // 
        _txtApiUrl.Dock = DockStyle.Fill;
        _txtApiUrl.Font = new Font("Segoe UI", 10F);
        _txtApiUrl.Location = new Point(184, 23);
        _txtApiUrl.Name = "_txtApiUrl";
        _txtApiUrl.Size = new Size(293, 25);
        _txtApiUrl.TabIndex = 1;
        // 
        // lblUser
        // 
        lblUser.Anchor = AnchorStyles.Left;
        lblUser.AutoSize = true;
        lblUser.Location = new Point(23, 72);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(76, 15);
        lblUser.TabIndex = 2;
        lblUser.Text = "Kullanıcı Adı:";
        // 
        // _txtUserName
        // 
        _txtUserName.Dock = DockStyle.Fill;
        _txtUserName.Font = new Font("Segoe UI", 10F);
        _txtUserName.Location = new Point(184, 63);
        _txtUserName.Name = "_txtUserName";
        _txtUserName.Size = new Size(293, 25);
        _txtUserName.TabIndex = 3;
        // 
        // lblPass
        // 
        lblPass.Anchor = AnchorStyles.Left;
        lblPass.AutoSize = true;
        lblPass.Location = new Point(23, 112);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(33, 15);
        lblPass.TabIndex = 4;
        lblPass.Text = "Şifre:";
        // 
        // _txtPassword
        // 
        _txtPassword.Dock = DockStyle.Fill;
        _txtPassword.Font = new Font("Segoe UI", 10F);
        _txtPassword.Location = new Point(184, 103);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.Size = new Size(293, 25);
        _txtPassword.TabIndex = 5;
        _txtPassword.UseSystemPasswordChar = true;
        // 
        // SaisApiSettingsStep
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(mainPanel);
        Name = "SaisApiSettingsStep";
        Size = new Size(500, 300);
        mainPanel.ResumeLayout(false);
        mainPanel.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label lblUrl;
    private System.Windows.Forms.TextBox _txtApiUrl;
    private System.Windows.Forms.Label lblUser;
    private System.Windows.Forms.TextBox _txtUserName;
    private System.Windows.Forms.Label lblPass;
    private System.Windows.Forms.TextBox _txtPassword;
}

