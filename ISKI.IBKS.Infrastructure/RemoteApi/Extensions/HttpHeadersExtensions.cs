using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.Extensions;

public static class HttpHeadersExtensions
{
    public static void AddRequiredHeader(
    this HttpRequestHeaders headers,
    string name,
    string? value)
    {
        if (!headers.TryAddWithoutValidation(name, value))
        {
            throw new InvalidOperationException(
                $"Failed to add required header '{name}'.");
        }
    }
}

