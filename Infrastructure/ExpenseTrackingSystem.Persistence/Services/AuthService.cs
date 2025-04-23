using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Abstractions.Token;
using ExpenseTrackingSystem.Application.Dtos.Token;
using ExpenseTrackingSystem.Application.Helpers;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IUserService _userService;
		private readonly IMailService _mailService;

		public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, IMailService mailService,
			ITokenService tokenService, SignInManager<AppUser> signInManager, IUserService userService)
		{
			_configuration = configuration;
			_userManager = userManager;
			_tokenService = tokenService;
			_signInManager = signInManager;
			_userService = userService;
			_mailService = mailService;
		}
		public async Task<TokenDto> LoginAsync(string email, string password, int accessTokenLifeTime)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				throw new Exception("Not found user");

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			if (result.Succeeded)
			{
				TokenDto token = _tokenService.CreateAccessToken(accessTokenLifeTime, user);
				await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
				return token;
			}

			throw new Exception("Authentication Error");
		}

		public async Task PasswordResetAsnyc(string email)
		{
			AppUser user = await _userManager.FindByEmailAsync(email);

			if (user != null)
			{
				string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

				resetToken = resetToken.UrlEncode();

				await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
			}
		}

		public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
			{
				TokenDto token = _tokenService.CreateAccessToken(15, user);
				await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
				return token;
			}
			else
				throw new Exception("Not Found User Exception");
		}

		public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
				resetToken = Encoding.UTF8.GetString(tokenBytes);

				return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
			}
			return false;
		}
	}
}
