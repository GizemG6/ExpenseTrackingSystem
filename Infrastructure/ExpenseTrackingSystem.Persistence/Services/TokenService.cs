using ExpenseTrackingSystem.Application.Abstractions.Token;
using ExpenseTrackingSystem.Application.Dtos.Token;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<AppUser> _userManager;

		public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
		{
			_configuration = configuration;
			_userManager = userManager;
		}
		public TokenDto CreateAccessToken(int second, AppUser appUser)
		{
			TokenDto token = new();
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
			token.Expiration = DateTime.UtcNow.AddSeconds(second);
			var role = _userManager.GetRolesAsync(appUser).Result.FirstOrDefault();

			var claims = new List<Claim>
							{
								new(ClaimTypes.Name, appUser.UserName),
								new(ClaimTypes.NameIdentifier, appUser.Id)
							};
			if (!string.IsNullOrEmpty(role))
				claims.Add(new Claim(ClaimTypes.Role, role));

			JwtSecurityToken securityToken = new(
				audience: _configuration["Token:Audience"],
				issuer: _configuration["Token:Issuer"],
				expires: token.Expiration,
				notBefore: DateTime.UtcNow,
				signingCredentials: signingCredentials,
				claims: claims);

			JwtSecurityTokenHandler tokenHandler = new();
			token.AccessToken = tokenHandler.WriteToken(securityToken);
			token.RefreshToken = CreateRefreshToken();

			return token;
		}

		public string CreateRefreshToken()
		{
			byte[] number = new byte[32];
			using RandomNumberGenerator random = RandomNumberGenerator.Create();
			random.GetBytes(number);
			return Convert.ToBase64String(number);
		}
	}
}
