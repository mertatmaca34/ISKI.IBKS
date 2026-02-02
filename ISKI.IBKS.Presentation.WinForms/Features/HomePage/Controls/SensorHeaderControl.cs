using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;

public partial class SensorHeaderControl : UserControl
{
    public string HeaderTitle
    {
        get => LabelHeaderTitle.Text;
        set => LabelHeaderTitle.Text = value;
    }

    public string HeaderTitle2
    {
        get => LabelHeaderTitle2.Text;
        set => LabelHeaderTitle2.Text = value;
    }

    public string HeaderTitle3
    {
        get => LabelHeaderTitle3.Text;
        set => LabelHeaderTitle3.Text = value;
    }

    public SensorHeaderControl()
    {
        InitializeComponent();
    }
}

