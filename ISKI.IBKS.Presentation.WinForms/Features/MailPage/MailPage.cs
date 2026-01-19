using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage
{
    public partial class MailPage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;

        public MailPage(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            InitializeNavigation();

            // Load default view
            LoadControl<MailStatementsPage>();
        }

        private void InitializeNavigation()
        {
            ButtonMailStatements.Click += (s, e) => LoadControl<MailStatementsPage>();
            ButtonMailUsers.Click += (s, e) => LoadControl<MailUsersPage>();
        }

        private void LoadControl<T>() where T : UserControl
        {
            PanelContent.Controls.Clear();
            var control = _serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(_serviceProvider);
            control.Dock = DockStyle.Fill;
            PanelContent.Controls.Add(control);
        }
    }
}
