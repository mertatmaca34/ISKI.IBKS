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

    private readonly bool _isConnected;
    private readonly TimeSpan _upTime;
    private readonly TimeSpan _dailyWashRemainingTime;
    private readonly TimeSpan _weeklyWashRemainingTime;
    private readonly DateTime _systemTime;

    public bool IsConnected
    {
        get => _isConnected;
        set => LabelConnectionStatus.Text = LabelConnectionStatusText + value;
    }

    public TimeSpan UpTime
    {
        get => _upTime;
        set => LabelUpTime.Text = LabelUpTimeText + value;
    }

    public TimeSpan DailyWashRemainingTime
    {
        get => _dailyWashRemainingTime;
        set => LabelDailyWashRemainingTime.Text = LabelDailyWashRemainingTimeText + value;
    }
    public TimeSpan WeeklyWashRemainingTime
    {
        get => _weeklyWashRemainingTime;
        set => LabelWeeklyWashRemainingTime.Text = LabelWeeklyWashRemainingTimeText + value;
    }

    public DateTime SystemTime
    {
        get => _systemTime;
        set => LabelSystemTime.Text = LabelSystemTimeText + value;
    }

    public StationStatusBar()
    {
        InitializeComponent();
    }
}
