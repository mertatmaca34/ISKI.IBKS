namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.MailSettings;

partial class MailSettingsStepPage
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
        MainPanel = new TableLayoutPanel();
        LabelHost = new Label();
        TextBoxSmtpHost = new TextBox();
        LabelPort = new Label();
        NumericUpDownSmtpPort = new NumericUpDown();
        LabelUser = new Label();
        TextBoxUser = new TextBox();
        LabelPassword = new Label();
        TextBoxSmtpPassword = new TextBox();
        LabelSsl = new Label();
        CheckBoxSsl = new CheckBox();
        LabelReceiver = new Label();
        TextBoxReceiverMailAddress = new TextBox();
        ButtonSendTestMail = new Button();
        MainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownSmtpPort).BeginInit();
        SuspendLayout();
        // 
        // MainPanel
        // 
        MainPanel.ColumnCount = 2;
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        MainPanel.Controls.Add(LabelHost, 0, 0);
        MainPanel.Controls.Add(TextBoxSmtpHost, 1, 0);
        MainPanel.Controls.Add(LabelPort, 0, 1);
        MainPanel.Controls.Add(NumericUpDownSmtpPort, 1, 1);
        MainPanel.Controls.Add(LabelUser, 0, 2);
        MainPanel.Controls.Add(TextBoxUser, 1, 2);
        MainPanel.Controls.Add(LabelPassword, 0, 3);
        MainPanel.Controls.Add(TextBoxSmtpPassword, 1, 3);
        MainPanel.Controls.Add(LabelSsl, 0, 4);
        MainPanel.Controls.Add(CheckBoxSsl, 1, 4);
        MainPanel.Controls.Add(LabelReceiver, 0, 5);
        MainPanel.Controls.Add(TextBoxReceiverMailAddress, 1, 5);
        MainPanel.Controls.Add(ButtonSendTestMail, 1, 6);
        MainPanel.Dock = DockStyle.Fill;
        MainPanel.Location = new Point(0, 0);
        MainPanel.Name = "MainPanel";
        MainPanel.Padding = new Padding(20);
        MainPanel.RowCount = 8;
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        MainPanel.Size = new Size(500, 378);
        MainPanel.TabIndex = 0;
        // 
        // LabelHost
        // 
        LabelHost.Anchor = AnchorStyles.Left;
        LabelHost.AutoSize = true;
        LabelHost.Location = new Point(23, 32);
        LabelHost.Name = "LabelHost";
        LabelHost.Size = new Size(83, 15);
        LabelHost.TabIndex = 0;
        LabelHost.Text = "SMTP Sunucu:";
        // 
        // TextBoxSmtpHost
        // 
        TextBoxSmtpHost.Anchor = AnchorStyles.Left;
        TextBoxSmtpHost.Font = new Font("Segoe UI", 10F);
        TextBoxSmtpHost.Location = new Point(184, 27);
        TextBoxSmtpHost.Name = "TextBoxSmtpHost";
        TextBoxSmtpHost.Size = new Size(293, 25);
        TextBoxSmtpHost.TabIndex = 1;
        // 
        // LabelPort
        // 
        LabelPort.Anchor = AnchorStyles.Left;
        LabelPort.AutoSize = true;
        LabelPort.Location = new Point(23, 72);
        LabelPort.Name = "LabelPort";
        LabelPort.Size = new Size(32, 15);
        LabelPort.TabIndex = 2;
        LabelPort.Text = "Port:";
        // 
        // NumericUpDownSmtpPort
        // 
        NumericUpDownSmtpPort.Anchor = AnchorStyles.Left;
        NumericUpDownSmtpPort.Location = new Point(184, 68);
        NumericUpDownSmtpPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        NumericUpDownSmtpPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        NumericUpDownSmtpPort.Name = "NumericUpDownSmtpPort";
        NumericUpDownSmtpPort.Size = new Size(293, 23);
        NumericUpDownSmtpPort.TabIndex = 3;
        NumericUpDownSmtpPort.Value = new decimal(new int[] { 25, 0, 0, 0 });
        // 
        // LabelUser
        // 
        LabelUser.Anchor = AnchorStyles.Left;
        LabelUser.AutoSize = true;
        LabelUser.Location = new Point(23, 112);
        LabelUser.Name = "LabelUser";
        LabelUser.Size = new Size(76, 15);
        LabelUser.TabIndex = 4;
        LabelUser.Text = "Kullanıcı Adı:";
        // 
        // TextBoxUser
        // 
        TextBoxUser.Anchor = AnchorStyles.Left;
        TextBoxUser.Font = new Font("Segoe UI", 10F);
        TextBoxUser.Location = new Point(184, 107);
        TextBoxUser.Name = "TextBoxUser";
        TextBoxUser.Size = new Size(293, 25);
        TextBoxUser.TabIndex = 5;
        // 
        // LabelPassword
        // 
        LabelPassword.Anchor = AnchorStyles.Left;
        LabelPassword.AutoSize = true;
        LabelPassword.Location = new Point(23, 152);
        LabelPassword.Name = "LabelPassword";
        LabelPassword.Size = new Size(33, 15);
        LabelPassword.TabIndex = 6;
        LabelPassword.Text = "Şifre:";
        // 
        // TextBoxSmtpPassword
        // 
        TextBoxSmtpPassword.Anchor = AnchorStyles.Left;
        TextBoxSmtpPassword.Font = new Font("Segoe UI", 10F);
        TextBoxSmtpPassword.Location = new Point(184, 147);
        TextBoxSmtpPassword.Name = "TextBoxSmtpPassword";
        TextBoxSmtpPassword.Size = new Size(293, 25);
        TextBoxSmtpPassword.TabIndex = 7;
        TextBoxSmtpPassword.UseSystemPasswordChar = true;
        // 
        // LabelSsl
        // 
        LabelSsl.Anchor = AnchorStyles.Left;
        LabelSsl.AutoSize = true;
        LabelSsl.Location = new Point(23, 192);
        LabelSsl.Name = "LabelSsl";
        LabelSsl.Size = new Size(87, 15);
        LabelSsl.TabIndex = 8;
        LabelSsl.Text = "SSL/TLS Kullan:";
        // 
        // CheckBoxSsl
        // 
        CheckBoxSsl.Anchor = AnchorStyles.Left;
        CheckBoxSsl.AutoSize = true;
        CheckBoxSsl.Checked = true;
        CheckBoxSsl.CheckState = CheckState.Checked;
        CheckBoxSsl.Location = new Point(184, 193);
        CheckBoxSsl.Name = "CheckBoxSsl";
        CheckBoxSsl.Size = new Size(15, 14);
        CheckBoxSsl.TabIndex = 9;
        // 
        // LabelReceiver
        // 
        LabelReceiver.Anchor = AnchorStyles.Left;
        LabelReceiver.AutoSize = true;
        LabelReceiver.Location = new Point(23, 232);
        LabelReceiver.Name = "LabelReceiver";
        LabelReceiver.Size = new Size(128, 15);
        LabelReceiver.TabIndex = 10;
        LabelReceiver.Text = "Test Maili Alıcı E-posta:";
        // 
        // TextBoxReceiverMailAddress
        // 
        TextBoxReceiverMailAddress.Anchor = AnchorStyles.Left;
        TextBoxReceiverMailAddress.Font = new Font("Segoe UI", 10F);
        TextBoxReceiverMailAddress.Location = new Point(184, 227);
        TextBoxReceiverMailAddress.Name = "TextBoxReceiverMailAddress";
        TextBoxReceiverMailAddress.Size = new Size(293, 25);
        TextBoxReceiverMailAddress.TabIndex = 11;
        // 
        // ButtonSendTestMail
        // 
        ButtonSendTestMail.Anchor = AnchorStyles.Right;
        ButtonSendTestMail.Location = new Point(297, 268);
        ButtonSendTestMail.Name = "ButtonSendTestMail";
        ButtonSendTestMail.Size = new Size(180, 34);
        ButtonSendTestMail.TabIndex = 12;
        ButtonSendTestMail.Text = "Test Maili Gönder";
        ButtonSendTestMail.UseVisualStyleBackColor = true;
        // 
        // MailSettingsStepPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(MainPanel);
        Name = "MailSettingsStepPage";
        Size = new Size(500, 378);
        MainPanel.ResumeLayout(false);
        MainPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)NumericUpDownSmtpPort).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TableLayoutPanel MainPanel;
    private System.Windows.Forms.Label LabelHost;
    private System.Windows.Forms.TextBox TextBoxSmtpHost;
    private System.Windows.Forms.Label LabelPort;
    private System.Windows.Forms.NumericUpDown NumericUpDownSmtpPort;
    private System.Windows.Forms.Label LabelUser;
    private System.Windows.Forms.TextBox TextBoxUser;
    private System.Windows.Forms.Label LabelPassword;
    private System.Windows.Forms.TextBox TextBoxSmtpPassword;
    private System.Windows.Forms.Label LabelSsl;
    private System.Windows.Forms.CheckBox CheckBoxSsl;
    private System.Windows.Forms.Label LabelReceiver;
    private System.Windows.Forms.TextBox TextBoxReceiverMailAddress;
    private System.Windows.Forms.Button ButtonSendTestMail;
}
