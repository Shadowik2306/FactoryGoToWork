using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
	public class MoneyTransferBindingModel : IMoneyTransferModel
	{
		public int Id { get; set; }

		public int Sum { get; set; }

		public int AccountSenderId { get; set; }

		public int AccountPayeeId { get; set; }

		public DateTime DateOperation { get; set; } = DateTime.Now;
	}
}
