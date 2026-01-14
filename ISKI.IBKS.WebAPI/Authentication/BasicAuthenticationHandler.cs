using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ISKI.IBKS.WebAPI.Authentication;

/// <summary>
/// Basic Authentication handler for SAIS API access.
/// Validates username and password from configuration.
/// </summary>
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _configuration = configuration;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing"));
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization!);
            
            if (authHeader.Scheme != "Basic")
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid authentication scheme"));
            }

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            
            if (credentials.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials format"));
            }

            var username = credentials[0];
            var password = credentials[1];

            // Get expected credentials from configuration
            var expectedUsername = _configuration["SaisApi:ConnectionUser"];
            var expectedPassword = _configuration["SaisApi:ConnectionPassword"];

            if (username != expectedUsername || password != expectedPassword)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "SaisClient")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail($"Authentication failed: {ex.Message}"));
        }
    }
}
