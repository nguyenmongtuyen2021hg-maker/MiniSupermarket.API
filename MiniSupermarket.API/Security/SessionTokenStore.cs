using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MiniSupermarket.API.Security;

public sealed class SessionTokenStore
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
    private readonly byte[] _signingKey;

    public SessionTokenStore(IConfiguration configuration)
    {
        var secret = configuration["JwtSettings:Secret"];
        if (string.IsNullOrWhiteSpace(secret) || Encoding.UTF8.GetByteCount(secret) < 32)
        {
            throw new InvalidOperationException("JwtSettings:Secret must contain at least 32 UTF-8 bytes.");
        }

        _signingKey = Encoding.UTF8.GetBytes(secret);
    }

    public string Create(string username, string role)
    {
        var payload = JsonSerializer.SerializeToUtf8Bytes(
            new SessionIdentity(username, role, DateTimeOffset.UtcNow.Add(TokenLifetime)));
        var signature = HMACSHA256.HashData(_signingKey, payload);
        return $"{Encode(payload)}.{Encode(signature)}";
    }

    public bool TryGet(string token, out SessionIdentity identity)
    {
        identity = null!;
        if (string.IsNullOrWhiteSpace(token) || token.Length > 4096)
        {
            return false;
        }

        var separator = token.IndexOf('.');
        if (separator <= 0 || separator == token.Length - 1 || token.IndexOf('.', separator + 1) >= 0)
        {
            return false;
        }

        try
        {
            var payload = Decode(token[..separator]);
            var suppliedSignature = Decode(token[(separator + 1)..]);
            var expectedSignature = HMACSHA256.HashData(_signingKey, payload);
            if (suppliedSignature.Length != expectedSignature.Length ||
                !CryptographicOperations.FixedTimeEquals(suppliedSignature, expectedSignature))
            {
                return false;
            }

            var parsed = JsonSerializer.Deserialize<SessionIdentity>(payload);
            if (parsed == null || string.IsNullOrWhiteSpace(parsed.Username) ||
                string.IsNullOrWhiteSpace(parsed.Role) || parsed.ExpiresAt <= DateTimeOffset.UtcNow)
            {
                return false;
            }

            identity = parsed;
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    private static string Encode(byte[] value) =>
        Convert.ToBase64String(value).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    private static byte[] Decode(string value)
    {
        var base64 = value.Replace('-', '+').Replace('_', '/');
        base64 += (base64.Length % 4) switch
        {
            2 => "==",
            3 => "=",
            0 => string.Empty,
            _ => throw new FormatException("Invalid base64url value.")
        };
        return Convert.FromBase64String(base64);
    }

    public sealed record SessionIdentity(string Username, string Role, DateTimeOffset ExpiresAt);
}
