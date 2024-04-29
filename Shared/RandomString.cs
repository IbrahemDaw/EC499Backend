using System.Text;

namespace Shared;

public static class RandomString
{
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(0, chars.Length);
            char randomChar = chars[randomIndex];
            stringBuilder.Append(randomChar);
        }

        return stringBuilder.ToString();
    }
}
