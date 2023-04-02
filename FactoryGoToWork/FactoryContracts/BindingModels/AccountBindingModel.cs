using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
	public class AccountBindingModel : IAccountModel
	{
		public int Id { get; set; }

		public int CashierId { get; set; }

		public int ClientId { get; set; }

		public string AccountNumber { get; set; } = string.Empty;

		public string PasswordAccount { get; set; } = string.Empty;

		public double Balance { get; set; }

		public DateTime DateOpen { get; set; } = DateTime.Now;
	}
}
