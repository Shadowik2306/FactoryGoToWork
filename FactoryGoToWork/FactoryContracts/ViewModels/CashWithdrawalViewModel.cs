using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
	public class CashWithdrawalViewModel : ICashWithdrawalModel
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		[DisplayName("Номер счёта")]
		public int AccountNumber { get; set; }

		[DisplayName("Сумма выданных наличных")]
		public int Sum { get; set; }

		[DisplayName("Дата операции")]
		public DateTime DateOperation { get; set; } = DateTime.Now;
	}
}
