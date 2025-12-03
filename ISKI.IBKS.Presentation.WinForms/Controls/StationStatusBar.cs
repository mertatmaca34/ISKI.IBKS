using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Controls
{
    public partial class StationStatusBar : UserControl
    {
        private const string LabelConnectionStatusText = "Bağlantı Durumu: ";
        private const string LabelUpTimeText = "Bağlantı Süresi: ";
        private const string LabelDailyWashRemainingTimeText = "G. Yıkamaya Kalan: ";
        private const string LabelWeeklyWashRemainingTimeText = "H. Yıkama Kalan: ";
        private const string LabelSystemTimeText = "Sistem Saati: ";
       
        public string IsConnected
        {
            get => LabelConnectionStatus.Text;
            set => LabelConnectionStatus.Text = LabelConnectionStatusText + value;
        }

        public string UpTime
        {
            get => LabelUpTime.Text;
            set => LabelUpTime.Text = LabelUpTimeText + value;
        }

        public string DailyWashRemainingTime
        {
            get => LabelDailyWashRemainingTime.Text;
            set => LabelDailyWashRemainingTime.Text = LabelDailyWashRemainingTimeText + value;
        }
        public string WeeklyWashRemainingTime
        {
            get => LabelWeeklyWashRemainingTime.Text;
            set => LabelWeeklyWashRemainingTime.Text = LabelWeeklyWashRemainingTimeText + value;
        }

        public string SystemTime
        {
            get => LabelSystemTime.Text;
            set => LabelSystemTime.Text = LabelSystemTimeText + value;
        }

        public StationStatusBar()
        {
            InitializeComponent();
        }
    }
}
