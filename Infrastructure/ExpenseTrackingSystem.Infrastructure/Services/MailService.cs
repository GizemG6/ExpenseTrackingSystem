using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class MailService : IMailService
	{
		private readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendExpenseCreatedMailAsync(string[] adminEmails, string userName, string categoryName, decimal amount, DateTime date, string expenseId)
		{
			StringBuilder mailBody = new();
			mailBody.AppendLine("Merhaba,<br><br>");
			mailBody.AppendLine($"Yeni bir masrafınız oluşturulmuştur. İşte detaylar:<br><br>");
			mailBody.AppendLine($"<strong>Masraf ID:</strong> {expenseId}<br>");
			mailBody.AppendLine($"<strong>Kullanıcı Adı:</strong> {userName}<br>");
			mailBody.AppendLine($"<strong>Kategori:</strong> {categoryName}<br>");
			mailBody.AppendLine($"<strong>Tutar:</strong> {amount:C}<br>");
			mailBody.AppendLine($"<strong>Tarih:</strong> {date:dd/MM/yyyy}<br><br>");
			mailBody.AppendLine("Masrafınızı sistemde takip edebilirsiniz.<br>");

			await SendMailAsync(adminEmails, "Yeni Masraf Oluşturuldu", mailBody.ToString());
		}

		public async Task SendExpenseStatusUpdateMailAsync(string toEmail, string expenseStatus, string expenseId)
		{
			StringBuilder mailBody = new();
			mailBody.AppendLine("Merhaba,<br><br>");
			mailBody.AppendLine($"Masrafınızın durumu şu şekilde güncellenmiştir:<br><br>");
			mailBody.AppendLine($"<strong>Masraf ID:</strong> {expenseId}<br>");
			mailBody.AppendLine($"<strong>Durum:</strong> {expenseStatus}<br><br>");
			mailBody.AppendLine("Masraf durumu hakkında daha fazla bilgi almak için sistemle iletişime geçebilirsiniz.<br>");

			await SendMailAsync(new[] { toEmail }, "Masraf Durumu Güncellenmiştir", mailBody.ToString());
		}

		public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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
					message.Subject = subject;
					message.Body = body;
					message.IsBodyHtml = isBodyHtml;

					foreach (var to in tos)
					{
						message.To.Add(to);
					}

					await smtpClient.SendMailAsync(message);
				}
			}
		}

		public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
		{
			StringBuilder mailBody = new();
			mailBody.AppendLine("Merhaba,<br><br>");
			mailBody.AppendLine("Eğer şifre sıfırlama talebinde bulunduysanız, aşağıdaki bilgileri kullanarak API üzerinden şifrenizi yenileyebilirsiniz:<br><br>");
			mailBody.AppendLine($"<strong>Kullanıcı ID:</strong> {userId}<br>");
			mailBody.AppendLine($"<strong>Reset Token:</strong> {resetToken}<br><br>");
			mailBody.AppendLine("Bu bilgileri kullanarak `api/auth/reset-password` endpoint'ine bir istek gönderebilirsiniz.<br>");

			await SendMailAsync(new[] { to }, "Şifre Yenileme Talebi", mailBody.ToString());
		}
	}
}
