using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.Shared;

/// <summary>
/// SQL Server kurulumu sırasında kullanıcıyı bilgilendiren modern splash screen formu
/// </summary>
public partial class SqlInstallationForm : Form
{
    private CancellationTokenSource? _cts;
    private bool _allowClose;
    
    /// <summary>
    /// İptal token'ı - kurulumu iptal etmek için kullanılır
    /// </summary>
    public CancellationToken CancellationToken => _cts?.Token ?? CancellationToken.None;
    
    /// <summary>
    /// Kurulum iptal edildi mi?
    /// </summary>
    public bool IsCancelled { get; private set; }

    public SqlInstallationForm()
    {
        _cts = new CancellationTokenSource();
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        // Load icon at runtime
        LoadIcon();

        // Wire up events
        btnCancel.Click += BtnCancel_Click;
        this.Paint += OnFormPaint;
    }

    private void LoadIcon()
    {
        string iconPath = Path.Combine(AppContext.BaseDirectory, "icons8_water.ico");
        if (!File.Exists(iconPath))
            iconPath = Path.Combine(AppContext.BaseDirectory, "Resources", "icons8_water.ico");
        if (File.Exists(iconPath))
        {
            this.Icon = new Icon(iconPath);
        }
    }

    private void OnFormPaint(object? sender, PaintEventArgs e)
    {
        using var pen = new Pen(Color.FromArgb(60, 60, 70), 2);
        e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
    }

    /// <summary>
    /// Durum mesajını günceller (thread-safe)
    /// </summary>
    public void UpdateStatus(string status)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateStatus(status)));
            return;
        }
        lblStatus.Text = status;
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show(
            "Kurulumu iptal etmek istediğinizden emin misiniz?\n\nDikkat: Yarım kalan kurulum sisteminizde sorunlara yol açabilir.",
            "Kurulum İptali",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            IsCancelled = true;
            _cts?.Cancel();
            btnCancel.Enabled = false;
            btnCancel.Text = "İptal Ediliyor...";
            lblStatus.Text = "Kurulum iptal ediliyor, lütfen bekleyin...";
        }
    }

    /// <summary>
    /// Form kapatılmasını engeller (kurulum sırasında)
    /// </summary>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Allow programmatic close via CloseForm()
        if (_allowClose || IsCancelled)
        {
            base.OnFormClosing(e);
            return;
        }
        
        // Block user close attempts
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            return;
        }
        base.OnFormClosing(e);
    }

    /// <summary>
    /// Formu güvenli şekilde kapatır
    /// </summary>
    public void CloseForm()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(CloseForm));
            return;
        }
        
        _allowClose = true;
        _cts?.Dispose();
        this.Close();
    }
}
