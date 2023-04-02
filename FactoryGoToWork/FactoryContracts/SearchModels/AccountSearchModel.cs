using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.SearchModels
{
	public class AccountSearchModel
	{
		public int? Id { get; set; }

		public string? AccountNumber { get; set; } = string.Empty;

		public int? CashierId { get; set; }

		public int? ClientId { get; set; }

		public string? PasswordAccount { get; set; } = string.Empty;

		public double? Balance { get; set; }

		public DateTime? DateOpen { get; set; }
	}
}
