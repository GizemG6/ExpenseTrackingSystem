using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Services
{
    public interface IMailService
    {
		Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
		Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
		Task SendExpenseStatusUpdateMailAsync(string toEmail, string expenseStatus, string expenseId);
		Task SendExpenseCreatedMailAsync(string[] adminEmails, string userName, string categoryName, decimal amount, DateTime date, string expenseId);
	}
}
