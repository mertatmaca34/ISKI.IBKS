namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.PlcSettings;

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
        mainPanel = new TableLayoutPanel();
        lblIp = new Label();
        _txtIpAddress = new TextBox();
        lblRack = new Label();
        _numRack = new NumericUpDown();
        lblSlot = new Label();
        _numSlot = new NumericUpDown();
        lblSensors = new Label();
        _sensorList = new CheckedListBox();
        _btnTest = new Button();
        mainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numRack).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSlot).BeginInit();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.ColumnCount = 2;
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        mainPanel.Controls.Add(lblIp, 0, 0);
        mainPanel.Controls.Add(_txtIpAddress, 1, 0);
        mainPanel.Controls.Add(lblRack, 0, 1);
        mainPanel.Controls.Add(_numRack, 1, 1);
        mainPanel.Controls.Add(lblSlot, 0, 2);
        mainPanel.Controls.Add(_numSlot, 1, 2);
        mainPanel.Controls.Add(lblSensors, 0, 3);
        mainPanel.Controls.Add(_sensorList, 0, 4);
        mainPanel.Controls.Add(_btnTest, 1, 5);
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.Location = new Point(0, 0);
        mainPanel.Name = "mainPanel";
        mainPanel.Padding = new Padding(20);
        mainPanel.RowCount = 6;
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        mainPanel.Size = new Size(500, 400);
        mainPanel.TabIndex = 0;
        // 
        // lblIp
        // 
        lblIp.Anchor = AnchorStyles.Left;
        lblIp.AutoSize = true;
        lblIp.Location = new Point(23, 32);
        lblIp.Name = "lblIp";
        lblIp.Size = new Size(80, 15);
        lblIp.TabIndex = 0;
        lblIp.Text = "PLC IP Adresi:";
        // 
        // _txtIpAddress
        // 
        _txtIpAddress.Dock = DockStyle.Fill;
        _txtIpAddress.Font = new Font("Segoe UI", 10F);
        _txtIpAddress.Location = new Point(184, 23);
        _txtIpAddress.Name = "_txtIpAddress";
        _txtIpAddress.Size = new Size(293, 25);
        _txtIpAddress.TabIndex = 1;
        // 
        // lblRack
        // 
        lblRack.Anchor = AnchorStyles.Left;
        lblRack.AutoSize = true;
        lblRack.Location = new Point(23, 72);
        lblRack.Name = "lblRack";
        lblRack.Size = new Size(35, 15);
        lblRack.TabIndex = 2;
        lblRack.Text = "Rack:";
        // 
        // _numRack
        // 
        _numRack.Dock = DockStyle.Fill;
        _numRack.Location = new Point(184, 63);
        _numRack.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numRack.Name = "_numRack";
        _numRack.Size = new Size(293, 23);
        _numRack.TabIndex = 3;
        // 
        // lblSlot
        // 
        lblSlot.Anchor = AnchorStyles.Left;
        lblSlot.AutoSize = true;
        lblSlot.Location = new Point(23, 112);
        lblSlot.Name = "lblSlot";
        lblSlot.Size = new Size(30, 15);
        lblSlot.TabIndex = 4;
        lblSlot.Text = "Slot:";
        // 
        // _numSlot
        // 
        _numSlot.Dock = DockStyle.Fill;
        _numSlot.Location = new Point(184, 103);
        _numSlot.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numSlot.Name = "_numSlot";
        _numSlot.Size = new Size(293, 23);
        _numSlot.TabIndex = 5;
        // 
        // lblSensors
        // 
        lblSensors.Anchor = AnchorStyles.Left;
        lblSensors.AutoSize = true;
        mainPanel.SetColumnSpan(lblSensors, 2);
        lblSensors.Location = new Point(23, 147);
        lblSensors.Name = "lblSensors";
        lblSensors.Size = new Size(124, 15);
        lblSensors.TabIndex = 6;
        lblSensors.Text = "Kullanılacak Sensörler:";
        // 
        // _sensorList
        // 
        _sensorList.CheckOnClick = true;
        mainPanel.SetColumnSpan(_sensorList, 2);
        _sensorList.Dock = DockStyle.Fill;
        _sensorList.Font = new Font("Segoe UI", 10F);
        _sensorList.FormattingEnabled = true;
        _sensorList.Location = new Point(23, 173);
        _sensorList.Name = "_sensorList";
        _sensorList.Size = new Size(454, 159);
        _sensorList.TabIndex = 3;
        // 
        // _btnTest
        // 
        _btnTest.Anchor = AnchorStyles.Right;
        _btnTest.Location = new Point(277, 340);
        _btnTest.Name = "_btnTest";
        _btnTest.Size = new Size(200, 35);
        _btnTest.TabIndex = 4;
        _btnTest.Text = "PLC Bağlantısını Test Et";
        _btnTest.UseVisualStyleBackColor = true;
        // 
        // PlcSettingsStep
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(mainPanel);
        Name = "PlcSettingsStep";
        Size = new Size(500, 400);
        mainPanel.ResumeLayout(false);
        mainPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_numRack).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSlot).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TableLayoutPanel mainPanel;
    private System.Windows.Forms.Label lblIp;
    private System.Windows.Forms.TextBox _txtIpAddress;
    private System.Windows.Forms.Label lblRack;
    private System.Windows.Forms.NumericUpDown _numRack;
    private System.Windows.Forms.Label lblSlot;
    private System.Windows.Forms.NumericUpDown _numSlot;
    private System.Windows.Forms.Label lblSensors;
    private System.Windows.Forms.CheckedListBox _sensorList;
    private System.Windows.Forms.Button _btnTest;
}

