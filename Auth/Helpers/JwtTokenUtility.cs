// Ignore Spelling: auth Jwt

using Microsoft.Extensions.Options;

namespace Auth.Helpers;

/*
 appsettings.json should contain
 "JWT": {
"ValidAudience": ,
"ValidIssuer": ,
"Secret": 
},
 */
public class JwtTokenUtility(IOptions<JwtOptions> options)
{
    private readonly JwtOptions jwtOptions = options.Value;
    public static List<Claim> GetUserClaims(User User)
    {
        var userClaims = new List<Claim>()
        {
            new Claim("id", User.Id.ToString()),
            new Claim(ClaimTypes.Name, User.UserName),
        };
        foreach (var permission in User.Roles.SelectMany(x=>x.Permissions))
        {
            userClaims.Add(new Claim("permission", permission.Id.ToString()));
        }

        return userClaims;
    }

    public string GenerateAccessToken(User User)
    {
        var claims = GetUserClaims(User);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));

        var jwtToken = new JwtSecurityToken(
            issuer: jwtOptions.ValidIssuer,
            audience: jwtOptions.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(jwtOptions.LifeSpan),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

}
