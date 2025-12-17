using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.StationStatus.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.StationStatus.Services;

public interface IStationStatusService
{
    Task<StationStatusDto> GetStationStatusAsync(Guid stationId, CancellationToken cancellationToken = default);
}
