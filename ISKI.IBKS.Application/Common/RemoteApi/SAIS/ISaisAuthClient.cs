using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public interface ISaisAuthClient
{
    Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);
}
