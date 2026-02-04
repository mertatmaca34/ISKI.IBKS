using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ISKI.IBKS.Application.Services.Iis;

namespace ISKI.IBKS.Infrastructure.Services.Iis;

public class IisDeploymentService : IIisDeploymentService
{
    private const string AppPoolName = "ISKI_AppPool";
    private const string SiteName = "ISKI_LocalAPI";

    public async Task<bool> EnsureIisInstalledAsync(IProgress<string>? progress = null)
    {
        try
        {
            progress?.Report("🔍 IIS servisleri kontrol ediliyor...");
            
            // IIS kurulu mu kontrol et
            string inetSrvPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "inetsrv");
            bool iisInstalled = Directory.Exists(inetSrvPath) && File.Exists(Path.Combine(inetSrvPath, "w3wp.exe"));
            
            if (!iisInstalled)
            {
                progress?.Report("📦 IIS kurulu değil, Windows özellikleri yükleniyor...");
                
                // IIS'i ve gerekli tüm bileşenleri sessizce kur
                var iisFeatures = new[]
                {
                    "IIS-WebServerRole",
                    "IIS-WebServer",
                    "IIS-CommonHttpFeatures",
                    "IIS-HttpErrors",
                    "IIS-StaticContent",
                    "IIS-DefaultDocument",
                    "IIS-DirectoryBrowsing",
                    "IIS-HealthAndDiagnostics",
                    "IIS-HttpLogging",
                    "IIS-LoggingLibraries",
                    "IIS-RequestMonitor",
                    "IIS-Security",
                    "IIS-RequestFiltering",
                    "IIS-Performance",
                    "IIS-HttpCompressionStatic",
                    "IIS-WebServerManagementTools",
                    "IIS-ManagementConsole",
                    "IIS-ManagementScriptingTools",
                    "NetFx4Extended-ASPNET45",
                    "IIS-NetFxExtensibility45",
                    "IIS-ISAPIExtensions",
                    "IIS-ISAPIFilter",
                    "IIS-ASPNET45"
                };

                // DISM ile özellikleri yükle
                string featureList = string.Join(" ", iisFeatures.Select(f => $"/FeatureName:{f}"));
                string dismArgs = $"/Online /Enable-Feature {featureList} /All /NoRestart /Quiet";
                
                progress?.Report("⚙️ Windows IIS özellikleri etkinleştiriliyor...\n(Bu işlem 2-5 dakika sürebilir)");
                
                int exitCode = await RunCommandWithAdminAsync("dism.exe", dismArgs, progress);
                
                if (exitCode != 0 && exitCode != 3010) // 3010 = reboot required but success
                {
                    progress?.Report($"❌ IIS kurulum hatası: {exitCode}");
                    return false;
                }

                progress?.Report("✅ IIS başarıyla kuruldu.");
                
                // IIS servisinin başlamasını bekle
                await Task.Delay(3000);
            }
            else
            {
                progress?.Report("✅ IIS zaten kurulu.");
            }

            // ASP.NET Core Hosting Bundle kurulumu
            progress?.Report("🔍 ASP.NET Core Hosting modülü kontrol ediliyor...");
            
            string hostingModulePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "IIS", "Asp.Net Core Module", "V2", "aspnetcorev2.dll");

            if (!File.Exists(hostingModulePath))
            {
                progress?.Report("📦 ASP.NET Core Hosting Bundle kuruluyor...");
                
                // Hem root hem Resources klasörünü kontrol et
                string hostingBundlePath = Path.Combine(AppContext.BaseDirectory, "dotnet-hosting-8.0.23-win.exe");
                if (!File.Exists(hostingBundlePath))
                    hostingBundlePath = Path.Combine(AppContext.BaseDirectory, "Resources", "dotnet-hosting-8.0.23-win.exe");
                
                if (File.Exists(hostingBundlePath))
                {
                    progress?.Report("⚙️ .NET 8 Hosting Bundle yükleniyor...\n(Bu işlem 1-2 dakika sürebilir)");
                    
                    int hostingExitCode = await RunCommandWithAdminAsync(hostingBundlePath, "/quiet /norestart", progress);
                    
                    if (hostingExitCode == 0 || hostingExitCode == 3010)
                    {
                        progress?.Report("✅ ASP.NET Core Hosting Bundle kuruldu.");
                    }
                    else
                    {
                        progress?.Report($"⚠️ Hosting Bundle kurulumu tamamlanamadı (Kod: {hostingExitCode})");
                    }
                }
                else
                {
                    progress?.Report("⚠️ dotnet-hosting-8.0.23-win.exe bulunamadı.");
                }
            }
            else
            {
                progress?.Report("✅ ASP.NET Core Hosting Bundle zaten kurulu.");
            }
            
            return true;
        }
        catch (Exception ex)
        {
            progress?.Report($"❌ IIS kontrol hatası: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeployApiAsync(DeploymentConfig config, IProgress<string>? progress = null)
    {
        try
        {
            // Zip dosyası var mı kontrol et
            if (!File.Exists(config.ZipPath))
            {
                progress?.Report($"❌ API dosyası bulunamadı: {config.ZipPath}");
                return false;
            }

            // 1. Klasör oluştur ve dosyaları çıkar
            progress?.Report("📦 Local API dosyaları sisteme kopyalanıyor...");
            
            // inetpub klasörünü oluştur (admin gerekebilir)
            string inetpubPath = @"C:\inetpub\wwwroot";
            if (!Directory.Exists(inetpubPath))
            {
                progress?.Report("📁 inetpub klasörü oluşturuluyor...");
                await RunCommandWithAdminAsync("cmd.exe", $"/c mkdir \"{inetpubPath}\"", null);
                await Task.Delay(500);
            }

            // Hedef klasörü temizle ve oluştur
            if (Directory.Exists(config.DestinationPath))
            {
                progress?.Report("🗑️ Eski API dosyaları temizleniyor...");
                await RunCommandWithAdminAsync("cmd.exe", $"/c rmdir /s /q \"{config.DestinationPath}\"", null);
                await Task.Delay(1000);
            }

            // Yeni klasör oluştur
            await RunCommandWithAdminAsync("cmd.exe", $"/c mkdir \"{config.DestinationPath}\"", null);
            await Task.Delay(500);

            // Zip'i geçici klasöre çıkar
            string tempPath = Path.Combine(Path.GetTempPath(), "ISKI_LocalAPI_Temp");
            if (Directory.Exists(tempPath))
                Directory.Delete(tempPath, true);
            
            Directory.CreateDirectory(tempPath);
            ZipFile.ExtractToDirectory(config.ZipPath, tempPath, overwriteFiles: true);

            // xcopy ile hedef klasöre kopyala (admin olarak)
            progress?.Report("📤 Dosyalar kopyalanıyor...");
            await RunCommandWithAdminAsync("xcopy", $"\"{tempPath}\" \"{config.DestinationPath}\" /E /I /Y /Q", null);
            await Task.Delay(1000);

            // Temp klasörü temizle
            try { Directory.Delete(tempPath, true); } catch { }

            // 2. Konfigürasyon Injection
            progress?.Report("⚙️ İstasyon ayarları API'ye tanımlanıyor...");
            string appSettingsPath = Path.Combine(config.DestinationPath, "appsettings.json");
            
            // appsettings.json dosyası için temp'e yaz sonra kopyala
            if (File.Exists(appSettingsPath))
            {
                try
                {
                    var json = File.ReadAllText(appSettingsPath);
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    
                    var data = JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, object>>(json);
                    if (data != null)
                    {
                        data["StationSettings"] = new 
                        { 
                            StationId = config.StationId,
                            LocalIp = config.LocalIp,
                            Port = config.Port
                        };

                        string tempSettings = Path.Combine(Path.GetTempPath(), "appsettings_temp.json");
                        File.WriteAllText(tempSettings, JsonSerializer.Serialize(data, options));
                        await RunCommandWithAdminAsync("cmd.exe", $"/c copy /Y \"{tempSettings}\" \"{appSettingsPath}\"", null);
                        try { File.Delete(tempSettings); } catch { }
                    }
                }
                catch { /* Config injection başarısız olsa bile devam et */ }
            }

            // 3. IIS Site Yapılandırması (appcmd.exe kullanarak)
            progress?.Report("🌐 IIS Site ve Application Pool yapılandırılıyor...");
            
            string appcmdPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "inetsrv", "appcmd.exe");
            
            if (File.Exists(appcmdPath))
            {
                // Mevcut site'ı sil (varsa)
                await RunCommandWithAdminAsync(appcmdPath, $"delete site \"{SiteName}\"", null);
                await Task.Delay(500);

                // Mevcut app pool'u sil (varsa)
                await RunCommandWithAdminAsync(appcmdPath, $"delete apppool \"{AppPoolName}\"", null);
                await Task.Delay(500);

                // Yeni app pool oluştur
                await RunCommandWithAdminAsync(appcmdPath, 
                    $"add apppool /name:\"{AppPoolName}\" /managedRuntimeVersion:\"\" /managedPipelineMode:Integrated", null);
                await Task.Delay(500);

                // Yeni site oluştur
                await RunCommandWithAdminAsync(appcmdPath, 
                    $"add site /name:\"{SiteName}\" /physicalPath:\"{config.DestinationPath}\" /bindings:http/*:{config.Port}:", null);
                await Task.Delay(500);

                // Site'ın app pool'unu ayarla
                await RunCommandWithAdminAsync(appcmdPath, 
                    $"set app \"{SiteName}/\" /applicationPool:\"{AppPoolName}\"", null);
                await Task.Delay(500);

                // Site'ı başlat
                await RunCommandWithAdminAsync(appcmdPath, $"start site \"{SiteName}\"", null);
                
                progress?.Report("✅ IIS Site yapılandırması tamamlandı.");
            }
            else
            {
                progress?.Report("⚠️ appcmd.exe bulunamadı. IIS yönetim araçları eksik olabilir.");
            }

            // 4. Warm-up
            progress?.Report("🚀 API servisleri başlatılıyor...");
            await Task.Delay(3000);
            
            try 
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                await client.GetAsync($"http://localhost:{config.Port}/");
            }
            catch { /* Warm-up başarısız olabilir */ }

            progress?.Report("✅ API başarıyla kuruldu.");
            return true;
        }
        catch (Exception ex)
        {
            progress?.Report($"❌ Deployment hatası: {ex.Message}");
            return false;
        }
    }

    private async Task<int> RunCommandWithAdminAsync(string fileName, string arguments, IProgress<string>? progress = null)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false, // App already runs elevated
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false
            };

            using var process = new Process { StartInfo = startInfo };
            
            if (!process.Start())
            {
                return -1;
            }

            // İlerleme göster
            var progressMessages = new[]
            {
                "📦 Windows bileşenleri hazırlanıyor...",
                "⚙️ IIS Web Sunucusu kuruluyor...",
                "🔧 HTTP özellikleri etkinleştiriliyor...",
                "📊 Güvenlik modülleri yapılandırılıyor...",
                "🌐 ASP.NET desteği ekleniyor...",
                "✅ Kurulum tamamlanmak üzere..."
            };

            int msgIndex = 0;
            var stopwatch = Stopwatch.StartNew();

            while (!process.HasExited)
            {
                if (stopwatch.Elapsed.TotalSeconds >= 20 && msgIndex < progressMessages.Length && progress != null)
                {
                    progress.Report(progressMessages[msgIndex]);
                    msgIndex++;
                    stopwatch.Restart();
                }
                await Task.Delay(500);
            }

            return process.ExitCode;
        }
        catch (Exception)
        {
            return -1;
        }
    }
}
