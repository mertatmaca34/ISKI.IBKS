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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        mainPanel = new TableLayoutPanel();
        lblHost = new Label();
        _txtSmtpHost = new TextBox();
        lblPort = new Label();
        _numSmtpPort = new NumericUpDown();
        lblUser = new Label();
        _txtSmtpUser = new TextBox();
        lblPass = new Label();
        _txtSmtpPassword = new TextBox();
        lblSsl = new Label();
        _chkSsl = new CheckBox();
        _btnTestMail = new Button();
        mainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numSmtpPort).BeginInit();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.ColumnCount = 2;
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        mainPanel.Controls.Add(lblHost, 0, 0);
        mainPanel.Controls.Add(_txtSmtpHost, 1, 0);
        mainPanel.Controls.Add(lblPort, 0, 1);
        mainPanel.Controls.Add(_numSmtpPort, 1, 1);
        mainPanel.Controls.Add(lblUser, 0, 2);
        mainPanel.Controls.Add(_txtSmtpUser, 1, 2);
        mainPanel.Controls.Add(lblPass, 0, 3);
        mainPanel.Controls.Add(_txtSmtpPassword, 1, 3);
        mainPanel.Controls.Add(lblSsl, 0, 4);
        mainPanel.Controls.Add(_chkSsl, 1, 4);
        mainPanel.Controls.Add(_btnTestMail, 1, 5);
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.Location = new Point(0, 0);
        mainPanel.Name = "mainPanel";
        mainPanel.Padding = new Padding(20);
        mainPanel.RowCount = 6;
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.Size = new Size(500, 350);
        mainPanel.TabIndex = 0;
        // 
        // lblHost
        // 
        lblHost.Dock = DockStyle.Fill;
        lblHost.Location = new Point(23, 20);
        lblHost.Name = "lblHost";
        lblHost.Size = new Size(155, 40);
        lblHost.TabIndex = 0;
        lblHost.Text = "SMTP Sunucu:";
        lblHost.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtSmtpHost
        // 
        _txtSmtpHost.Anchor = AnchorStyles.Left;
        _txtSmtpHost.Font = new Font("Segoe UI", 10F);
        _txtSmtpHost.Location = new Point(184, 27);
        _txtSmtpHost.Name = "_txtSmtpHost";
        _txtSmtpHost.Size = new Size(293, 25);
        _txtSmtpHost.TabIndex = 1;
        // 
        // lblPort
        // 
        lblPort.Dock = DockStyle.Fill;
        lblPort.Location = new Point(23, 60);
        lblPort.Name = "lblPort";
        lblPort.Size = new Size(155, 40);
        lblPort.TabIndex = 2;
        lblPort.Text = "Port:";
        lblPort.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numSmtpPort
        // 
        _numSmtpPort.Anchor = AnchorStyles.Left;
        _numSmtpPort.Location = new Point(184, 68);
        _numSmtpPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        _numSmtpPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _numSmtpPort.Name = "_numSmtpPort";
        _numSmtpPort.Size = new Size(293, 23);
        _numSmtpPort.TabIndex = 3;
        _numSmtpPort.Value = new decimal(new int[] { 587, 0, 0, 0 });
        // 
        // lblUser
        // 
        lblUser.Dock = DockStyle.Fill;
        lblUser.Location = new Point(23, 100);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(155, 40);
        lblUser.TabIndex = 4;
        lblUser.Text = "Kullanıcı Adı:";
        lblUser.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtSmtpUser
        // 
        _txtSmtpUser.Anchor = AnchorStyles.Left;
        _txtSmtpUser.Font = new Font("Segoe UI", 10F);
        _txtSmtpUser.Location = new Point(184, 107);
        _txtSmtpUser.Name = "_txtSmtpUser";
        _txtSmtpUser.Size = new Size(293, 25);
        _txtSmtpUser.TabIndex = 5;
        // 
        // lblPass
        // 
        lblPass.Dock = DockStyle.Fill;
        lblPass.Location = new Point(23, 140);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(155, 40);
        lblPass.TabIndex = 6;
        lblPass.Text = "Şifre:";
        lblPass.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtSmtpPassword
        // 
        _txtSmtpPassword.Anchor = AnchorStyles.Left;
        _txtSmtpPassword.Font = new Font("Segoe UI", 10F);
        _txtSmtpPassword.Location = new Point(184, 147);
        _txtSmtpPassword.Name = "_txtSmtpPassword";
        _txtSmtpPassword.Size = new Size(293, 25);
        _txtSmtpPassword.TabIndex = 7;
        _txtSmtpPassword.UseSystemPasswordChar = true;
        // 
        // lblSsl
        // 
        lblSsl.Dock = DockStyle.Fill;
        lblSsl.Location = new Point(23, 180);
        lblSsl.Name = "lblSsl";
        lblSsl.Size = new Size(155, 40);
        lblSsl.TabIndex = 8;
        lblSsl.Text = "SSL/TLS Kullan:";
        lblSsl.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _chkSsl
        // 
        _chkSsl.Checked = true;
        _chkSsl.CheckState = CheckState.Checked;
        _chkSsl.Dock = DockStyle.Fill;
        _chkSsl.Location = new Point(184, 183);
        _chkSsl.Name = "_chkSsl";
        _chkSsl.Size = new Size(293, 34);
        _chkSsl.TabIndex = 9;
        _chkSsl.UseVisualStyleBackColor = true;
        // 
        // _btnTestMail
        // 
        _btnTestMail.Dock = DockStyle.Right;
        _btnTestMail.Location = new Point(297, 223);
        _btnTestMail.Name = "_btnTestMail";
        _btnTestMail.Size = new Size(180, 104);
        _btnTestMail.TabIndex = 10;
        _btnTestMail.Text = "Test Maili Gönder";
        _btnTestMail.UseVisualStyleBackColor = true;
        // 
        // MailSettingsStep
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(mainPanel);
        Name = "MailSettingsStep";
        Size = new Size(500, 350);
        mainPanel.ResumeLayout(false);
        mainPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_numSmtpPort).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.TextBox _txtSmtpHost;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.NumericUpDown _numSmtpPort;
    private System.Windows.Forms.Label lblUser;
    private System.Windows.Forms.TextBox _txtSmtpUser;
    private System.Windows.Forms.Label lblPass;
    private System.Windows.Forms.TextBox _txtSmtpPassword;
    private System.Windows.Forms.Label lblSsl;
    private System.Windows.Forms.CheckBox _chkSsl;
    private System.Windows.Forms.Button _btnTestMail;
}
