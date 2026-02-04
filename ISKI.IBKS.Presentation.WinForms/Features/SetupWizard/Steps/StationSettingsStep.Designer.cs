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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        mainPanel = new TableLayoutPanel();
        lblId = new Label();
        _txtStationId = new TextBox();
        lblName = new Label();
        _txtStationName = new TextBox();
        lblHost = new Label();
        _txtLocalApiHost = new TextBox();
        lblPort = new Label();
        _txtLocalApiPort = new TextBox();
        lblUser = new Label();
        _txtLocalApiUser = new TextBox();
        lblPass = new Label();
        _txtLocalApiPassword = new TextBox();
        mainPanel.SuspendLayout();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.ColumnCount = 2;
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        mainPanel.Controls.Add(lblId, 0, 0);
        mainPanel.Controls.Add(_txtStationId, 1, 0);
        mainPanel.Controls.Add(lblName, 0, 1);
        mainPanel.Controls.Add(_txtStationName, 1, 1);
        mainPanel.Controls.Add(lblHost, 0, 2);
        mainPanel.Controls.Add(_txtLocalApiHost, 1, 2);
        mainPanel.Controls.Add(lblPort, 0, 3);
        mainPanel.Controls.Add(_txtLocalApiPort, 1, 3);
        mainPanel.Controls.Add(lblUser, 0, 4);
        mainPanel.Controls.Add(_txtLocalApiUser, 1, 4);
        mainPanel.Controls.Add(lblPass, 0, 5);
        mainPanel.Controls.Add(_txtLocalApiPassword, 1, 5);
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.Location = new Point(0, 0);
        mainPanel.Name = "mainPanel";
        mainPanel.Padding = new Padding(20);
        mainPanel.RowCount = 7;
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        mainPanel.Size = new Size(500, 350);
        mainPanel.TabIndex = 0;
        // 
        // lblId
        // 
        lblId.Dock = DockStyle.Fill;
        lblId.Location = new Point(23, 20);
        lblId.Name = "lblId";
        lblId.Size = new Size(155, 40);
        lblId.TabIndex = 0;
        lblId.Text = "İstasyon ID (GUID):";
        lblId.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtStationId
        // 
        _txtStationId.Dock = DockStyle.Fill;
        _txtStationId.Font = new Font("Segoe UI", 10F);
        _txtStationId.Location = new Point(184, 23);
        _txtStationId.Name = "_txtStationId";
        _txtStationId.Size = new Size(293, 25);
        _txtStationId.TabIndex = 1;
        // 
        // lblName
        // 
        lblName.Dock = DockStyle.Fill;
        lblName.Location = new Point(23, 60);
        lblName.Name = "lblName";
        lblName.Size = new Size(155, 40);
        lblName.TabIndex = 2;
        lblName.Text = "İstasyon Adı:";
        lblName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtStationName
        // 
        _txtStationName.Dock = DockStyle.Fill;
        _txtStationName.Font = new Font("Segoe UI", 10F);
        _txtStationName.Location = new Point(184, 63);
        _txtStationName.Name = "_txtStationName";
        _txtStationName.Size = new Size(293, 25);
        _txtStationName.TabIndex = 3;
        // 
        // lblHost
        // 
        lblHost.Dock = DockStyle.Fill;
        lblHost.Location = new Point(23, 100);
        lblHost.Name = "lblHost";
        lblHost.Size = new Size(155, 40);
        lblHost.TabIndex = 4;
        lblHost.Text = "Local API Host/IP:";
        lblHost.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtLocalApiHost
        // 
        _txtLocalApiHost.Dock = DockStyle.Fill;
        _txtLocalApiHost.Font = new Font("Segoe UI", 10F);
        _txtLocalApiHost.Location = new Point(184, 103);
        _txtLocalApiHost.Name = "_txtLocalApiHost";
        _txtLocalApiHost.Size = new Size(293, 25);
        _txtLocalApiHost.TabIndex = 5;
        // 
        // lblPort
        // 
        lblPort.Dock = DockStyle.Fill;
        lblPort.Location = new Point(23, 140);
        lblPort.Name = "lblPort";
        lblPort.Size = new Size(155, 40);
        lblPort.TabIndex = 6;
        lblPort.Text = "Local API Port:";
        lblPort.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtLocalApiPort
        // 
        _txtLocalApiPort.Dock = DockStyle.Fill;
        _txtLocalApiPort.Font = new Font("Segoe UI", 10F);
        _txtLocalApiPort.Location = new Point(184, 143);
        _txtLocalApiPort.Name = "_txtLocalApiPort";
        _txtLocalApiPort.Size = new Size(293, 25);
        _txtLocalApiPort.TabIndex = 7;
        // 
        // lblUser
        // 
        lblUser.Dock = DockStyle.Fill;
        lblUser.Location = new Point(23, 180);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(155, 40);
        lblUser.TabIndex = 8;
        lblUser.Text = "Local API Kullanıcı:";
        lblUser.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtLocalApiUser
        // 
        _txtLocalApiUser.Dock = DockStyle.Fill;
        _txtLocalApiUser.Font = new Font("Segoe UI", 10F);
        _txtLocalApiUser.Location = new Point(184, 183);
        _txtLocalApiUser.Name = "_txtLocalApiUser";
        _txtLocalApiUser.Size = new Size(293, 25);
        _txtLocalApiUser.TabIndex = 9;
        // 
        // lblPass
        // 
        lblPass.Dock = DockStyle.Fill;
        lblPass.Location = new Point(23, 220);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(155, 40);
        lblPass.TabIndex = 10;
        lblPass.Text = "Local API Şifre:";
        lblPass.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtLocalApiPassword
        // 
        _txtLocalApiPassword.Dock = DockStyle.Fill;
        _txtLocalApiPassword.Font = new Font("Segoe UI", 10F);
        _txtLocalApiPassword.Location = new Point(184, 223);
        _txtLocalApiPassword.Name = "_txtLocalApiPassword";
        _txtLocalApiPassword.Size = new Size(293, 25);
        _txtLocalApiPassword.TabIndex = 11;
        _txtLocalApiPassword.UseSystemPasswordChar = true;
        // 
        // StationSettingsStep
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(mainPanel);
        Name = "StationSettingsStep";
        Size = new Size(500, 350);
        mainPanel.ResumeLayout(false);
        mainPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label lblId;
    private System.Windows.Forms.TextBox _txtStationId;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox _txtStationName;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.TextBox _txtLocalApiHost;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.TextBox _txtLocalApiPort;
    private System.Windows.Forms.Label lblUser;
    private System.Windows.Forms.TextBox _txtLocalApiUser;
    private System.Windows.Forms.Label lblPass;
    private System.Windows.Forms.TextBox _txtLocalApiPassword;
}
