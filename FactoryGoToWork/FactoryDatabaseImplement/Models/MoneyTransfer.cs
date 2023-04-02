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
    public class MoneyTransfer : IMoneyTransferModel
    {
		public int Id { get; set; }

        [Required]
		public int Sum { get; set; }

		[Required]
		public int AccountSenderId { get; set; }

		[Required]
		public int AccountPayeeId { get; set; }

		[Required]
		public DateTime DateOperation { get; set; }

		public static MoneyTransfer Create(BankYouBancruptDatabase context, MoneyTransferBindingModel model)
		{
			return new MoneyTransfer()
			{
				Id = model.Id,
				Sum = model.Sum,
				AccountSenderId = model.AccountSenderId,
				AccountPayeeId = model.AccountPayeeId,
				DateOperation = model.DateOperation
			};
		}

		public void Update(MoneyTransferBindingModel model)
		{
			Id = model.Id;
			DateOperation = model.DateOperation;
		}

		public MoneyTransferViewModel GetViewModel => new()
		{
			Id = Id,
			AccountPayeeId = AccountPayeeId,
			AccountSenderId = AccountSenderId,
			DateOperation = DateOperation,
			Sum = Sum
		};
	}
}
