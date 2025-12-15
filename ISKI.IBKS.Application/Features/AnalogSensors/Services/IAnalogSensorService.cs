using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.AnalogSensors.Services;

public interface IAnalogSensorService
{
    Task<IReadOnlyList<ChannelReadingDto>> GetChannelsAsync(Guid stationId, CancellationToken cancellationToken = default);
}
