namespace Server.Instructure.TokenHelper;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration configuration;

    public TokenGenerator(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string? GenerateToken()
    {
        var secretKey =
            configuration["JwtSettings:SecretKey"];

        var expireTime =
            configuration["JwtSettings:ExpireTime"];

        if (secretKey is null || expireTime == null)
        {
            return null;
        }

        var securityKey =
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials =
            new SigningCredentials(key: securityKey, algorithm: SecurityAlgorithms.HmacSha256);

        var claims =
            new List<Claim>()
            {
                new Claim(type:ClaimTypes.NameIdentifier,value: 1.ToString()),

                new Claim(type:ClaimTypes.Name , value: "Erfan-Edalati"),

                new Claim(type:ClaimTypes.Role , value: "Admin"),
            };

        var securityToken =
            new JwtSecurityToken(
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(expireTime)));

        var token =
            new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}
