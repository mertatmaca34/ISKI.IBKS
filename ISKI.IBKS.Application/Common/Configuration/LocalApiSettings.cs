namespace ISKI.IBKS.Application.Common.Configuration;

public record LocalApiSettings
{
    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 441;
    public string UserName { get; init; } = "iski";
    public string Password { get; init; } = "iski";
}

