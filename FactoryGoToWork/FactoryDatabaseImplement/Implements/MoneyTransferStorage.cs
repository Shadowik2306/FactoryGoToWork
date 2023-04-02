using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using FactoryImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Implements
{
	public class MoneyTransferStorage : IMoneyTransferStorage
	{
		public List<MoneyTransferViewModel> GetFullList()
		{
			using var context = new BankYouBancruptDatabase();

			return context.MoneyTransfers
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public List<MoneyTransferViewModel> GetFilteredList(MoneyTransferSearchModel model)
		{
			if (model.AccountSenderId < 0)
			{
				return new();
			}

			using var context = new BankYouBancruptDatabase();

			return context.MoneyTransfers
					.Where(x => x.AccountSenderId == model.AccountSenderId)
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public MoneyTransferViewModel? GetElement(MoneyTransferSearchModel model)
		{
			if (model.AccountSenderId < 0 && !model.Id.HasValue)
			{
				return null;
			}

			using var context = new BankYouBancruptDatabase();

			return context.MoneyTransfers
					.FirstOrDefault(x => (!(model.AccountSenderId < 0) && x.AccountSenderId == model.AccountSenderId
					&& x.AccountPayeeId == model.AccountPayeeId && x.DateOperation == x.DateOperation
					&& x.Sum == model.Sum) || (model.Id.HasValue && x.Id == model.Id))
					?.GetViewModel;
		}

		public MoneyTransferViewModel? Insert(MoneyTransferBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();

			var newMoneyTransfer = MoneyTransfer.Create(context, model);

			if (newMoneyTransfer == null)
			{
				return null;
			}

			context.MoneyTransfers.Add(newMoneyTransfer);
			context.SaveChanges();

			return newMoneyTransfer.GetViewModel;
		}

		public MoneyTransferViewModel? Update(MoneyTransferBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			using var transaction = context.Database.BeginTransaction();

			try
			{
				var moneyTransfer = context.MoneyTransfers.FirstOrDefault(rec => rec.Id == model.Id);

				if (moneyTransfer == null)
				{
					return null;
				}

				moneyTransfer.Update(model);
				context.SaveChanges();
				transaction.Commit();

				return moneyTransfer.GetViewModel;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public MoneyTransferViewModel? Delete(MoneyTransferBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			var element = context.MoneyTransfers
				.Include(x => x.AccountPayeeId)
				.Include(x => x.AccountSenderId)
				.Include(x => x.Sum)
				.Include(x => x.DateOperation)
				.FirstOrDefault(rec => rec.Id == model.Id);

			if (element != null)
			{
				context.MoneyTransfers.Remove(element);
				context.SaveChanges();

				return element.GetViewModel;
			}

			return null;
		}
	}
}
