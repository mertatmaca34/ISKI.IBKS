using ISKI.IBKS.Application.Services.Sql;
using Microsoft.Win32;

namespace ISKI.IBKS.Infrastructure.Services.Sql;

public class SqlCheckService : ISqlCheckService
{
    public bool IsSqlExpressInstalled()
    {
        try
        {
            // Check for SQL Server Express instance in Registry
            // Common location for SQL Express: HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL"))
            {
                if (key != null)
                {
                    var instances = key.GetValueNames();
                    foreach (var instance in instances)
                    {
                        if (instance.Equals("SQLEXPRESS", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }

            // Fallback check or 64-bit registry view if needed
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (var key = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL"))
                {
                    if (key != null)
                    {
                        var instances = key.GetValueNames();
                        foreach (var instance in instances)
                        {
                            if (instance.Equals("SQLEXPRESS", StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            // If registry access fails, assume not installed or handle error
            return false;
        }

        return false;
    }
}
