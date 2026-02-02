using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using System.Security.Cryptography;
using System.Text;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Utilities;

public static class PasswordHasher
{
    public static string HashPassword(string plainPassword)
    {
        if (string.IsNullOrEmpty(plainPassword))
            return string.Empty;

        var firstHash = ComputeMD5Hash(plainPassword);

        var secondHash = ComputeMD5Hash(firstHash);

        return secondHash;
    }

    private static string ComputeMD5Hash(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.Default.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        var builder = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            builder.Append(hashBytes[i].ToString("x2"));
        }

        return builder.ToString();
    }
}

