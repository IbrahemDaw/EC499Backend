using System.Security.Cryptography;
using System.Text;

namespace Shared;

public static class PasswordHash
{
    public static string HashPass(this string Pass)
    {
        var bytes = new UTF8Encoding().GetBytes(Pass);
        var hashBytes = MD5.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
