using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Shared.Results;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Extensions;

public static class SaisResultEnvelopeExtensions
{
    public static Result ToResult<T>(this SaisResultEnvelope<T> envelope)
    {
        if (envelope.Result)
        {
            return Result.Success();
        }

        return Result.Failure(Error.Create("SAIS_ERROR", envelope.Message ?? "Unknown SAIS error"));
    }
}
