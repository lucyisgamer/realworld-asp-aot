using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator : IJwtTokenGenerator {
    private static readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
    private static readonly SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
    private static readonly SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    public string GenerateJwtToken(string username) {
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "Realworld",
            audience: "Realworld_api",
            claims: [new Claim("Username", username)],
            expires: DateTime.Now.AddDays(2),
            signingCredentials: creds);
        return jwtSecurityTokenHandler.WriteToken(token);
    } 
}