using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using FactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Implements
{
	public class MasterStorage : IMasterStorage
	{
		public List<MasterViewModel> GetFullList()
		{
			using var context = new FactoryDatabase();

			return context.Masters
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public List<MasterViewModel> GetFilteredList(MasterSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Email))
			{
				return new();
			}

			using var context = new FactoryDatabase();

			return context.Masters
					.Where(x => x.Email.Contains(model.Email))
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public MasterViewModel? GetElement(MasterSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Email) && !model.Id.HasValue)
			{
				return null;
			}

			using var context = new FactoryDatabase();

			return context.Masters
					.FirstOrDefault(x => ((!string.IsNullOrEmpty(model.Email) && x.Email == model.Email) && (!string.IsNullOrEmpty(model.Password) && x.Password == model.Password)) ||
										(model.Id.HasValue && x.Id == model.Id))
					?.GetViewModel;
		}

		public MasterViewModel? Insert(MasterBindingModel model)
		{
			using var context = new FactoryDatabase();

			var newMaster = Master.Create(context, model);

			if (newMaster == null)
			{
				return null;
			}

			context.Masters.Add(newMaster);
			context.SaveChanges();

			return newMaster.GetViewModel;
		}

		public MasterViewModel? Update(MasterBindingModel model)
		{
			using var context = new FactoryDatabase();
			var master = context.Masters.FirstOrDefault(x => x.Id == model.Id);

			if (master == null)
			{
				return null;
			}

			master.Update(model);
			context.SaveChanges();

			return master.GetViewModel;
		}

		public MasterViewModel? Delete(MasterBindingModel model)
		{
			using var context = new FactoryDatabase();
			var element = context.Masters.FirstOrDefault(rec => rec.Id == model.Id);

			if (element != null)
			{
				context.Masters.Remove(element);
				context.SaveChanges();

				return element.GetViewModel;
			}

			return null;
		}
	}
}
