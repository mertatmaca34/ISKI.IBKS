using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.IoT;

public interface IDataCollectionService
{
    Task<PlcDataSnapshot?> GetLatestSnapshotAsync(Guid stationId, CancellationToken ct = default);
    Task<Guid> GetStationIdAsync(CancellationToken ct = default);
    Task SaveAndSendCalibrationAsync(Calibration calibration, CancellationToken ct = default);
}
