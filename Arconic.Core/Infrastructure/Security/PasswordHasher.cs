using System.Security.Cryptography;
using Arconic.Core.Abstractions.Security;

namespace Arconic.Core.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA256;
    private const string Separator = ":";

    public string GetHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName, KeySize);
        var result = string.Join(Separator, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        return result;
    }
    public bool Verify(string? passwordHash, string? inputPassword)
    {
        if (string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(inputPassword)) return false;
        var parts = passwordHash.Split(Separator);
        if (parts.Length != 2) return false;
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, HashAlgorithmName, KeySize);
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}