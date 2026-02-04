namespace ISKI.IBKS.Presentation.WinForms.Features.Shared;

partial class SqlInstallationForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        _cts?.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        contentPanel = new Panel();
        lblTitle = new Label();
        progressBar = new ProgressBar();
        lblStatus = new Label();
        btnCancel = new Button();
        contentPanel.SuspendLayout();
        SuspendLayout();
        // 
        // contentPanel
        // 
        contentPanel.BackColor = Color.FromArgb(32, 32, 38);
        contentPanel.Controls.Add(lblTitle);
        contentPanel.Controls.Add(progressBar);
        contentPanel.Controls.Add(lblStatus);
        contentPanel.Controls.Add(btnCancel);
        contentPanel.Location = new Point(0, 0);
        contentPanel.Name = "contentPanel";
        contentPanel.Size = new Size(450, 200);
        contentPanel.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.Transparent;
        lblTitle.Font = new Font("Segoe UI Semibold", 14F);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(25, 25);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(400, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "⚙️  Veritabanı Yapılandırılıyor...";
        lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // progressBar
        // 
        progressBar.Location = new Point(25, 75);
        progressBar.MarqueeAnimationSpeed = 30;
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(400, 8);
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.TabIndex = 1;
        // 
        // lblStatus
        // 
        lblStatus.BackColor = Color.Transparent;
        lblStatus.Font = new Font("Segoe UI", 9F);
        lblStatus.ForeColor = Color.FromArgb(180, 180, 180);
        lblStatus.Location = new Point(25, 95);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(400, 40);
        lblStatus.TabIndex = 2;
        lblStatus.Text = "Hazırlanıyor...";
        // 
        // btnCancel
        // 
        btnCancel.BackColor = Color.FromArgb(60, 60, 70);
        btnCancel.Cursor = Cursors.Hand;
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 90);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 9F);
        btnCancel.ForeColor = Color.White;
        btnCancel.Location = new Point(325, 155);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 3;
        btnCancel.Text = "İptal Et";
        btnCancel.UseVisualStyleBackColor = false;
        // 
        // SqlInstallationForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(32, 32, 38);
        ClientSize = new Size(450, 200);
        Controls.Add(contentPanel);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.None;
        Name = "SqlInstallationForm";
        StartPosition = FormStartPosition.CenterScreen;
        contentPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel contentPanel;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnCancel;
}
