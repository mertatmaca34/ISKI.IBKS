using Microsoft.Extensions.Logging;
using ISKI.IBKS.Shared.Results;

namespace ISKI.IBKS.Application.Common.Features.Setup.InstallSql;

public class InstallSqlHandler
{
    private readonly ILogger<InstallSqlHandler> _logger;

    public InstallSqlHandler(ILogger<InstallSqlHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<SqlInstallationResult>> Handle(InstallSqlCommand command, CancellationToken ct)
    {
        _logger.LogInformation("SQL Installation started.");
        // TODO: Implement SQL Express installation logic
        return Result<SqlInstallationResult>.Success(new SqlInstallationResult(true, "SQL Server Express is already installed or installation was skipped."));
    }
}
