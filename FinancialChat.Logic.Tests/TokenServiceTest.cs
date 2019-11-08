using FinancialChat.Logic.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace FinancialChat.Logic.Tests
{
    public class TokenServiceTest
    {
        [Fact]
        public void GenerateJwtTokenTest()
        {
            // Arrange
            var config = Options.Create(new TokenOptions
            {
                Secret = "Rm9OW05aqGtOa9r53tNC",
                ExpirationDays = "1"
            });

            var service = new TokenService(config);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(config.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "user")
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble("1")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                NotBefore = DateTime.UtcNow
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = new JsonWebToken(tokenHandler.WriteToken(securityToken), tokenDescriptor.Expires.Value.Ticks);
            // Act
            var result = service.GenerateJwtToken(new Domain.Models.ApplicationUser { UserName = "user" });

            // Assert
            Assert.Equal(typeof(JsonWebToken), result.GetType());
            Assert.Equal(token.Token, result.Token);
        }
    }
}
