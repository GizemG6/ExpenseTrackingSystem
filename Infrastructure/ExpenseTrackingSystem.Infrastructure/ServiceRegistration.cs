using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Infrastructure.Services;
using ExpenseTrackingSystem.Persistence.Services;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTrackingSystem.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IMailService, MailService>();
			services.AddSingleton<RabbitMqService>();
			services.AddHostedService<RabbitMqBackgroundService>();

			services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

			var redisConnection = configuration.GetSection("Redis")["ConnectionString"];

			services.AddHangfire(config =>
			{
				config.UseRedisStorage(redisConnection);
			});
			services.AddHangfireServer();
		}
	}
}
