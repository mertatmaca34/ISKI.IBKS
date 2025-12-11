using ISKI.IBKS.Infrastructure.RemoteApi.Extensions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Exceptions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Extensions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public class SaisApiClientBase(HttpClient httpClient, ISaisTicketProvider? saisTicketProvider, IOptions<SAISOptions> saisOptions)
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = null,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private readonly SAISOptions _saisOptions = saisOptions.Value;

    public async Task<SaisResultEnvelope<TResponse>> PostAsync<TResponse>(
        string relativeUri,
        object payload,
        bool includeTicket,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, relativeUri)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(payload, SerializerOptions),
                Encoding.UTF8,
                "application/json")
        };

        if (includeTicket)
        {
            if (saisTicketProvider is null)
            {
                throw new InvalidOperationException("Ticket provider has not been configured for this client.");
            }

            var ticket = await saisTicketProvider.GetTicketAsync(cancellationToken).ConfigureAwait(false);

            var ticketHeaderValue = JsonSerializer.Serialize(ticket, SerializerOptions);

            request.Headers.Remove(_saisOptions.TicketHeaderName);
            request.Headers.AddRequiredHeader(_saisOptions.TicketHeaderName, ticketHeaderValue);
        }

        try
        {
            using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized && includeTicket)
            {
                saisTicketProvider?.InvalidateTicket();
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SaisResultEnvelope<TResponse>>(SerializerOptions, cancellationToken)
                         .ConfigureAwait(false) ?? throw new SaisApiException("SAIS API response body was empty.");

            result.EnsureSuccess();

            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new SaisApiException("SAİS API request failed.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new SaisApiException("SAİS API returned an unsupported media type.", ex);
        }
        catch (JsonException ex)
        {
            throw new SaisApiException("SAİS API response could not be parsed.", ex);
        }
    }
}
