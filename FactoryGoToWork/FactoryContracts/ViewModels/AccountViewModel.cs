using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
	public class AccountViewModel : IAccountModel
	{
		public int Id { get; set; }

		public int CashierId { get; set; }

		public int ClientId { get; set; }

		[DisplayName("Номер счёта")]
		public string AccountNumber { get; set; } = string.Empty;

		[DisplayName("Имя")]
		public string Name { get; set; } = string.Empty;

		[DisplayName("Отчество")]
		public string Patronymic { get; set; } = string.Empty;

		public string PasswordAccount { get; set; } = string.Empty;

		[DisplayName("Баланс")]
		public double Balance { get; set; }

		[DisplayName("Дата открытия")]
		public DateTime DateOpen { get; set; } = DateTime.Now;
	}
}
