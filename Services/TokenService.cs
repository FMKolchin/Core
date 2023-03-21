using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace _4.Services
{
    public static class TokenService
    {
        private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Esty&FaigaMiriamAreDOingHomeWorkNow19:51"));
        private static string issuer = "https://task-management.com";
        public static SecurityToken GetToken(List<Claim> claims) =>
            new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddHours(10.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

        public static TokenValidationParameters GetTokenValidationParameters() =>
            new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Esty&FaigaMiriamAreDOingHomeWorkNow19:51")),
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };

        public static string WriteToken(SecurityToken token) =>
            new JwtSecurityTokenHandler().WriteToken(token);

    //        public static void GetTokenId(string token){
    //     string splitToken = token.Split(" ")[2];

    // }

        public static string DecodeToken(string token){
            token = token.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token) as JwtSecurityToken;
            var id = decodedValue.Claims.First(claim => claim.Type == "Id").Value;
            return id;
        }
    }
    
}