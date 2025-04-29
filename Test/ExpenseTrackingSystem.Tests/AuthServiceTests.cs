using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Abstractions.Token;
using ExpenseTrackingSystem.Application.Dtos.Token;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using ExpenseTrackingSystem.Persistence.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ExpenseTrackingSystem.Tests
{
	public class AuthServiceTests
	{
		private readonly Mock<UserManager<AppUser>> _userManagerMock;
		private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
		private readonly Mock<ITokenService> _tokenServiceMock;
		private readonly Mock<IUserService> _userServiceMock;
		private readonly Mock<IMailService> _mailServiceMock;
		private readonly Mock<IAuditLogService> _auditLogServiceMock;
		private readonly Mock<IConfiguration> _configurationMock;

		private readonly AuthService _authService;

		public AuthServiceTests()
		{
			var userStoreMock = new Mock<IUserStore<AppUser>>();
			_userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

			var contextAccessorMock = new Mock<IHttpContextAccessor>();
			var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
			_signInManagerMock = new Mock<SignInManager<AppUser>>(
				_userManagerMock.Object,
				contextAccessorMock.Object,
				userPrincipalFactoryMock.Object,
				null,
				null,
				null,
				null
			);

			_tokenServiceMock = new Mock<ITokenService>();
			_userServiceMock = new Mock<IUserService>();
			_mailServiceMock = new Mock<IMailService>();
			_auditLogServiceMock = new Mock<IAuditLogService>();
			_configurationMock = new Mock<IConfiguration>();

			_authService = new AuthService(
				_configurationMock.Object,
				_userManagerMock.Object,
				_mailServiceMock.Object,
				_tokenServiceMock.Object,
				_signInManagerMock.Object,
				_userServiceMock.Object,
				_auditLogServiceMock.Object
			);
		}

		[Fact]
		public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
		{
			var email = "test@example.com";
			var password = "Password123!";
			var accessTokenLifetime = 15;

			var user = new AppUser
			{
				Id = "user-id-123",
				Email = email,
				UserName = "testuser"
			};

			var expectedToken = new TokenDto
			{
				AccessToken = "access-token",
				RefreshToken = "refresh-token",
				Expiration = DateTime.UtcNow.AddMinutes(15)
			};

			_userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
			_signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, password, false)).ReturnsAsync(SignInResult.Success);
			_tokenServiceMock.Setup(x => x.CreateAccessToken(accessTokenLifetime, user)).Returns(expectedToken);
			_userServiceMock.Setup(x => x.UpdateRefreshTokenAsync(expectedToken.RefreshToken, user, expectedToken.Expiration, 15)).Returns(Task.CompletedTask);
			_auditLogServiceMock.Setup(x => x.LogActionAsync(user.Id, "Login", "User", user.Id)).Returns(Task.CompletedTask);

			var result = await _authService.LoginAsync(email, password, accessTokenLifetime);

			result.Should().NotBeNull();
			result.AccessToken.Should().Be(expectedToken.AccessToken);
			result.RefreshToken.Should().Be(expectedToken.RefreshToken);

			_userManagerMock.Verify(x => x.FindByEmailAsync(email), Times.Once);
			_signInManagerMock.Verify(x => x.CheckPasswordSignInAsync(user, password, false), Times.Once);
			_tokenServiceMock.Verify(x => x.CreateAccessToken(accessTokenLifetime, user), Times.Once);
			_userServiceMock.Verify(x => x.UpdateRefreshTokenAsync(expectedToken.RefreshToken, user, expectedToken.Expiration, 15), Times.Once);
			_auditLogServiceMock.Verify(x => x.LogActionAsync(user.Id, "Login", "User", user.Id), Times.Once);
		}
	}
}
