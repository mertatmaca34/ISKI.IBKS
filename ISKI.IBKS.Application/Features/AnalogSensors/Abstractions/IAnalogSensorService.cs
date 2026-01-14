using ISKI.IBKS.Application.Common.Results;
using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
namespace ISKI.IBKS.Application.Features.AnalogSensors.Abstractions;

public interface IAnalogSensorService
{
    Task<IDataResult<IReadOnlyList<ChannelReadingDto>>> GetChannelsAsync(Guid stationId, CancellationToken cancellationToken = default);
}
