using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;

public sealed class SAISOptions
{
    public string BaseUrl { get; init; } = "https://entegrationsais.csb.gov.tr/";
    public string LoginUrl { get; init; } = "/Security/login";
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(10);

    //Login
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;

    //Ticket
    public TimeSpan TicketTtl { get; init; } = TimeSpan.FromMinutes(29);
    public string TicketHeaderName { get; init; } = "AToken";

}
