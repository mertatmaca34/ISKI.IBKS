using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Common.Navigation;

public class NavigationEventArgs : EventArgs
{
    public UserControl NewPage { get; }

    public UserControl? OldPage { get; }

    public NavigationEventArgs(UserControl newPage, UserControl? oldPage)
    {
        NewPage = newPage;
        OldPage = oldPage;
    }
}

