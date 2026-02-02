namespace ISKI.IBKS.Application.Common.Configuration;

public record SaisSettings
{
    public string BaseUrl { get; init; } = string.Empty;
    public string LoginUrl { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string TicketHeaderName { get; init; } = "AToken";
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(30);
}

