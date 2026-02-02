using Microsoft.Win32;

namespace ISKI.IBKS.Infrastructure.Services.Sql;

public static class SqlHelper
{
    public static bool IsSqlExpressInstalled()
    {
        try
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server"))
            {
                if (key != null)
                {
                    string[] subKeys = key.GetSubKeyNames();
                    foreach (var subKey in subKeys)
                    {
                        if (subKey.Contains("SQLEXPRESS", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        catch
        {
            // Fallback or log
        }
        return false;
    }
}
