using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.DataSeed;

public static class UserSeed
{
    private static string HashPass(this string Pass)
    {
        var bytes = new UTF8Encoding().GetBytes(Pass);
        var hashBytes = MD5.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
    public static List<User> GetUserSeed()
    {
        return
        [
            new() {
                Id = 1,
                CreatedAt = DateTime.Now,
                Email = "admin@email.com",
                FullName = "Admin",
                UserName = "admin123",
                IsEnabled = true,
                IsDeleted = false,
                RequirePasswordChange = true,
                PasswordHash = HashPass("admin123"),
                PhoneNumber = "123",
                UpdatedAt = DateTime.Now,
             }
        ];
    }
}
