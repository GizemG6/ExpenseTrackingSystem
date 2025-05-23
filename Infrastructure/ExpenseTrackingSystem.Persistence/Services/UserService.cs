﻿using AutoMapper;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Application.Helpers;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
    public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IMapper _mapper;

		public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public async Task AssignRoleToUserAsnyc(string userId, string role)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				throw new Exception("User not found");

			if (!RoleConstants.AllRoles.Contains(role))
			{
				throw new Exception($"Invalid role: {role}");
			}

			if (!await _roleManager.RoleExistsAsync(role))
			{
				var createRoleResult = await _roleManager.CreateAsync(new AppRole { Name = role });
				if (!createRoleResult.Succeeded)
				{
					throw new Exception($"Could not create role: {role}");
				}
			}

			var addResult = await _userManager.AddToRoleAsync(user, role);
			if (!addResult.Succeeded)
			{
				throw new Exception($"Role could not be assigned: {role}");
			}
		}

		public async Task<UserCreateResponseDto> CreateAsync(UserCreateDto model)
		{
			AppUser user = _mapper.Map<AppUser>(model);

			user.Id = Guid.NewGuid().ToString();
			user.UserName = GenerateValidUsername(model.FullName);

			IdentityResult result = await _userManager.CreateAsync(user, model.Password);

			UserCreateResponseDto response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
				response.Message = "User created successfully.";
			else
			{
				var message = string.Join("\n", result.Errors.Select(e => $"{e.Code} - {e.Description}"));
				throw new Exception(message);
			}

			return response;
		}

		public async Task<bool> DeleteUserAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			user.IsActive = false;
			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<List<AppUser>> GetAllUsersAsync()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task<AppUser> GetUserByIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
				throw new Exception("User not found.");

			return user;
		}

		public async Task<List<UserDto>> GetUsersByRoleAsync(string roleName)
		{
			if (!await _roleManager.RoleExistsAsync(roleName))
				throw new Exception($"Role '{roleName}' does not exist.");

			var users = await _userManager.GetUsersInRoleAsync(roleName);
			return _mapper.Map<List<UserDto>>(users);
		}

		public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				resetToken = resetToken.UrlDecode();
				IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
				if (result.Succeeded)
					await _userManager.UpdateSecurityStampAsync(user);
				else
					throw new Exception();
			}
		}

		public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
				await _userManager.UpdateAsync(user);
			}
			else
				throw new Exception("User not found");
		}

		public async Task<bool> UpdateUserByTitleAsync(UpdateUserTitleDto model)
		{
			var user = await _userManager.FindByIdAsync(model.Id);
			if (user == null) return false;

			user.Title = model.Title;

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<bool> UpdateUserIbanAsync(UpdateUserIbanDto model)
		{
			var user = await _userManager.FindByIdAsync(model.Id);
			if (user == null) return false;

			user.IBAN = model.Iban;

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		private string GenerateValidUsername(string fullName)
		{
			var normalized = fullName
				.ToLower()
				.Replace("ç", "c").Replace("ğ", "g")
				.Replace("ı", "i").Replace("ö", "o")
				.Replace("ş", "s").Replace("ü", "u");

			var username = new string(normalized.Where(char.IsLetterOrDigit).ToArray());

			return string.IsNullOrWhiteSpace(username) ? "user" + Guid.NewGuid().ToString("N").Substring(0, 6) : username;
		}

		private static class RoleConstants
		{
			public const string Admin = "Admin";
			public const string Employee = "Employee";

			public static readonly string[] AllRoles = { Admin, Employee };
		}
	}
}
