﻿using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserCreateResponseDto> CreateAsync(UserCreateDto model);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<List<AppUser>> GetAllUsersAsync();
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<bool> DeleteUserAsync(string userId);
        Task AssignRoleToUserAsnyc(string userId, string role);
        Task<List<UserDto>> GetUsersByRoleAsync(string roleName);
		Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
		Task<bool> UpdateUserByTitleAsync(UpdateUserTitleDto model);
        Task<bool> UpdateUserIbanAsync(UpdateUserIbanDto model);
	}
}
