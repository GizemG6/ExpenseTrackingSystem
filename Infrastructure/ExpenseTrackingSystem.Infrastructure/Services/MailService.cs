using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ExpenseTrackingSystem.Infrastructure.Services;
using ExpenseTrackingSystem.Application.Dtos.Mail;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class MailService : IMailService
	{
		private readonly IConfiguration _configuration;
		private readonly RabbitMqService _rabbitMqService;

		public MailService(IConfiguration configuration, RabbitMqService rabbitMqService)
		{
			_configuration = configuration;
			_rabbitMqService = rabbitMqService;
		}

		public async Task SendExpenseCreatedMailAsync(string[] adminEmails, string userName, string categoryName, decimal amount, DateTime date, string expenseId)
		{
			StringBuilder mailBody = new();
			mailBody.AppendLine("Hello,<br><br>");
			mailBody.AppendLine($"A new expense has been created. Details:<br><br>");
			mailBody.AppendLine($"<strong>Expense ID:</strong> {expenseId}<br>");
			mailBody.AppendLine($"<strong>UserName:</strong> {userName}<br>");
			mailBody.AppendLine($"<strong>Category:</strong> {categoryName}<br>");
			mailBody.AppendLine($"<strong>Amount:</strong> {amount:C}<br>");
			mailBody.AppendLine($"<strong>Date:</strong> {date:dd/MM/yyyy}<br><br>");
			mailBody.AppendLine("You can track your expenses in the system.<br>");

			var mailMessage = new
			{
				ToEmails = adminEmails,
				Subject = "New Expense Created",
				Body = mailBody.ToString(),
				IsBodyHtml = true
			};

			_rabbitMqService.PublishMessage("emailQueue", JsonConvert.SerializeObject(mailMessage));
		}

		public async Task SendExpenseStatusUpdateMailAsync(string toEmail, string expenseStatus, string expenseId)
		{
			StringBuilder mailBody = new();
			mailBody.AppendLine("Hello,<br><br>");
			mailBody.AppendLine($"The status of your expense has been updated as follows:<br><br>");
			mailBody.AppendLine($"<strong>Expense ID:</strong> {expenseId}<br>");
			mailBody.AppendLine($"<strong>Status:</strong> {expenseStatus}<br><br>");
			mailBody.AppendLine("You can contact the system to get more information about the expense status..<br>");

			var mailMessage = new
			{
				ToEmails = new[] { toEmail },
				Subject = "Expense Status Updated",
				Body = mailBody.ToString(),
				IsBodyHtml = true
			};

			_rabbitMqService.PublishMessage("emailQueue", JsonConvert.SerializeObject(mailMessage));
		}

		public async Task SendMailAsync(MailRequest mailRequest)
		{
			var smtpSettings = _configuration.GetSection("MailSettings");
			string smtpServer = smtpSettings["SmtpServer"];
			int smtpPort = int.Parse(smtpSettings["SmtpPort"]);
			string senderEmail = smtpSettings["SenderEmail"];
			string senderPassword = smtpSettings["SenderPassword"];
			bool enableSSL = bool.Parse(smtpSettings["EnableSSL"]);

			using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
			{
				smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
				smtpClient.EnableSsl = enableSSL;

				using (var message = new MailMessage())
				{
					message.From = new MailAddress(senderEmail);
					message.Subject = mailRequest.Subject;
					message.Body = mailRequest.Body;
					message.IsBodyHtml = mailRequest.IsBodyHtml;

					foreach (var to in mailRequest.ToEmails)
					{
						message.To.Add(to);
					}
					try
					{
						await smtpClient.SendMailAsync(message);
					}
					catch (Exception ex)
					{
						throw new Exception("An error occurred while sending the email: " + ex.Message);
					}
				}
			}
		}

		public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
		{
			StringBuilder mailBody = new();
			mailBody.AppendLine("Hello,<br><br>");
			mailBody.AppendLine("If you have requested a password reset, you can reset your password via the API using the following information:<br><br>");
			mailBody.AppendLine($"<strong>User ID:</strong> {userId}<br>");
			mailBody.AppendLine($"<strong>Reset Token:</strong> {resetToken}<br><br>");
			mailBody.AppendLine("Using this information, you can send a request to the `api/Users/update-password` endpoint.<br>");

			var mailMessage = new
			{
				ToEmails = new[] { to },
				Subject = "Password Renewal Request",
				Body = mailBody.ToString(),
				IsBodyHtml = true
			};
			
			_rabbitMqService.PublishMessage("emailQueue", JsonConvert.SerializeObject(mailMessage));
		}
	}
}
