using ISKI.IBKS.Shared.Localization;
using System;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;

public partial class StationParameterStatusControl : UserControl
{
    public StationParameterStatusControl()
    {
        InitializeComponent();
        InitializeLocalization();
    }

    private void InitializeLocalization()
    {
        label1.Text = Strings.Param_Header;
        label2.Text = Strings.Param_LastWash;
        label3.Text = Strings.Param_LastWeeklyWash;
        label4.Text = Strings.Param_LastCalibration;

        label5.Text = Strings.Sensor_Akm;
        label6.Text = Strings.Sensor_DO;
        label7.Text = Strings.Sensor_Flow;
        label8.Text = Strings.Sensor_Koi;
        label9.Text = Strings.Sensor_Ph;
        label10.Text = Strings.Sensor_Sicaklik;
        label11.Text = Strings.Sensor_Iletkenlik;
    }
}
