﻿using FinancialChat.Domain.Models;
using FinancialChat.Logic.Interface;
using FinancialChat.Logic.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialChat.Logic
{
    /// <summary>
    /// Contain methods for generating authorization tokens
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenOptions> _configuration;
        public TokenService(IOptions<TokenOptions> configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JSON Web Token by providing an authenticated user.
        /// </summary>
        public JsonWebToken GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration.Value.ExpirationDays)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                NotBefore = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JsonWebToken(tokenHandler.WriteToken(token), tokenDescriptor.Expires.Value.Ticks);
        }
    }
}
