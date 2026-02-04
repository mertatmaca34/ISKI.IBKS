using System.Diagnostics;
using ISKI.IBKS.Application.Services.Sql;
using Microsoft.Win32;

namespace ISKI.IBKS.Infrastructure.Services.Sql;

/// <summary>
/// SQL Server Express kurulum servisi
/// Registry kontrolü ve otomatik kurulum sağlar
/// </summary>
public class SqlInstallationService : ISqlInstallationService
{
    private const string SqlExpressInstallerPath = "Resources\\SQLEXPR_x64_ENU.exe";
    private const string RegistryPath = @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL";
    private const string InstanceName = "SQLEXPRESS";

    /// <inheritdoc/>
    public bool IsSqlExpressInstalled()
    {
        try
        {
            // Method 1: Check registry for instance
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = baseKey.OpenSubKey(RegistryPath))
            {
                if (HasSqlExpressInstance(key))
                    return true;
            }

            // Method 2: Check 32-bit registry view
            using (var key = Registry.LocalMachine.OpenSubKey(RegistryPath))
            {
                if (HasSqlExpressInstance(key))
                    return true;
            }

            // Method 3: Check if SQL Server service exists
            var services = System.ServiceProcess.ServiceController.GetServices();
            if (services.Any(s => s.ServiceName.Equals("MSSQL$SQLEXPRESS", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<SqlInstallationResult> InstallSqlExpressAsync(
        IProgress<string>? progress = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Adım 1: Kurulum dosyası kontrolü
            progress?.Report("Kurulum dosyası kontrol ediliyor...");
            
            string exePath = Path.Combine(AppContext.BaseDirectory, SqlExpressInstallerPath);
            
            if (!File.Exists(exePath))
            {
                return new SqlInstallationResult(
                    Success: false,
                    ExitCode: -1,
                    ErrorMessage: $"SQL Server kurulum dosyası bulunamadı: {exePath}");
            }

            // Adım 2: Konfigürasyon dosyası oluştur (INI dosyası yöntemi - en güvenilir)
            progress?.Report("Kurulum yapılandırması hazırlanıyor...");
            
            string configPath = Path.Combine(Path.GetTempPath(), "SqlExpressConfig.ini");
            await CreateConfigFileAsync(configPath);
            
            await Task.Delay(500, cancellationToken);

            // Adım 3: Önce sessiz çıkartma yap (Bootstrapper doğrudan /CONFIGURATIONFILE desteklemez)
            progress?.Report("📦 Kurulum dosyaları çıkartılıyor...");
            
            string extractPath = Path.Combine(Path.GetTempPath(), "SQLEXPRESS_Setup_" + Guid.NewGuid().ToString("N").Substring(0, 8));
            
            // Önceden varsa temizle
            if (Directory.Exists(extractPath))
            {
                try { Directory.Delete(extractPath, true); } catch { }
            }
            
            // Sessiz çıkartma - /Q /X parametreleri
            var extractInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"/Q /X:\"{extractPath}\"",
                UseShellExecute = false, // App already runs elevated
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            progress?.Report("📦 Kurulum dosyaları çıkartılıyor...\n[Bu işlem 1-2 dakika sürebilir]");
            int extractCode = await RunProcessAsync(extractInfo, cancellationToken);
            
            if (extractCode != 0)
            {
                return new SqlInstallationResult(
                    Success: false,
                    ExitCode: extractCode,
                    ErrorMessage: $"Kurulum dosyaları çıkartılamadı. Hata Kodu: {extractCode}");
            }

            // Adım 4: Setup.exe'yi bul
            progress?.Report("⚙️ Setup dosyası aranıyor...");
            string setupExe = FindSetupExe(extractPath);
            
            if (string.IsNullOrEmpty(setupExe))
            {
                try { Directory.Delete(extractPath, true); } catch { }
                return new SqlInstallationResult(
                    Success: false,
                    ExitCode: -4,
                    ErrorMessage: "Setup.exe bulunamadı. Kurulum dosyası bozuk olabilir.");
            }

            // Adım 5: Gerçek kurulum - setup.exe'yi komut satırı parametreleri ile çalıştır
            // /Q = Tamamen sessiz (GUI yok), /SECURITYMODE=SQL = Mixed mode auth, /SAPWD = SA şifresi
            progress?.Report("🔧 SQL Server Express kuruluyor...");
            
            string installArgs = string.Join(" ",
                "/Q",                                          // Tamamen sessiz mod (GUI yok)
                "/ACTION=Install",
                "/FEATURES=SQLENGINE",
                "/INSTANCENAME=SQLEXPRESS",
                "/SQLSVCACCOUNT=\"NT AUTHORITY\\SYSTEM\"",
                "/SQLSYSADMINACCOUNTS=\"BUILTIN\\Administrators\"",
                "/SECURITYMODE=SQL",                           // Mixed mode authentication
                "/SAPWD=\"1Q2w3e\"",                           // SA şifresi
                "/TCPENABLED=1",
                "/NPENABLED=0",
                "/IACCEPTSQLSERVERLICENSETERMS",
                "/UPDATEENABLED=0",
                "/SKIPRULES=RebootRequiredCheck"
            );
            
            var startInfo = new ProcessStartInfo
            {
                FileName = setupExe,
                Arguments = installArgs,
                UseShellExecute = false, // App already runs elevated
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            int exitCode = await RunProcessWithProgressAsync(startInfo, progress, cancellationToken);

            // Temizlik
            try { Directory.Delete(extractPath, true); } catch { }

            // Temizlik
            try { File.Delete(configPath); } catch { }

            // Sonuç değerlendirme
            if (exitCode == 0 || exitCode == 3010)
            {
                progress?.Report("SQL Server Express kurulumu başarıyla tamamlandı.");
                return new SqlInstallationResult(Success: true, ExitCode: exitCode);
            }

            // Bilinen hata kodlarını kontrol et
            string errorMessage = GetErrorMessage(exitCode);
            progress?.Report(errorMessage);
            
            return new SqlInstallationResult(
                Success: false,
                ExitCode: exitCode,
                ErrorMessage: errorMessage);
        }
        catch (OperationCanceledException)
        {
            return new SqlInstallationResult(
                Success: false,
                ExitCode: -2,
                ErrorMessage: "Kurulum kullanıcı tarafından iptal edildi.");
        }
        catch (Exception ex)
        {
            return new SqlInstallationResult(
                Success: false,
                ExitCode: -3,
                ErrorMessage: $"Kurulum sırasında beklenmeyen hata: {ex.Message}");
        }
    }

    /// <summary>
    /// SQL Server sessiz kurulum için INI konfigürasyon dosyası oluşturur
    /// </summary>
    private static async Task CreateConfigFileAsync(string configPath)
    {
        var config = @"
[OPTIONS]
ACTION=""Install""
FEATURES=SQLENGINE
INSTANCENAME=""SQLEXPRESS""
INSTANCEID=""SQLEXPRESS""
SQLSVCACCOUNT=""NT AUTHORITY\SYSTEM""
SQLSYSADMINACCOUNTS=""BUILTIN\Administrators""
AGTSVCSTARTUPTYPE=""Disabled""
SQLTEMPDBFILECOUNT=""1""
SQLTEMPDBDIR=""C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA""
SQLTEMPDBLOGDIR=""C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA""
SQLUSERDBDIR=""C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA""
SQLUSERDBLOGDIR=""C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA""
SQLBACKUPDIR=""C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\Backup""
TCPENABLED=""1""
NPENABLED=""0""
BROWSERSVCSTARTUPTYPE=""Disabled""
UpdateEnabled=""False""
";
        await File.WriteAllTextAsync(configPath, config.Trim());
    }

    /// <summary>
    /// Çıkartılan klasörde setup.exe dosyasını bulur
    /// </summary>
    private static string FindSetupExe(string extractPath)
    {
        if (!Directory.Exists(extractPath))
            return string.Empty;

        // Doğrudan kök klasörde ara
        string setupExe = Path.Combine(extractPath, "setup.exe");
        if (File.Exists(setupExe))
            return setupExe;

        // Bilinen alt klasörlerde ara
        var knownFolders = new[] { "SQLEXPR_x64_ENU", "SQLEXPR", "x64" };
        foreach (var folder in knownFolders)
        {
            setupExe = Path.Combine(extractPath, folder, "setup.exe");
            if (File.Exists(setupExe))
                return setupExe;
        }

        // Tüm alt klasörlerde recursive ara
        try
        {
            var files = Directory.GetFiles(extractPath, "setup.exe", SearchOption.AllDirectories);
            if (files.Length > 0)
                return files[0];
        }
        catch { }

        return string.Empty;
    }
    /// <summary>
    /// Bilinen hata kodları için açıklayıcı mesaj döndürür
    /// </summary>
    private static string GetErrorMessage(int exitCode)
    {
        return exitCode switch
        {
            -2067922935 => "Bekleyen bir yeniden başlatma var. Lütfen bilgisayarı yeniden başlatıp tekrar deneyin.",
            -2061893628 => "Kurulum dosyası geçersiz veya bozuk olabilir. Lütfen SQLEXPR_x64_ENU.exe dosyasını Microsoft'tan tekrar indirin.",
            -2068052081 => "SQL Server zaten kurulu veya önceki kurulumdan kalan dosyalar var.",
            1602 => "Kurulum kullanıcı tarafından iptal edildi.",
            3010 => "Kurulum başarılı, ancak yeniden başlatma gerekiyor.",
            _ => $"Kurulum başarısız oldu. Hata Kodu: {exitCode}\n\nÇözüm önerileri:\n1. Bilgisayarı yeniden başlatın\n2. Yönetici olarak çalıştırın\n3. SQL Server'ı manuel olarak kurun"
        };
    }

    /// <summary>
    /// Process'i async olarak çalıştırır
    /// </summary>
    private static async Task<int> RunProcessAsync(ProcessStartInfo startInfo, CancellationToken cancellationToken)
    {
        using var process = new Process { StartInfo = startInfo };
        
        if (!process.Start())
        {
            throw new InvalidOperationException("SQL Server kurulum işlemi başlatılamadı.");
        }

        await process.WaitForExitAsync(cancellationToken);
        
        return process.ExitCode;
    }

    /// <summary>
    /// Process'i async olarak çalıştırır ve ilerleme durumunu günceller
    /// </summary>
    private static async Task<int> RunProcessWithProgressAsync(
        ProcessStartInfo startInfo, 
        IProgress<string>? progress, 
        CancellationToken cancellationToken)
    {
        using var process = new Process { StartInfo = startInfo };
        
        if (!process.Start())
        {
            throw new InvalidOperationException("SQL Server kurulum işlemi başlatılamadı.");
        }

        // İlerleme mesajları (her 15 saniyede bir güncellenir)
        var progressMessages = new[]
        {
            "📦 Kurulum dosyaları çıkartılıyor...",
            "⚙️ SQL Server bileşenleri hazırlanıyor...",
            "🔧 Veritabanı motoru kuruluyor...",
            "📊 Sistem veritabanları oluşturuluyor...",
            "🔐 Güvenlik ayarları yapılandırılıyor...",
            "🌐 Ağ protokolleri etkinleştiriliyor...",
            "📝 Kayıt defteri ayarları yazılıyor...",
            "🚀 SQL Server servisi başlatılıyor...",
            "✅ Son kontroller yapılıyor...",
            "⏳ Kurulum tamamlanmak üzere..."
        };

        int messageIndex = 0;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // Process tamamlanana kadar bekle ve ilerleme göster
        while (!process.HasExited)
        {
            // Her 15 saniyede bir mesaj güncelle
            if (stopwatch.Elapsed.TotalSeconds >= 15)
            {
                stopwatch.Restart();
                if (messageIndex < progressMessages.Length)
                {
                    progress?.Report(progressMessages[messageIndex]);
                    messageIndex++;
                }
            }

            // 1 saniye bekle ve iptal kontrolü yap
            try
            {
                await Task.Delay(1000, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                try { process.Kill(); } catch { }
                throw;
            }
        }

        return process.ExitCode;
    }

    /// <summary>
    /// Registry anahtarında SQLEXPRESS instance'ı var mı kontrol eder
    /// </summary>
    private static bool HasSqlExpressInstance(RegistryKey? key)
    {
        if (key == null) return false;

        var instances = key.GetValueNames();
        return instances.Any(instance => 
            instance.Equals(InstanceName, StringComparison.OrdinalIgnoreCase));
    }
}
