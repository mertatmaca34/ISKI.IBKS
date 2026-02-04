using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls
{
    public partial class SettingsBarControl : UserControl
    {
        [Description("Ayar Adı"), Category("IBKS")]
        public string AyarAdi
        {
            get => LabelParameter.Text;
            set => LabelParameter.Text = value;
        }

        [Description("Ayar Değeri"), Category("IBKS")]
        public string AyarDegeri
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }
        public SettingsBarControl()
        {
            InitializeComponent();
        }
    }
}
