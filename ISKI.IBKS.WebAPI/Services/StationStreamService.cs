using Grpc.Core;
using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.WebAPI.Protos;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;

namespace ISKI.IBKS.WebAPI.Services;

public class StationStreamService : StationStream.StationStreamBase
{
    private readonly IStationSnapshotCache _snapshotCache;
    private readonly ILogger<StationStreamService> _logger;

    public StationStreamService(IStationSnapshotCache snapshotCache, ILogger<StationStreamService> logger)
    {
        _snapshotCache = snapshotCache;
        _logger = logger;
    }

    public override async Task GetLiveStream(
        StreamRequest request,
        IServerStreamWriter<TelemetryData> responseStream,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.StationId, out var stationId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid StationId"));
        }

        _logger.LogInformation("gRPC Stream started for Station: {StationId}", stationId);

        while (!context.CancellationToken.IsCancellationRequested)
        {
            var snapshot = await _snapshotCache.Get(stationId);

            if (snapshot != null)
            {
                var data = new TelemetryData
                {
                    StationId = stationId.ToString(),
                    ReadTime = Timestamp.FromDateTime(snapshot.SystemTime.ToUniversalTime()),
                    Ph = snapshot.Ph,
                    Conductivity = snapshot.Iletkenlik,
                    DissolvedOxygen = snapshot.CozunmusOksijen,
                    Turbidity = snapshot.Akm,
                    IsSampling = snapshot.AkmNumuneTetik || snapshot.KoiNumuneTetik || snapshot.PhNumuneTetik,
                    IsCalibration = snapshot.KabinKalibrasyonModu,
                    IsMaintenance = snapshot.KabinBakimModu
                };

                await responseStream.WriteAsync(data);
            }

            await Task.Delay(1000, context.CancellationToken);
        }

        _logger.LogInformation("gRPC Stream ended for Station: {StationId}", stationId);
    }
}

