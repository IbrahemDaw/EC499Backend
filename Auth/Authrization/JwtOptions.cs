namespace Auth.Models;

public class JwtOptions
{
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public double LifeSpan { get; set; }
}
