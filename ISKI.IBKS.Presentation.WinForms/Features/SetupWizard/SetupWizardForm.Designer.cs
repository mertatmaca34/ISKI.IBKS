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

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.SuspendLayout();

        // Header Panel
        _headerPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 80,
            BackColor = Color.FromArgb(0, 120, 215),
            Padding = new Padding(20, 15, 20, 15)
        };

        _lblTitle = new Label
        {
            Dock = DockStyle.Top,
            Text = "Kurulum Sihirbazı",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
            Height = 35
        };

        _lblDescription = new Label
        {
            Dock = DockStyle.Fill,
            Text = "Adım açıklaması",
            ForeColor = Color.FromArgb(220, 220, 220),
            Font = new Font("Segoe UI", 10F)
        };

        _headerPanel.Controls.Add(_lblDescription);
        _headerPanel.Controls.Add(_lblTitle);

        // Step indicator panel
        _stepPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 50,
            BackColor = Color.FromArgb(240, 240, 240),
            Padding = new Padding(20, 10, 20, 10)
        };

        _lblStepIndicator = new Label
        {
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 10F)
        };
        _stepPanel.Controls.Add(_lblStepIndicator);

        // Content panel
        _contentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        // Button panel
        _buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            Padding = new Padding(20, 10, 20, 10)
        };

        _btnPrevious = new Button
        {
            Text = "< Geri",
            Width = 100,
            Height = 35,
            Dock = DockStyle.Left,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(220, 220, 220)
        };

        _btnNext = new Button
        {
            Text = "İleri >",
            Width = 100,
            Height = 35,
            Dock = DockStyle.Right,
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };

        _buttonPanel.Controls.Add(_btnPrevious);
        _buttonPanel.Controls.Add(_btnNext);

        // Add controls in correct order
        this.Controls.Add(_contentPanel);
        this.Controls.Add(_stepPanel);
        this.Controls.Add(_headerPanel);
        this.Controls.Add(_buttonPanel);

        // Form settings
        this.Text = "İSKİ IBKS - Kurulum Sihirbazı";
        this.Size = new Size(700, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ControlBox = false; // Kapat butonu yok

        this.ResumeLayout(false);
    }

    private Panel _headerPanel;
    private Label _lblTitle;
    private Label _lblDescription;
    private Panel _stepPanel;
    private Label _lblStepIndicator;
    private Panel _contentPanel;
    private Panel _buttonPanel;
    private Button _btnPrevious;
    private Button _btnNext;
}
