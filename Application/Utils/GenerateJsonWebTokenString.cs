﻿using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utils
{
    public class GenerateJsonWebTokenString
    {
        private IUnitOfWork _unitOfWork;

        public GenerateJsonWebTokenString(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GenerateJsonWebToken(Account account, IConfiguration appSettingConfiguration, string secretKey, DateTime now)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var rolename = _unitOfWork.RoleRepository.GetRoleName((Guid)account.RoleId);

            var claims = new[]
            {
            new Claim("Id", account.Id.ToString()),
            new Claim("Username", account.UserName),
            new Claim("Email", account.Email),
            new Claim("role", rolename),
        };

            var token = new JwtSecurityToken(
                issuer: appSettingConfiguration.GetSection("JWTSection:Issuer").Value,
                audience: appSettingConfiguration.GetSection("JWTSection:Audience").Value,
                claims: claims,
                expires: now.AddMinutes(180),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
