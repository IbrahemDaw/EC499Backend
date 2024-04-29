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

    public static string ParseBearerToken(StringValues token)
    {
        var stringToken = token.ToString();
        if (stringToken == null)
        {
            return string.Empty;
        }
        if (!stringToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return stringToken;
        }

        return stringToken.Split(" ").ElementAt(1).Trim();
    }
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

    public bool ValidateAccessToken(string Token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false, // you may want to change this 
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.ValidIssuer,
            ValidAudience = jwtOptions.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };

        SecurityToken validatedToken;

        try
        {
            IPrincipal principal = tokenHandler.ValidateToken(Token, validationParameters, out validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    public List<Claim> GetUserClaimsFromToken(string authToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false, // you may want to change this 
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.ValidIssuer,
            ValidAudience = jwtOptions.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };

        try
        {
            var principal = tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return [];
            }

            return principal.Claims.ToList();
        }
        catch (Exception)
        {
            return [];
        }

    }
    public string GetUserIdFromToken(string authToken)
    {
        var userClaims = GetUserClaimsFromToken(authToken);
        return userClaims.First(t => t.Type == ClaimTypes.NameIdentifier).Value.ToString();
    }

}
