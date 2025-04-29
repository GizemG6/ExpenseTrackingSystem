using ExpenseTrackingSystem.Infrastructure.Services;
using ExpenseTrackingSystem.Persistence.Services;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Tests
{
	public class MailServiceTests
	{
		private readonly MailService _mailService;

		public MailServiceTests()
		{
			IConfiguration configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

			var rabbitMqService = new RabbitMqService(configuration);
			_mailService = new MailService(configuration, rabbitMqService);
		}

		[Fact]
		public async Task SendExpenseCreatedMailAsync_ShouldPublishMessageToRabbitMq()
		{
			var adminEmails = new[] { "gizem@example.com" };
			var userName = "Gizem Güneş";
			var categoryName = "Office Supplies";
			var amount = 100.0m;
			var date = DateTime.Now;
			var expenseId = "exp-001";

			await _mailService.SendExpenseCreatedMailAsync(adminEmails, userName, categoryName, amount, date, expenseId);

		}
	}
}
