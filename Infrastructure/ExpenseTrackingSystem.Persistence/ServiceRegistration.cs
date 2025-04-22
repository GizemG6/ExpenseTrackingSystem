using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Abstractions.Token;
using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Application.Repositories.Payment;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using ExpenseTrackingSystem.Persistence.Context;
using ExpenseTrackingSystem.Persistence.Repositories;
using ExpenseTrackingSystem.Persistence.Repositories.Payment;
using ExpenseTrackingSystem.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence
{
    public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<AppUser, AppRole>(options =>
			{
				options.Password.RequiredLength = 4;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IExpenseReadRepository, ExpenseReadRepository>();
			services.AddScoped<IExpenseWriteRepository, ExpenseWriteRepository>();
			services.AddScoped<IExpenseCategoryReadRepository, ExpenseCategoryReadRepository>();
			services.AddScoped<IExpenseCategoryWriteRepository, ExpenseCategoryWriteRepository>();
			services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
			services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();
		}
	}
}
