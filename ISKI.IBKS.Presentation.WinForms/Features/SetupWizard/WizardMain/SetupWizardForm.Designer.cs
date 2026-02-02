namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;

partial class SetupWizardForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupWizardForm));
        _headerPanel = new Panel();
        _lblDescription = new Label();
        _lblTitle = new Label();
        _stepPanel = new Panel();
        _lblStepIndicator = new Label();
        _contentPanel = new Panel();
        _buttonPanel = new Panel();
        _btnPrevious = new Button();
        _btnNext = new Button();
        _headerPanel.SuspendLayout();
        _stepPanel.SuspendLayout();
        _buttonPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _headerPanel
        // 
        _headerPanel.BackColor = Color.FromArgb(0, 120, 215);
        _headerPanel.Controls.Add(_lblDescription);
        _headerPanel.Controls.Add(_lblTitle);
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.Location = new Point(0, 0);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Padding = new Padding(20, 15, 20, 15);
        _headerPanel.Size = new Size(684, 80);
        _headerPanel.TabIndex = 2;
        // 
        // _lblDescription
        // 
        _lblDescription.Dock = DockStyle.Fill;
        _lblDescription.Font = new Font("Segoe UI", 10F);
        _lblDescription.ForeColor = Color.FromArgb(220, 220, 220);
        _lblDescription.Location = new Point(20, 50);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(644, 15);
        _lblDescription.TabIndex = 1;
        _lblDescription.Text = "Step Description";
        // 
        // _lblTitle
        // 
        _lblTitle.Dock = DockStyle.Top;
        _lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        _lblTitle.ForeColor = Color.White;
        _lblTitle.Location = new Point(20, 15);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Size = new Size(644, 35);
        _lblTitle.TabIndex = 0;
        _lblTitle.Text = "Setup Wizard";
        // 
        // _stepPanel
        // 
        _stepPanel.BackColor = Color.FromArgb(240, 240, 240);
        _stepPanel.Controls.Add(_lblStepIndicator);
        _stepPanel.Dock = DockStyle.Top;
        _stepPanel.Location = new Point(0, 80);
        _stepPanel.Name = "_stepPanel";
        _stepPanel.Padding = new Padding(20, 10, 20, 10);
        _stepPanel.Size = new Size(684, 50);
        _stepPanel.TabIndex = 1;
        // 
        // _lblStepIndicator
        // 
        _lblStepIndicator.Dock = DockStyle.Fill;
        _lblStepIndicator.Font = new Font("Segoe UI", 10F);
        _lblStepIndicator.Location = new Point(20, 10);
        _lblStepIndicator.Name = "_lblStepIndicator";
        _lblStepIndicator.Size = new Size(644, 30);
        _lblStepIndicator.TabIndex = 0;
        _lblStepIndicator.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _contentPanel
        // 
        _contentPanel.Dock = DockStyle.Fill;
        _contentPanel.Location = new Point(0, 130);
        _contentPanel.Name = "_contentPanel";
        _contentPanel.Padding = new Padding(10);
        _contentPanel.Size = new Size(684, 371);
        _contentPanel.TabIndex = 0;
        // 
        // _buttonPanel
        // 
        _buttonPanel.Controls.Add(_btnPrevious);
        _buttonPanel.Controls.Add(_btnNext);
        _buttonPanel.Dock = DockStyle.Bottom;
        _buttonPanel.Location = new Point(0, 501);
        _buttonPanel.Name = "_buttonPanel";
        _buttonPanel.Padding = new Padding(20, 10, 20, 10);
        _buttonPanel.Size = new Size(684, 60);
        _buttonPanel.TabIndex = 3;
        // 
        // _btnPrevious
        // 
        _btnPrevious.BackColor = Color.FromArgb(220, 220, 220);
        _btnPrevious.Dock = DockStyle.Left;
        _btnPrevious.FlatStyle = FlatStyle.Flat;
        _btnPrevious.Location = new Point(20, 10);
        _btnPrevious.Name = "_btnPrevious";
        _btnPrevious.Size = new Size(100, 40);
        _btnPrevious.TabIndex = 1;
        _btnPrevious.Text = "< Back";
        _btnPrevious.UseVisualStyleBackColor = false;
        // 
        // _btnNext
        // 
        _btnNext.BackColor = Color.FromArgb(0, 120, 215);
        _btnNext.Dock = DockStyle.Right;
        _btnNext.FlatStyle = FlatStyle.Flat;
        _btnNext.ForeColor = Color.White;
        _btnNext.Location = new Point(564, 10);
        _btnNext.Name = "_btnNext";
        _btnNext.Size = new Size(100, 40);
        _btnNext.TabIndex = 0;
        _btnNext.Text = "Next >";
        _btnNext.UseVisualStyleBackColor = false;
        // 
        // SetupWizardForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 561);
        ControlBox = false;
        Controls.Add(_contentPanel);
        Controls.Add(_stepPanel);
        Controls.Add(_headerPanel);
        Controls.Add(_buttonPanel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SetupWizardForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ISKI IBKS - Setup Wizard";
        TopMost = true;
        _headerPanel.ResumeLayout(false);
        _stepPanel.ResumeLayout(false);
        _buttonPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Panel _headerPanel;
    private System.Windows.Forms.Label _lblTitle;
    private System.Windows.Forms.Label _lblDescription;
    private System.Windows.Forms.Panel _stepPanel;
    private System.Windows.Forms.Label _lblStepIndicator;
    private System.Windows.Forms.Panel _contentPanel;
    private System.Windows.Forms.Panel _buttonPanel;
    private System.Windows.Forms.Button _btnPrevious;
    private System.Windows.Forms.Button _btnNext;
}

