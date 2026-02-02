using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.Shared;

public partial class DeploymentForm : Form
{

    public DeploymentForm(string title = "Web Sunucusu Yapılandırılıyor...")
    {
        InitializeComponent();
        this.lblTitle.Text = title;
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

