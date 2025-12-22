using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.DigitalSensors.Services;

public interface IDigitalSensorService
{
    Task<IReadOnlyList<DigitalReadingDto>> GetDigitalSensorsAsync(Guid stationId, CancellationToken cancellationToken = default);
}
