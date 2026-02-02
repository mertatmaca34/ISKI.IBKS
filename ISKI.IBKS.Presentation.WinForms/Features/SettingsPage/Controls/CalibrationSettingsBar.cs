using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ISKI.IBKS.Shared.Localization;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls
{
    public partial class CalibrationSettingsBar : UserControl
    {
        [Description("Parametre"), Category("IBKS")]
        public string Parameter
        {
            get => LabelParameter.Text;
            set => LabelParameter.Text = value;
        }

        [Description("Zero Referans DeÄŸeri"), Category("IBKS")]
        public string ZeroRef
        {
            get => ComboBoxZeroRef.Text;
            set => ComboBoxZeroRef.Text = value;
        }

        [Description("Zero SÃ¼re DeÄŸeri"), Category("IBKS")]
        public string ZeroTime
        {
            get => ComboBoxZeroTime.Text;
            set => ComboBoxZeroTime.Text = value;
        }

        [Description("Span Referans DeÄŸeri"), Category("IBKS")]
        public string SpanRef
        {
            get => ComboBoxSpanRef.Text;
            set => ComboBoxSpanRef.Text = value;
        }

        [Description("Span Süre Değeri"), Category("IBKS")]
        public string SpanTime
        {
            get => ComboBoxSpanTime.Text;
            set => ComboBoxSpanTime.Text = value;
        }

        public CalibrationSettingsBar()
        {
            InitializeComponent();
            InitializeLocalization();
        }

        private void InitializeLocalization()
        {
            label1.Text = Strings.Cal_ZeroRef;
            label3.Text = Strings.Cal_ZeroTime;
            label4.Text = Strings.Cal_SpanRef;
            label5.Text = Strings.Cal_SpanTime;
        }
    }
}

