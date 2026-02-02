using ISKI.IBKS.Shared.Localization;
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

public partial class StatusBarControl : UserControl
{
    private string _labelConnectionStatusText;
    private string _labelUpTimeText;
    private string _labelDailyWashRemainingTimeText;
    private string _labelWeeklyWashRemainingTimeText;
    private string _labelSystemTimeText;

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
            LabelConnectionStatus.Text = _labelConnectionStatusText + (value ? Strings.Common_Connected : Strings.Common_Disconnected);
        }
    }

    public TimeSpan UpTime
    {
        get => _upTime;
        set
        {
            _upTime = value;
            LabelUpTime.Text = _labelUpTimeText + value.ToString("dd':'hh':'mm':'ss");
        }
    }

    public TimeSpan DailyWashRemainingTime
    {
        get => _dailyWashRemainingTime;
        set
        {
            _dailyWashRemainingTime = value;
            LabelDailyWashRemainingTime.Text = _labelDailyWashRemainingTimeText + value;
        }
    }

    public TimeSpan WeeklyWashRemainingTime
    {
        get => _weeklyWashRemainingTime;
        set
        {
            _weeklyWashRemainingTime = value;
            LabelWeeklyWashRemainingTime.Text = _labelWeeklyWashRemainingTimeText + value;
        }
    }

    public DateTime SystemTime
    {
        get => _systemTime;
        set
        {
            _systemTime = value;
            LabelSystemTime.Text = _labelSystemTimeText + value;
        }
    }

    public StatusBarControl()
    {
        InitializeComponent();
        InitializeLocalization();
    }

    public void InitializeLocalization()
    {
        _labelConnectionStatusText = Strings.Status_ConnStatus;
        _labelUpTimeText = Strings.Status_UpTime;
        _labelDailyWashRemainingTimeText = Strings.Status_DailyWashRem;
        _labelWeeklyWashRemainingTimeText = Strings.Status_WeeklyWashRem;
        _labelSystemTimeText = Strings.Status_SystemTime;

        // Force refresh
        IsConnected = IsConnected;
        UpTime = UpTime;
        DailyWashRemainingTime = DailyWashRemainingTime;
        WeeklyWashRemainingTime = WeeklyWashRemainingTime;
        SystemTime = SystemTime;
    }
}

