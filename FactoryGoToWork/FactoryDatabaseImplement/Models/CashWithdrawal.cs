using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Models
{
    public class CashWithdrawal : ICashWithdrawalModel
    {
		public int Id { get; set; }

		[Required]
		public int AccountId { get; set; }

		//для передачи названия изделия
		public virtual Account Account { get; set; }

		[Required]
		public int Sum { get; set; }

		[Required]
		public DateTime DateOperation { get; set; }

		public static CashWithdrawal Create(BankYouBancruptDatabase context, CashWithdrawalBindingModel model)
		{
			return new CashWithdrawal()
			{
				Id = model.Id,
				AccountId = model.AccountId,
				Account = context.Accounts.First(x => x.Id == model.AccountId),
				Sum = model.Sum,
				DateOperation = model.DateOperation
			};
		}

		public void Update(CashWithdrawalBindingModel model)
		{
			DateOperation = model.DateOperation;
		}

		public CashWithdrawalViewModel GetViewModel => new()
		{
			Id = Id,
			AccountId = AccountId,
			Sum = Sum,
			DateOperation = DateOperation
		};
	}
}
