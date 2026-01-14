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

public partial class StationStatusBar : UserControl
{
    private const string LabelConnectionStatusText = "Bağlantı Durumu: ";
    private const string LabelUpTimeText = "Bağlantı Süresi: ";
    private const string LabelDailyWashRemainingTimeText = "G. Yıkamaya Kalan: ";
    private const string LabelWeeklyWashRemainingTimeText = "H. Yıkama Kalan: ";
    private const string LabelSystemTimeText = "Sistem Saati: ";

    private bool _isConnected;
    private TimeSpan _upTime;
    private TimeSpan _dailyWashRemainingTime;
    private TimeSpan _weeklyWashRemainingTime;
    private DateTime _systemTime;

    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            _isConnected = value;
            LabelConnectionStatus.Text = LabelConnectionStatusText + value;
        }
    }

    public TimeSpan UpTime
    {
        get => _upTime;
        set
        {
            _upTime = value;
            LabelUpTime.Text = LabelUpTimeText + value.ToString("dd':'hh':'mm':'ss");
        }
    }

    public TimeSpan DailyWashRemainingTime
    {
        get => _dailyWashRemainingTime;
        set
        {
            _dailyWashRemainingTime = value;
            LabelDailyWashRemainingTime.Text = LabelDailyWashRemainingTimeText + value;
        }
    }

    public TimeSpan WeeklyWashRemainingTime
    {
        get => _weeklyWashRemainingTime;
        set
        {
            _weeklyWashRemainingTime = value;
            LabelWeeklyWashRemainingTime.Text = LabelWeeklyWashRemainingTimeText + value;
        }
    }

    public DateTime SystemTime
    {
        get => _systemTime;
        set
        {
            _systemTime = value;
            LabelSystemTime.Text = LabelSystemTimeText + value;
        }
    }

    public StationStatusBar()
    {
        InitializeComponent();
    }
}
