namespace ISKI.IBKS.Application.Services.Sql;

/// <summary>
/// SQL Server Express kurulum servisi arayüzü
/// </summary>
public interface ISqlInstallationService
{
    /// <summary>
    /// SQL Server Express'in kurulu olup olmadığını kontrol eder
    /// </summary>
    bool IsSqlExpressInstalled();

    /// <summary>
    /// SQL Server Express'i async olarak kurar
    /// </summary>
    /// <param name="progress">İlerleme durumu callback'i</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Kurulum sonucu</returns>
    Task<SqlInstallationResult> InstallSqlExpressAsync(
        IProgress<string>? progress = null,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// SQL kurulum sonucu
/// </summary>
public record SqlInstallationResult(
    bool Success,
    int ExitCode,
    string? ErrorMessage = null);
