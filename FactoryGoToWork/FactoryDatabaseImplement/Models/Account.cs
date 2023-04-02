using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Models
{
    public class Account : IAccountModel
    {
		public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; } = string.Empty;

		[Required]
		public int CashierId { get; set; }

		[Required]
		public int ClientId { get; set; }

		//для передачи ФИО клиента
		public virtual Client Client { get; set; }

		[Required]
		public string PasswordAccount { get; set; } = string.Empty;

		[Required]
		public double Balance { get; set; }

		[Required]
		public DateTime DateOpen { get; set; } = DateTime.Now;

		//для реализации связи один ко многим со Снятием наличных
		[ForeignKey("AccountId")]
		public virtual List<CashWithdrawal> CashWithdrawals { get; set; } = new();

		//для реализации связи один ко многим с Переводом денег
		[ForeignKey("AccountSenderId")]
		public virtual List<MoneyTransfer> MoneyTransferSenders { get; set; } = new();

		[ForeignKey("AccountPayeeId")]
		public virtual List<MoneyTransfer> MoneyTransferPayees { get; set; } = new();

		//для реализации связи один ко многим с Картами
		[ForeignKey("AccountId")]
		public virtual List<Card> Cards { get; set; } = new();

		public static Account Create(BankYouBancruptDatabase context, AccountBindingModel model)
		{
			return new Account()
			{
				Id = model.Id,
				ClientId = model.ClientId,
				Client = context.Clients.First(x => x.Id == model.ClientId),
				PasswordAccount = model.PasswordAccount,
				Balance = model.Balance,
				DateOpen = model.DateOpen,
				CashierId = model.CashierId,
				AccountNumber = model.AccountNumber
			};
		}

		public void Update(AccountBindingModel model)
		{
			Balance = model.Balance;
			PasswordAccount = model.PasswordAccount;
		}

		public AccountViewModel GetViewModel => new()
		{
			Id = Id,
			CashierId = CashierId,
			ClientId = ClientId,
			AccountNumber = AccountNumber,
			Balance = Balance,
			PasswordAccount = PasswordAccount,
			DateOpen = DateOpen
		};
	}
}
