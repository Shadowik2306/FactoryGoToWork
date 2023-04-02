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
	public class CashWithdrawalStorage : ICashWithdrawalStorage
	{
		public List<CashWithdrawalViewModel> GetFullList()
		{
			using var context = new BankYouBancruptDatabase();

			return context.CashWithdrawals
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public List<CashWithdrawalViewModel> GetFilteredList(CashWithdrawalSearchModel model)
		{
			if (model.AccountId < 0)
			{
				return new();
			}

			using var context = new BankYouBancruptDatabase();

			return context.CashWithdrawals
					.Where(x => x.AccountId == model.AccountId)
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public CashWithdrawalViewModel? GetElement(CashWithdrawalSearchModel model)
		{
			if (model.AccountId < 0 && !model.Id.HasValue)
			{
				return null;
			}

			using var context = new BankYouBancruptDatabase();

			return context.CashWithdrawals
					.FirstOrDefault(x => (!(model.AccountId < 0) && x.AccountId == model.AccountId) ||
										(model.Id.HasValue && x.Id == model.Id))
					?.GetViewModel;
		}

		public CashWithdrawalViewModel? Insert(CashWithdrawalBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();

			var newCashWithdrawal = CashWithdrawal.Create(context, model);

			if (newCashWithdrawal == null)
			{
				return null;
			}

			context.CashWithdrawals.Add(newCashWithdrawal);
			context.SaveChanges();

			return newCashWithdrawal.GetViewModel;
		}

		public CashWithdrawalViewModel? Update(CashWithdrawalBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			using var transaction = context.Database.BeginTransaction();

			try
			{
				var cashWithdrawal = context.CashWithdrawals.FirstOrDefault(rec => rec.Id == model.Id);

				if (cashWithdrawal == null)
				{
					return null;
				}

				cashWithdrawal.Update(model);
				context.SaveChanges();
				transaction.Commit();

				return cashWithdrawal.GetViewModel;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public CashWithdrawalViewModel? Delete(CashWithdrawalBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			var element = context.CashWithdrawals
				.Include(x => x.AccountId)
				.FirstOrDefault(rec => rec.Id == model.Id);

			if (element != null)
			{
				context.CashWithdrawals.Remove(element);
				context.SaveChanges();

				return element.GetViewModel;
			}

			return null;
		}
	}
}
