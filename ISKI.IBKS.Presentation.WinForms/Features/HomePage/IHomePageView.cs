using ISKI.IBKS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public interface IHomePageView
{
    event EventHandler Load;
    void BindAnalogSensors(StationSnapshot stationSnapshot);
}
