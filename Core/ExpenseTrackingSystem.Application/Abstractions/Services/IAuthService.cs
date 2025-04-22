using ExpenseTrackingSystem.Application.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Services
{
	public interface IAuthService
	{
		Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
		Task<TokenDto> LoginAsync(string email, string password, int accessTokenLifeTime); 
		Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
		Task PasswordResetAsnyc(string email);
	}
}
