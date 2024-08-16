namespace Arconic.Core.Abstractions.Security;

public interface IPasswordHasher
{
    bool Verify(string? passwordHash, string? inputPassword);
    string GetHash(string password);
}