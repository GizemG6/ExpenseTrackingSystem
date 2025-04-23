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
	}
}
