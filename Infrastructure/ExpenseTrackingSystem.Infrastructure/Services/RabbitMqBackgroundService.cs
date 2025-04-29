using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Mail;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Infrastructure.Services
{
	public class RabbitMqBackgroundService: BackgroundService
	{
		private readonly RabbitMqService _rabbitMqService;
		private readonly IMailService _mailService;

		public RabbitMqBackgroundService(RabbitMqService rabbitMqService, IMailService mailService)
		{
			_rabbitMqService = rabbitMqService;
			_mailService = mailService;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_rabbitMqService.ListenToQueue("emailQueue", async (message) =>
			{

				var mailRequest = JsonSerializer.Deserialize<MailRequest>(message);

				if (mailRequest != null)
				{
					BackgroundJob.Enqueue<IMailService>(mailService =>
						mailService.SendMailAsync(mailRequest));
				}
			});

			return Task.CompletedTask;
		}
	}
}
