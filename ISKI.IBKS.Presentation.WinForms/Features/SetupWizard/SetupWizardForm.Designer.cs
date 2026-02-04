namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this._headerPanel = new System.Windows.Forms.Panel();
        this._lblTitle = new System.Windows.Forms.Label();
        this._lblDescription = new System.Windows.Forms.Label();
        this._stepPanel = new System.Windows.Forms.Panel();
        this._lblStepIndicator = new System.Windows.Forms.Label();
        this._contentPanel = new System.Windows.Forms.Panel();
        this._buttonPanel = new System.Windows.Forms.Panel();
        this._btnPrevious = new System.Windows.Forms.Button();
        this._btnNext = new System.Windows.Forms.Button();
        this._headerPanel.SuspendLayout();
        this._stepPanel.SuspendLayout();
        this._buttonPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // _headerPanel
        // 
        this._headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
        this._headerPanel.Controls.Add(this._lblDescription);
        this._headerPanel.Controls.Add(this._lblTitle);
        this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this._headerPanel.Location = new System.Drawing.Point(0, 0);
        this._headerPanel.Name = "_headerPanel";
        this._headerPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
        this._headerPanel.Size = new System.Drawing.Size(700, 80);
        this._headerPanel.TabIndex = 0;
        // 
        // _lblTitle
        // 
        this._lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this._lblTitle.ForeColor = System.Drawing.Color.White;
        this._lblTitle.Location = new System.Drawing.Point(20, 15);
        this._lblTitle.Name = "_lblTitle";
        this._lblTitle.Size = new System.Drawing.Size(660, 35);
        this._lblTitle.TabIndex = 0;
        this._lblTitle.Text = "Kurulum Sihirbazı";
        // 
        // _lblDescription
        // 
        this._lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
        this._lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
        this._lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
        this._lblDescription.Location = new System.Drawing.Point(20, 50);
        this._lblDescription.Name = "_lblDescription";
        this._lblDescription.Size = new System.Drawing.Size(660, 15);
        this._lblDescription.TabIndex = 1;
        this._lblDescription.Text = "Adım açıklaması";
        // 
        // _stepPanel
        // 
        this._stepPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
        this._stepPanel.Controls.Add(this._lblStepIndicator);
        this._stepPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this._stepPanel.Location = new System.Drawing.Point(0, 80);
        this._stepPanel.Name = "_stepPanel";
        this._stepPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
        this._stepPanel.Size = new System.Drawing.Size(700, 50);
        this._stepPanel.TabIndex = 1;
        // 
        // _lblStepIndicator
        // 
        this._lblStepIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
        this._lblStepIndicator.Font = new System.Drawing.Font("Segoe UI", 10F);
        this._lblStepIndicator.Location = new System.Drawing.Point(20, 10);
        this._lblStepIndicator.Name = "_lblStepIndicator";
        this._lblStepIndicator.Size = new System.Drawing.Size(660, 30);
        this._lblStepIndicator.TabIndex = 0;
        this._lblStepIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // _contentPanel
        // 
        this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this._contentPanel.Location = new System.Drawing.Point(0, 130);
        this._contentPanel.Name = "_contentPanel";
        this._contentPanel.Padding = new System.Windows.Forms.Padding(10);
        this._contentPanel.Size = new System.Drawing.Size(700, 410);
        this._contentPanel.TabIndex = 2;
        // 
        // _buttonPanel
        // 
        this._buttonPanel.Controls.Add(this._btnPrevious);
        this._buttonPanel.Controls.Add(this._btnNext);
        this._buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this._buttonPanel.Location = new System.Drawing.Point(0, 540);
        this._buttonPanel.Name = "_buttonPanel";
        this._buttonPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
        this._buttonPanel.Size = new System.Drawing.Size(700, 60);
        this._buttonPanel.TabIndex = 3;
        // 
        // _btnPrevious
        // 
        this._btnPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
        this._btnPrevious.Dock = System.Windows.Forms.DockStyle.Left;
        this._btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this._btnPrevious.Location = new System.Drawing.Point(20, 10);
        this._btnPrevious.Name = "_btnPrevious";
        this._btnPrevious.Size = new System.Drawing.Size(100, 40);
        this._btnPrevious.TabIndex = 0;
        this._btnPrevious.Text = "< Geri";
        this._btnPrevious.UseVisualStyleBackColor = false;
        // 
        // _btnNext
        // 
        this._btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
        this._btnNext.Dock = System.Windows.Forms.DockStyle.Right;
        this._btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this._btnNext.ForeColor = System.Drawing.Color.White;
        this._btnNext.Location = new System.Drawing.Point(580, 10);
        this._btnNext.Name = "_btnNext";
        this._btnNext.Size = new System.Drawing.Size(100, 40);
        this._btnNext.TabIndex = 1;
        this._btnNext.Text = "İleri >";
        this._btnNext.UseVisualStyleBackColor = false;
        // 
        // SetupWizardForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(700, 600);
        this.ControlBox = false;
        this.Controls.Add(this._contentPanel);
        this.Controls.Add(this._stepPanel);
        this.Controls.Add(this._headerPanel);
        this.Controls.Add(this._buttonPanel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "SetupWizardForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "İSKİ IBKS - Kurulum Sihirbazı";
        this.TopMost = true;
        this._headerPanel.ResumeLayout(false);
        this._stepPanel.ResumeLayout(false);
        this._buttonPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

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
