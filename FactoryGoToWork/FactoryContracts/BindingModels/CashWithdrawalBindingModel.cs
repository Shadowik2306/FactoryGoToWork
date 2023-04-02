using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
	public class CashWithdrawalBindingModel : ICashWithdrawalModel
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int Sum { get; set; }

		public DateTime DateOperation { get; set; } = DateTime.Now;
	}
}
