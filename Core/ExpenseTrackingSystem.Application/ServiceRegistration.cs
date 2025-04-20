using AutoMapper;
using ExpenseTrackingSystem.Application.Mapper;
using ExpenseTrackingSystem.Application.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application
{
	public static class ServiceRegistration
	{
		public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers().AddFluentValidation(x =>
			{
				x.RegisterValidatorsFromAssemblyContaining<AuditLogValidator>();
				x.RegisterValidatorsFromAssemblyContaining<ExpenseCategoryValidator>();
				x.RegisterValidatorsFromAssemblyContaining<ExpenseValidator>();
				x.RegisterValidatorsFromAssemblyContaining<PaymentSimulationValidator>();
				x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
			});

			services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MapperConfig())).CreateMapper());
		}
	}
}
