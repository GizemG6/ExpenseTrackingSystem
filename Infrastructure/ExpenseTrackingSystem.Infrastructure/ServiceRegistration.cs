using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Persistence.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTrackingSystem.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IMailService, MailService>();
		}
	}
}
