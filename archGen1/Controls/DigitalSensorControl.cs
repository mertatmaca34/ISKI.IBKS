using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace archGen1.Controls
{
    public partial class DigitalSensorControl : UserControl
    {
        public Color StatusIndicator 
        {
            get => PanelStatusIndicator.BackColor; 
            set => PanelStatusIndicator.BackColor = value;
        }
        public string SensorName
        {
            get => LabelSensorName.Text; 
            set => LabelSensorName.Text = value;
        }
        public DigitalSensorControl()
        {
            InitializeComponent();
        }
    }
}
