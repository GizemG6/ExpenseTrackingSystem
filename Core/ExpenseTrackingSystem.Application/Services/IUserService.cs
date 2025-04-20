using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Services
{
	public interface IUserService
	{
		Task<UserDto> CreateAsync(UserCreateDto model);
		Task<AppUser> GetUserByIdAsync(string userId);
		Task<List<AppUser>> GetAllUsersAsync();
		Task<bool> UpdateUserAsync(AppUser user);
		Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
		Task<bool> DeleteUserAsync(string userId);
		Task AssignRoleToUserAsnyc(string userId, string[] roles);
		Task<string[]> GetRolesToUserAsync(string userId);
	}
}
