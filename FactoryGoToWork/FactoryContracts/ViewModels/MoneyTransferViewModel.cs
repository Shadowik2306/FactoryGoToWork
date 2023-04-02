using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
	public class MoneyTransferViewModel : IMoneyTransferModel
	{
		public int Id { get; set; }

		[DisplayName("Сумма перевода")]
		public int Sum { get; set; }

		public int AccountSenderId { get; set; }

		[DisplayName("Номер счёта отп.")]
		public int AccountSenderNumber { get; set; }

		public int AccountPayeeId { get; set; }

		[DisplayName("Номер счёта получ.")]
		public int AccountPayeeNumber { get; set; }

		[DisplayName("Дата операции")]
		public DateTime DateOperation { get; set; } = DateTime.Now;
	}
}
