using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Helpers
{
	public class FileHelper
	{
		public static async Task<string?> SaveReceiptFileAsync(IFormFile? file)
		{
			if (file == null || file.Length == 0)
				return null;

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "receipts");
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fileName = $"{Guid.NewGuid()}_{file.FileName}";
			var filePath = Path.Combine(folderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return Path.Combine("receipts", fileName).Replace("\\", "/");
		}
	}
}
