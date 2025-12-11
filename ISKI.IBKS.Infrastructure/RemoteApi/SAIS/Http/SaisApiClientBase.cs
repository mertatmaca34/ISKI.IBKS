using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public class SaisApiClientBase
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = null,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly ISaisTicketProvider _saisTicketProvider;
    private readonly SAISOptions _saisOptions;

    public SaisApiClientBase(HttpClient httpClient, ILogger logger, ISaisTicketProvider saisTicketProvider, IOptions<SAISOptions> saisOptions)
    {
        _httpClient = httpClient;
        _logger = logger;
        _saisTicketProvider = saisTicketProvider;
        _saisOptions = saisOptions.Value;
    }

    public async Task<TResponse> PostAsync<TResponse>(
        string relativeUri,
        object payload,
        bool includeTicket,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, relativeUri)
        {
            Content = payload is null
            ? null
            : new StringContent(JsonSerializer.Serialize(payload, SerializerOptions), Encoding.UTF8, "application/json")
        };

        if (includeTicket)
        {
            if (_saisTicketProvider is null)
            {
                throw new InvalidOperationException("Ticket provider has not been configured for this client.");
            }
        }

        var ticket = await _saisTicketProvider.GetTicketAsync(cancellationToken).ConfigureAwait(false);

        request.Headers.Remove()
    }
}
