using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Helpers
{
	public static class IbanGenerator
	{
		public static string GenerateFakeIban()
		{
			var random = new Random();
			var ibanNumbers = string.Concat(Enumerable.Range(0, 24)
				.Select(_ => random.Next(0, 10).ToString())
				.ToArray());

			return $"TR{ibanNumbers}";
		}
	}
}
