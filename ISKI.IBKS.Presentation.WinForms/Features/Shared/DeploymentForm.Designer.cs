namespace ISKI.IBKS.Presentation.WinForms.Features.Shared;

partial class DeploymentForm
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
        contentPanel.SuspendLayout();
        SuspendLayout();
        // 
        // contentPanel
        // 
        contentPanel.BackColor = Color.FromArgb(32, 32, 38);
        contentPanel.Controls.Add(lblTitle);
        contentPanel.Controls.Add(progressBar);
        contentPanel.Controls.Add(lblStatus);
        contentPanel.Location = new Point(0, 0);
        contentPanel.Name = "contentPanel";
        contentPanel.Size = new Size(450, 180);
        contentPanel.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.Transparent;
        lblTitle.Font = new Font("Segoe UI Semibold", 14F);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(25, 30);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(400, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "🌐  Web Sunucusu Yapılandırılıyor...";
        lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // progressBar
        // 
        progressBar.Location = new Point(25, 85);
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
        lblStatus.Location = new Point(25, 105);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(400, 40);
        lblStatus.TabIndex = 2;
        lblStatus.Text = "Hazırlanıyor...";
        // 
        // DeploymentForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(32, 32, 38);
        ClientSize = new Size(450, 180);
        Controls.Add(contentPanel);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.None;
        Name = "DeploymentForm";
        StartPosition = FormStartPosition.CenterScreen;
        contentPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel contentPanel;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblStatus;
}
