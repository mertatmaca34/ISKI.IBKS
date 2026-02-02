using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.Shared;

public partial class SqlInstallationForm : Form
{
    private CancellationTokenSource? _cts;

    public CancellationToken CancellationToken => _cts?.Token ?? CancellationToken.None;

    public bool IsCancelled { get; private set; }

    public SqlInstallationForm()
    {
        _cts = new CancellationTokenSource();
        InitializeComponent();
        LoadAppIcon();
    }

    private void LoadAppIcon()
    {
        string iconPath = System.IO.Path.Combine(AppContext.BaseDirectory, "icons8_water.ico");
        if (!System.IO.File.Exists(iconPath))
            iconPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "icons8_water.ico");

        if (System.IO.File.Exists(iconPath))
        {
            this.Icon = new Icon(iconPath);
        }
    }

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
            "Kurulumu iptal etmek istediÃ„Å¸inizden emin misiniz?\n\nDikkat: YarÃ„Â±m kalan kurulum sisteminizde sorunlara yol aÃƒÂ§abilir.",
            "Kurulum Ã„Â°ptali",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            IsCancelled = true;
            _cts?.Cancel();
            btnCancel.Enabled = false;
            btnCancel.Text = "Ã„Â°ptal Ediliyor...";
            lblStatus.Text = "Kurulum iptal ediliyor, lÃƒÂ¼tfen bekleyin...";
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing && !IsCancelled)
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

        _cts?.Dispose();
        this.Close();
    }
}

