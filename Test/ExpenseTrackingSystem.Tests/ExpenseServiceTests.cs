using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Expense;
using ExpenseTrackingSystem.Application.Helpers;
using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Application.Repositories.Payment;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using ExpenseTrackingSystem.Persistence.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FileHelper = ExpenseTrackingSystem.Application.Helpers.FileHelper;

namespace ExpenseTrackingSystem.Tests
{
	public class ExpenseServiceTests
	{
		private readonly Mock<IExpenseReadRepository> _readRepo = new();
		private readonly Mock<IExpenseWriteRepository> _writeRepo = new();
		private readonly Mock<UserManager<AppUser>> _userManager;
		private readonly Mock<IExpenseCategoryReadRepository> _categoryRepo = new();
		private readonly Mock<IPaymentReadRepository> _paymentReadRepo = new();
		private readonly Mock<IPaymentWriteRepository> _paymentWriteRepo = new();
		private readonly Mock<IHttpContextAccessor> _httpContext = new();
		private readonly Mock<IAuditLogService> _auditLogService = new();
		private readonly Mock<IMailService> _mailService = new();
		private readonly Mock<RoleManager<AppRole>> _roleManager;
		private readonly ExpenseService _service;

		public ExpenseServiceTests()
		{
			var store = new Mock<IUserStore<AppUser>>();
			_userManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);

			var roleStore = new Mock<IRoleStore<AppRole>>();
			_roleManager = new Mock<RoleManager<AppRole>>(roleStore.Object, null, null, null, null);

			_service = new ExpenseService(
				_readRepo.Object,
				_writeRepo.Object,
				_userManager.Object,
				_categoryRepo.Object,
				_paymentReadRepo.Object,
				_paymentWriteRepo.Object,
				_httpContext.Object,
				_auditLogService.Object,
				_mailService.Object,
				_roleManager.Object
			);
		}

		[Fact]
		public async Task CreateAsync_ShouldCreateExpense_WhenDataIsValid()
		{
			var userId = "test-user-id";
			var expenseDto = new ExpenseCreateDto { Amount = 500, CategoryId = 1, UserId = userId };

			var httpContext = new DefaultHttpContext();
			httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId)
			}));

			_httpContext.Setup(x => x.HttpContext).Returns(httpContext);

			var appUser = new AppUser { Id = userId, FullName = "Test" };
			_userManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(appUser);

			var category = new ExpenseCategory { Id = 1, Name = "Test" };
			_categoryRepo.Setup(x => x.GetByIdAsync(1, true)).ReturnsAsync(category);

			_writeRepo.Setup(x => x.AddAsync(It.IsAny<Expense>())).ReturnsAsync(true);

			var result = await _service.CreateAsync(expenseDto);

			result.Should().NotBeNull();
			result.Amount.Should().Be(expenseDto.Amount);
			_writeRepo.Verify(x => x.AddAsync(It.IsAny<Expense>()), Times.Once);
		}

	}
}
