using System.Security.Cryptography;
using System.Text;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Utilities;

/// <summary>
/// Utility class for SAIS API password hashing.
/// SAIS API requires passwords to be hashed twice with MD5.
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Hashes the password twice with MD5 as required by SAIS API.
    /// </summary>
    /// <param name="plainPassword">The plain text password</param>
    /// <returns>Double MD5 hashed password in lowercase hexadecimal format</returns>
    public static string HashPassword(string plainPassword)
    {
        if (string.IsNullOrEmpty(plainPassword))
            return string.Empty;

        // First MD5 hash
        var firstHash = ComputeMD5Hash(plainPassword);
        
        // Second MD5 hash
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
