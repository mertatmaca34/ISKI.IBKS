using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public class SaisApiClientBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly ISaisTicketProvider _saisTicketProvider;

    public SaisApiClientBase(HttpClient httpClient, ILogger logger, ISaisTicketProvider saisTicketProvider)
    {
        _httpClient = httpClient;
        _logger = logger;
        _saisTicketProvider = saisTicketProvider;
    }


}
