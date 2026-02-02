using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Application.Common.IoT.Plc;

namespace ISKI.IBKS.Application.Common.IoT.Plc;

public interface IStationSnapshotReader
{
    Task<PlcDataSnapshot> ReadAsync(PlcStationConfig station, CancellationToken ct = default);
}
