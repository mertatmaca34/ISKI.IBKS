using ISKI.IBKS.Infrastructure.IoT.Plc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Readers;
public interface IStationPlcTagReader
{
    TagBag ReadTagBag(Guid stationId, CancellationToken ct = default);
}
