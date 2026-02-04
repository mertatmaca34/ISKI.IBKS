using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Login;

public sealed record LoginRequest
{
    [JsonPropertyName("username")]
    public string UserName { get; init; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;
}
