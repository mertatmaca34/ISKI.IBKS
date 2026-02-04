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

    #region Component Designer generated code

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
        lblUrl.Dock = DockStyle.Fill;
        lblUrl.Location = new Point(23, 20);
        lblUrl.Name = "lblUrl";
        lblUrl.Size = new Size(155, 40);
        lblUrl.TabIndex = 0;
        lblUrl.Text = "SAIS API Adresi:";
        lblUrl.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtApiUrl
        // 
        _txtApiUrl.Anchor = AnchorStyles.Left;
        _txtApiUrl.Font = new Font("Segoe UI", 10F);
        _txtApiUrl.Location = new Point(184, 27);
        _txtApiUrl.Name = "_txtApiUrl";
        _txtApiUrl.Size = new Size(281, 25);
        _txtApiUrl.TabIndex = 1;
        // 
        // lblUser
        // 
        lblUser.Dock = DockStyle.Fill;
        lblUser.Location = new Point(23, 60);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(155, 40);
        lblUser.TabIndex = 2;
        lblUser.Text = "Kullanıcı Adı:";
        lblUser.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtUserName
        // 
        _txtUserName.Anchor = AnchorStyles.Left;
        _txtUserName.Font = new Font("Segoe UI", 10F);
        _txtUserName.Location = new Point(184, 67);
        _txtUserName.Name = "_txtUserName";
        _txtUserName.Size = new Size(281, 25);
        _txtUserName.TabIndex = 3;
        // 
        // lblPass
        // 
        lblPass.Dock = DockStyle.Fill;
        lblPass.Location = new Point(23, 100);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(155, 40);
        lblPass.TabIndex = 4;
        lblPass.Text = "Şifre:";
        lblPass.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtPassword
        // 
        _txtPassword.Anchor = AnchorStyles.Left;
        _txtPassword.Font = new Font("Segoe UI", 10F);
        _txtPassword.Location = new Point(184, 107);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.Size = new Size(281, 25);
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

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label lblUrl;
    private System.Windows.Forms.TextBox _txtApiUrl;
    private System.Windows.Forms.Label lblUser;
    private System.Windows.Forms.TextBox _txtUserName;
    private System.Windows.Forms.Label lblPass;
    private System.Windows.Forms.TextBox _txtPassword;
}
