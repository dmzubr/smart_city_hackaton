using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace UG.WebApi.Auth
{
    public static class SecurityAlgorithmService
    {
        private static readonly string secretKey = "Botans_Rules_Motherfucker!!!_782387327842378237843278^@";

        public static SymmetricSecurityKey signingKey { get { return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)); } }
        public static string securityAlgorithm = SecurityAlgorithms.HmacSha256;

        public static TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            // The signing key must match!
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,

            // Validate the JWT Issuer (iss) claim
            ValidateIssuer = true,
            ValidIssuer = "UGIssuer",

            // Validate the JWT Audience (aud) claim
            ValidateAudience = true,
            ValidAudience = "UGAudience",

            // Validate the token expiry
            ValidateLifetime = true,

            // If you want to allow a certain amount of clock drift, set that here:
            ClockSkew = TimeSpan.Zero
        };

        public static AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = null;
            SecurityToken validToken = null;

            try
            {
                principal = handler.ValidateToken(protectedText, tokenValidationParameters, out validToken);

                var validJwt = validToken as JwtSecurityToken;

                if (validJwt == null)
                {
                    throw new ArgumentException("Invalid JWT");
                }

                if (!validJwt.Header.Alg.Equals(securityAlgorithm, StringComparison.Ordinal))
                {
                    throw new ArgumentException($"Algorithm must be '{securityAlgorithm }'");
                }

                // Additional custom validation of JWT claims here (if any)
            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            // Validation passed. Return a valid AuthenticationTicket:
            return new AuthenticationTicket(principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties(), "Cookie");
        }
    }
}
