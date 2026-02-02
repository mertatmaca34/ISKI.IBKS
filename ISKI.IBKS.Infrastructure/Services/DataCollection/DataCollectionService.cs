using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Infrastructure.Services.DataCollection;

public class DataCollectionService : ISKI.IBKS.Application.Common.IoT.IDataCollectionService
{
    private readonly IbksDbContext _dbContext;
    private readonly ISKI.IBKS.Application.Common.Configuration.IStationConfiguration _stationConfig;
    private readonly ISKI.IBKS.Application.Common.IoT.Snapshots.IStationSnapshotCache _snapshotCache;

    public DataCollectionService(
        IbksDbContext dbContext,
        ISKI.IBKS.Application.Common.Configuration.IStationConfiguration stationConfig,
        ISKI.IBKS.Application.Common.IoT.Snapshots.IStationSnapshotCache snapshotCache)
    {
        _dbContext = dbContext;
        _stationConfig = stationConfig;
        _snapshotCache = snapshotCache;
    }

    public async Task<Guid> GetStationIdAsync(CancellationToken ct = default)
    {
        return await Task.FromResult(_stationConfig.StationId);
    }

    public async Task<ISKI.IBKS.Domain.IoT.PlcDataSnapshot?> GetLatestSnapshotAsync(Guid stationId, CancellationToken ct = default)
    {
        return await _snapshotCache.Get(stationId);
    }

    public async Task SaveAndSendCalibrationAsync(ISKI.IBKS.Domain.Entities.Calibration calibration, CancellationToken ct = default)
    {
        _dbContext.Calibrations.Add(calibration);
        await _dbContext.SaveChangesAsync(ct);
    }
}
