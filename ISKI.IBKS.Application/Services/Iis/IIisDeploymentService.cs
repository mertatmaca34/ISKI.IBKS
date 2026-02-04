using System;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Services.Iis;

/// <summary>
/// IIS kurulum ve API deployment servisi arayüzü
/// </summary>
public interface IIisDeploymentService
{
    /// <summary>
    /// IIS ve gerekli modüllerin (ASP.NET Core Hosting Bundle vb.) kurulu olduğunu doğrular.
    /// Kurulu değilse DISM ile kurmaya çalışır.
    /// </summary>
    Task<bool> EnsureIisInstalledAsync(IProgress<string>? progress = null);

    /// <summary>
    /// Local API dosyalarını belirtilen konuma çıkartır ve IIS üzerinde siteyi yapılandırır.
    /// </summary>
    Task<bool> DeployApiAsync(DeploymentConfig config, IProgress<string>? progress = null);
}

/// <summary>
/// Deployment konfigürasyonu
/// </summary>
/// <param name="ZipPath">Kaynak .zip dosya yolu</param>
/// <param name="DestinationPath">Hedef klasör yolu</param>
/// <param name="LocalIp">API'nin dinleyeceği IP</param>
/// <param name="Port">API portu</param>
/// <param name="StationId">İstasyon GUID'si</param>
public record DeploymentConfig(
    string ZipPath, 
    string DestinationPath, 
    string LocalIp, 
    int Port, 
    Guid StationId);
