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
	public class AccountStorage : IAccountStorage
	{
		public List<AccountViewModel> GetFullList()
		{
			using var context = new BankYouBancruptDatabase();

			return context.Accounts
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public List<AccountViewModel> GetFilteredList(AccountSearchModel model)
		{
			if (string.IsNullOrEmpty(model.AccountNumber))
			{
				return new();
			}

			using var context = new BankYouBancruptDatabase();

			return context.Accounts
					.Where(x => x.AccountNumber.Contains(model.AccountNumber))
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public AccountViewModel? GetElement(AccountSearchModel model)
		{
			if (string.IsNullOrEmpty(model.AccountNumber) && !model.Id.HasValue)
			{
				return null;
			}

			using var context = new BankYouBancruptDatabase();

			return context.Accounts
					.FirstOrDefault(x => (!string.IsNullOrEmpty(model.AccountNumber) && x.AccountNumber == model.AccountNumber) ||
										(model.Id.HasValue && x.Id == model.Id))
					?.GetViewModel;
		}

		public AccountViewModel? Insert(AccountBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();

			var newAccount = Account.Create(context, model);

			if (newAccount == null)
			{
				return null;
			}

			context.Accounts.Add(newAccount);
			context.SaveChanges();

			return newAccount.GetViewModel;
		}

		public AccountViewModel? Update(AccountBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			using var transaction = context.Database.BeginTransaction();

			try
			{
				var account = context.Accounts.FirstOrDefault(rec => rec.Id == model.Id);

				if (account == null)
				{
					return null;
				}

				account.Update(model);
				context.SaveChanges();
				transaction.Commit();

				return account.GetViewModel;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public AccountViewModel? Delete(AccountBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			var element = context.Accounts
				.Include(x => x.AccountNumber)
				.FirstOrDefault(rec => rec.Id == model.Id);

			if (element != null)
			{
				context.Accounts.Remove(element);
				context.SaveChanges();

				return element.GetViewModel;
			}

			return null;
		}
	}
}
