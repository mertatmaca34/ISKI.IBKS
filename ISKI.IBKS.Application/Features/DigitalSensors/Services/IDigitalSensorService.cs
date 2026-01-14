using ISKI.IBKS.Application.Common.Results;
using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;

namespace ISKI.IBKS.Application.Features.DigitalSensors.Services;

public interface IDigitalSensorService
{
    Task<IDataResult<IReadOnlyList<DigitalReadingDto>>> GetDigitalSensorsAsync(Guid stationId, CancellationToken cancellationToken = default);
}
