using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Users.Entities;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace PropertyManager.Infrastructure.Implementations.Security.Authentication
{
    public class TokenProvider(IConfiguration configuration) : ITokenProvider
    {
        public string Create(Owner owner) 
        {
            string secretKey = configuration["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                ([
                    new Claim(JwtRegisteredClaimNames.Sub, owner.IdOwner.ToString(CultureInfo.InvariantCulture)),
                    new Claim(JwtRegisteredClaimNames.UniqueName, owner.Name)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
