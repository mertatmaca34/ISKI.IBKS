using ISKI.IBKS.Infrastructure.RemoteApi.Extensions;
using ISKI.IBKS.Infrastructure.RemoteApi.Exceptions;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using ISKI.IBKS.Domain.Enums;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<SaisApiClientBase> _logger;

    public SaisApiClientBase(HttpClient httpClient,
        IOptions<SAISOptions> saisOptions,
        ISaisTicketProvider? saisTicketProvider,
        ILogger<SaisApiClientBase> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _saisOptions = saisOptions?.Value ?? throw new ArgumentNullException(nameof(saisOptions));
        _saisTicketProvider = saisTicketProvider;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
        _logger.LogInformation(
            "SAIS API Request",
            $"Initiating request to {relativeUri}. IncludeTicket={includeTicket}",
            LogCategory.ApiCall);

        if (includeTicket)
        {
            if (_saisTicketProvider is null)
            {
                _logger.LogError(
                    "SAIS API Error",
                    "Ticket provider has not been configured for this client.",
                    LogCategory.ApiCall);
                throw new InvalidOperationException("Ticket provider has not been configured for this client.");
            }

            var ticket = await _saisTicketProvider.GetTicketAsync(cancellationToken).ConfigureAwait(false);

            _logger.LogInformation(
                "SAIS Ticket Validated",
                $"Using ticket {ticket.TicketId} for request to {relativeUri}",
                LogCategory.ApiCall);
        }

        return await ExecuteRequestWithRetryAsync<TResponse>(relativeUri, payload, includeTicket, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<SaisResultEnvelope<TResponse>> ExecuteRequestWithRetryAsync<TResponse>(
        string relativeUri,
        object? payload,
        bool includeTicket,
        CancellationToken cancellationToken,
        bool isRetry = false)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, relativeUri)
        {
            Content = payload is null
                ? null
                : new StringContent(JsonSerializer.Serialize(payload, SerializerOptions), Encoding.UTF8, "application/json")
        };

        if (includeTicket)
        {
            var ticket = await _saisTicketProvider!.GetTicketAsync(cancellationToken).ConfigureAwait(false);

            var aToken = new AToken(ticket.TicketId, ticket.DeviceId);
            var ticketHeaderValue = JsonSerializer.Serialize(aToken, SerializerOptions);

            request.Headers.Remove(_saisOptions.TicketHeaderName);
            request.Headers.AddRequiredHeader(_saisOptions.TicketHeaderName, ticketHeaderValue);
        }
        else
        {
            var emptyToken = new AToken(null, null);
            request.Headers.AddRequiredHeader("AToken", JsonSerializer.Serialize(emptyToken, SerializerOptions));
        }

        string? rawResponse = null;

        try
        {
            using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized && includeTicket)
            {
                _logger.LogWarning(
                    "SAIS API 401 Response",
                    $"Received 401 Unauthorized from {relativeUri}. IsRetry={isRetry}",
                    LogCategory.ApiCall);

                _saisTicketProvider?.InvalidateTicket();

                if (!isRetry)
                {
                    _logger.LogInformation(
                        "SAIS API Retry",
                        $"Retrying request to {relativeUri} with new ticket",
                        LogCategory.ApiCall);

                    return await ExecuteRequestWithRetryAsync<TResponse>(
                        relativeUri, payload, includeTicket, cancellationToken, isRetry: true)
                        .ConfigureAwait(false);
                }
            }

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<SaisResultEnvelope<TResponse>>(rawResponse, SerializerOptions)
                         ?? throw new RemoteApiException("SAIS API response body was empty.");

            result.EnsureSuccess();

            _logger.LogInformation(
                "SAIS API Success",
                $"Request to {relativeUri} completed successfully",
                LogCategory.ApiCall);

            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(
                "SAIS API Request Failed",
                $"Request to {relativeUri} failed: {ex.Message}",
                LogCategory.ApiCall);
            throw new RemoteApiException("SAIS API request failed.", ex);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(
                "SAIS API Timeout",
                $"Request to {relativeUri} timed out",
                LogCategory.ApiCall);
            throw new RemoteApiException("SAIS API request timed out.", ex);
        }
        catch (NotSupportedException ex)
        {
            _logger.LogError(
                "SAIS API Unsupported Media Type",
                $"Request to {relativeUri} returned unsupported media type",
                LogCategory.ApiCall);
            throw new RemoteApiException("SAIS API returned an unsupported media type.", ex);
        }
        catch (JsonException ex)
        {
            var responseContent = rawResponse ?? "(no response captured)";
            if (responseContent.Length > 500)
                responseContent = responseContent.Substring(0, 500) + "...[truncated]";

            _logger.LogError(
                "SAIS API Parse Error",
                $"Response from {relativeUri} could not be parsed. Response: {responseContent}",
                LogCategory.ApiCall);

            if (relativeUri.Contains("GetCalibration") || relativeUri.Contains("GetChannelInformation"))
            {
                return new SaisResultEnvelope<TResponse> { Result = false, Message = "Parse error - response may be empty or malformed" };
            }

            throw new RemoteApiException("SAIS API response could not be parsed.", ex);
        }
    }
}

