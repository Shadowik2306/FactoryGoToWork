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
	public class CashierStorage : ICashierStorage
	{
		public List<CashierViewModel> GetFullList()
		{
			using var context = new BankYouBancruptDatabase();

			return context.Cashiers
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public List<CashierViewModel> GetFilteredList(CashierSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Email))
			{
				return new();
			}

			using var context = new BankYouBancruptDatabase();

			return context.Cashiers
					.Where(x => x.Email.Contains(model.Email))
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public CashierViewModel? GetElement(CashierSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Email) && !model.Id.HasValue)
			{
				return null;
			}

			using var context = new BankYouBancruptDatabase();

			return context.Cashiers
					.FirstOrDefault(x => (!string.IsNullOrEmpty(model.Email) && x.Email == model.Email) ||
										(model.Id.HasValue && x.Id == model.Id))
					?.GetViewModel;
		}

		public CashierViewModel? Insert(CashierBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();

			var newCashier = Cashier.Create(context, model);

			if (newCashier == null)
			{
				return null;
			}

			context.Cashiers.Add(newCashier);
			context.SaveChanges();

			return newCashier.GetViewModel;
		}

		public CashierViewModel? Update(CashierBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			var cashier = context.Cashiers.FirstOrDefault(x => x.Id == model.Id);

			if (cashier == null)
			{
				return null;
			}

			cashier.Update(model);
			context.SaveChanges();

			return cashier.GetViewModel;
		}

		public CashierViewModel? Delete(CashierBindingModel model)
		{
			using var context = new BankYouBancruptDatabase();
			var element = context.Cashiers.FirstOrDefault(rec => rec.Id == model.Id);

			if (element != null)
			{
				context.Cashiers.Remove(element);
				context.SaveChanges();

				return element.GetViewModel;
			}

			return null;
		}
	}
}
