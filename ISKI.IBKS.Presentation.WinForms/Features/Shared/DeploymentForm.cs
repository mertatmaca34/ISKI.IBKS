using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.Shared;

/// <summary>
/// IIS ve API kurulumu sırasında kullanıcıyı bilgilendiren modern splash screen formu
/// </summary>
public partial class DeploymentForm : Form
{
    private string _titleText = "Web Sunucusu Yapılandırılıyor...";

    public DeploymentForm(string title = "Web Sunucusu Yapılandırılıyor...")
    {
        _titleText = title;
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        // Set title
        lblTitle.Text = $"🌐  {_titleText}";
        
        // Form border paint event
        this.Paint += OnFormPaint;
    }

    private void OnFormPaint(object? sender, PaintEventArgs e)
    {
        using var pen = new Pen(Color.FromArgb(0, 120, 215), 2);
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

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            return;
        }
        base.OnFormClosing(e);
    }

    public void CloseForm()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(CloseForm));
            return;
        }
        this.Close();
    }
}
