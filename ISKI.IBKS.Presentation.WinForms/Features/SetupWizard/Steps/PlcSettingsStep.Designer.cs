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

    #region Component Designer generated code

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
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainPanel.Size = new Size(500, 400);
        mainPanel.TabIndex = 0;
        // 
        // lblIp
        // 
        lblIp.Dock = DockStyle.Fill;
        lblIp.Location = new Point(23, 20);
        lblIp.Name = "lblIp";
        lblIp.Size = new Size(155, 40);
        lblIp.TabIndex = 0;
        lblIp.Text = "PLC IP Adresi:";
        lblIp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtIpAddress
        // 
        _txtIpAddress.Anchor = AnchorStyles.Left;
        _txtIpAddress.Font = new Font("Segoe UI", 10F);
        _txtIpAddress.Location = new Point(184, 27);
        _txtIpAddress.Name = "_txtIpAddress";
        _txtIpAddress.Size = new Size(293, 25);
        _txtIpAddress.TabIndex = 1;
        // 
        // lblRack
        // 
        lblRack.Dock = DockStyle.Fill;
        lblRack.Location = new Point(23, 60);
        lblRack.Name = "lblRack";
        lblRack.Size = new Size(155, 40);
        lblRack.TabIndex = 2;
        lblRack.Text = "Rack:";
        lblRack.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numRack
        // 
        _numRack.Anchor = AnchorStyles.Left;
        _numRack.Font = new Font("Segoe UI", 10F);
        _numRack.Location = new Point(184, 67);
        _numRack.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numRack.Name = "_numRack";
        _numRack.Size = new Size(293, 25);
        _numRack.TabIndex = 3;
        // 
        // lblSlot
        // 
        lblSlot.Dock = DockStyle.Fill;
        lblSlot.Location = new Point(23, 100);
        lblSlot.Name = "lblSlot";
        lblSlot.Size = new Size(155, 40);
        lblSlot.TabIndex = 4;
        lblSlot.Text = "Slot:";
        lblSlot.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numSlot
        // 
        _numSlot.Anchor = AnchorStyles.Left;
        _numSlot.Font = new Font("Segoe UI", 10F);
        _numSlot.Location = new Point(184, 107);
        _numSlot.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numSlot.Name = "_numSlot";
        _numSlot.Size = new Size(293, 25);
        _numSlot.TabIndex = 5;
        // 
        // lblSensors
        // 
        mainPanel.SetColumnSpan(lblSensors, 2);
        lblSensors.Dock = DockStyle.Fill;
        lblSensors.Location = new Point(23, 140);
        lblSensors.Name = "lblSensors";
        lblSensors.Size = new Size(454, 30);
        lblSensors.TabIndex = 6;
        lblSensors.Text = "Kullanılacak Sensörler:";
        lblSensors.TextAlign = ContentAlignment.BottomLeft;
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
        _sensorList.Size = new Size(454, 164);
        _sensorList.TabIndex = 7;
        // 
        // _btnTest
        // 
        _btnTest.Dock = DockStyle.Right;
        _btnTest.Location = new Point(277, 343);
        _btnTest.Name = "_btnTest";
        _btnTest.Size = new Size(200, 34);
        _btnTest.TabIndex = 8;
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

    #endregion

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
