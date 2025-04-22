using ExpenseTrackingSystem.Application.Dtos.Token;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Token
{
	public interface ITokenService
	{
		TokenDto CreateAccessToken(int second, AppUser appUser);
		string CreateRefreshToken();
	}
}
