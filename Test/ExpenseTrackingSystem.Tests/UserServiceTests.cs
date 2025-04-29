using AutoMapper;
using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using ExpenseTrackingSystem.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Tests
{
	public class UserServiceTests
	{
		private readonly Mock<UserManager<AppUser>> _userManagerMock;
		private readonly Mock<RoleManager<AppRole>> _roleManagerMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly UserService _userService;

		public UserServiceTests()
		{
			var userStoreMock = new Mock<IUserStore<AppUser>>();
			_userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

			var roleStoreMock = new Mock<IRoleStore<AppRole>>();
			_roleManagerMock = new Mock<RoleManager<AppRole>>(roleStoreMock.Object, null, null, null, null);

			_mapperMock = new Mock<IMapper>();

			_userService = new UserService(_userManagerMock.Object, _roleManagerMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task CreateAsync_ShouldReturnSuccess_WhenUserIsCreated()
		{
			var createDto = new UserCreateDto
			{
				FullName = "Mustafa Kemal Atatürk",
				Password = "sonsuzadek1938+"
			};

			var appUser = new AppUser
			{
				FullName = createDto.FullName,
				Email = "ataturk@example.com"
			};

			_mapperMock.Setup(x => x.Map<AppUser>(createDto)).Returns(appUser);
			_userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), createDto.Password))
							.ReturnsAsync(IdentityResult.Success);

			var result = await _userService.CreateAsync(createDto);

			Assert.True(result.Succeeded);
			Assert.Equal("User created successfully.", result.Message);
			_userManagerMock.Verify(x => x.CreateAsync(It.IsAny<AppUser>(), createDto.Password), Times.Once);
		}

		[Fact]
		public async Task CreateAsync_ShouldThrowException_WhenUserCreationFails()
		{
			var createDto = new UserCreateDto
			{
				FullName = "Mustafa Kemal Atatürk",
				Password = "sonsuzadek1938+"
			};

			var appUser = new AppUser
			{
				FullName = createDto.FullName
			};

			var identityErrors = new List<IdentityError>
			{
				new IdentityError { Code = "PasswordTooWeak", Description = "Password is too weak." }
			};

			var failedResult = IdentityResult.Failed(identityErrors.ToArray());

			_mapperMock.Setup(x => x.Map<AppUser>(createDto)).Returns(appUser);
			_userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), createDto.Password))
							.ReturnsAsync(failedResult);

			var ex = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(createDto));
			Assert.Contains("PasswordTooWeak", ex.Message);
			_userManagerMock.Verify(x => x.CreateAsync(It.IsAny<AppUser>(), createDto.Password), Times.Once);
		}
	}
}
