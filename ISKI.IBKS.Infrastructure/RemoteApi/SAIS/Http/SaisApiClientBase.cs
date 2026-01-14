using ISKI.IBKS.Domain.Exceptions;
using ISKI.IBKS.Infrastructure.RemoteApi.Extensions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Extensions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    protected readonly SAISOptions _saisOptions;
    private readonly HttpClient _httpClient;
    private readonly ISaisTicketProvider? _saisTicketProvider;

    public SaisApiClientBase(HttpClient httpClient,
        IOptions<SAISOptions> saisOptions,
        ISaisTicketProvider? saisTicketProvider)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _saisOptions = saisOptions?.Value ?? throw new ArgumentNullException(nameof(saisOptions));
        _saisTicketProvider = saisTicketProvider ?? throw new ArgumentNullException(nameof(saisTicketProvider));

        if (string.IsNullOrWhiteSpace(_httpClient.BaseAddress?.ToString()))
        {
            _httpClient.BaseAddress = new Uri(_saisOptions.BaseUrl.TrimEnd('/') + "/", UriKind.Absolute);
        }
    }

    public async Task<SaisResultEnvelope<TResponse>> PostAsync<TResponse>(
        string relativeUri,
        object? payload,
        bool includeTicket,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, relativeUri)
        {
            Content = payload is null
                ? null
                : new StringContent(JsonSerializer.Serialize(payload, SerializerOptions), Encoding.UTF8, "application/json")
        };

        request.Headers.Remove(_saisOptions.TicketHeaderName);

        if (includeTicket)
        {
            if (_saisTicketProvider is null)
            {
                throw new InvalidOperationException("Ticket provider has not been configured for this client.");
            }

            var ticket = await _saisTicketProvider.GetTicketAsync(cancellationToken).ConfigureAwait(false);

            // SAIS API requires AToken wrapper serialization
            var aToken = new Contracts.AToken(ticket.TicketId, ticket.DeviceId);
            var ticketHeaderValue = JsonSerializer.Serialize(aToken, SerializerOptions);

            request.Headers.Remove(_saisOptions.TicketHeaderName);
            request.Headers.AddRequiredHeader(_saisOptions.TicketHeaderName, ticketHeaderValue);
        }

        try
        {
            using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized && includeTicket)
            {
                _saisTicketProvider?.InvalidateTicket();
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SaisResultEnvelope<TResponse>>(SerializerOptions, cancellationToken)
                         .ConfigureAwait(false) ?? throw new RemoteApiException("SAIS API response body was empty.");

            result.EnsureSuccess();

            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new RemoteApiException("SAIS API request failed.", ex);
        }
        catch (TimeoutException ex)
        {
            return await Task.FromResult<SaisResultEnvelope<TResponse>>(default!).ConfigureAwait(false);

            throw new RemoteApiException("SAIS API request timed out.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new RemoteApiException("SAIS API returned an unsupported media type.", ex);
        }
        catch (JsonException ex)
        {
            throw new RemoteApiException("SAIS API response could not be parsed.", ex);
        }
    }
}
