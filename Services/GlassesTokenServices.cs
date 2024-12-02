using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace OurApi.Services
{
    public static class GlassesTokenService
    {
         private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));
       private static string issuer = "http://localhost:5071";
        public static SecurityToken GetToken(List<Claim> claims) =>
        new JwtSecurityToken(
    issuer: issuer,
    audience: null, // אפשר להוריד אם לא צריך audience
    claims: claims,
    expires: DateTime.UtcNow.AddDays(30), 
    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
);

        public static bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true, 
                ValidateAudience = false, 
                ValidateIssuer = false, 
                RequireExpirationTime = true, 
                ClockSkew = TimeSpan.Zero 
            };
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return validatedToken.ValidTo <= DateTime.UtcNow;
            }
            catch (SecurityTokenExpiredException)
            {
                return true;
            }
            catch (Exception)
            {
                return true; 
            }
        }
        public static TokenValidationParameters GetTokenValidationParameters()
        {


            return new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = key,
                //ClockSkew = TimeSpan.Zero, // remove delay of token when expire
                LifetimeValidator = LifetimeValidator //TimeSpan.FromDays(30)

            };
        }
        public static string WriteToken(SecurityToken token) =>
    new JwtSecurityTokenHandler().WriteToken(token);


    public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters validationParameters)
{
    if (!expires.HasValue)
        return false;

    return expires.Value > DateTime.UtcNow;
}


         
           // return true;
        }
    }
 






            