using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Configuration;

public interface ISelectedSensorsProvider
{
    Task<IReadOnlyList<string>> GetSelectedSensorsAsync(CancellationToken cancellationToken = default);
}
