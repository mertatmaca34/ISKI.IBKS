using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.SimulationPage;

public class SimulationPagePresenter
{
    private readonly IOptions<PlcSettings> _options;

    public SimulationPagePresenter(
        IOptions<PlcSettings> options)
    {
        _options = options;
    }


}
