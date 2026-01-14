using ISKI.IBKS.Domain.Exceptions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Extensions;
public static class SaisResultEnvelopeExtensions
{
    public static void EnsureSuccess<T>(this SaisResultEnvelope<T> envelope)
    {
        if (envelope is not { Result: true })
        {
            throw new RemoteApiException(
                envelope.Message ?? "SAIS API returned an unsuccessful result.");
        }
    }
}

