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
	public class StageStorage : IStageStorage
    {
		public List<StageViewModel> GetFullList()
		{
			using var context = new FactoryDatabase();

			return context.Stages
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public List<StageViewModel> GetFilteredList(StageSearchModel model)
		{
			if (model.Id is null)
			{
				return new();
			}

			using var context = new FactoryDatabase();

			return context.Stages
					.Where(x => x.Id.Equals(model.Id))
					.Select(x => x.GetViewModel)
					.ToList();
		}

		public StageViewModel? GetElement(StageSearchModel model)
		{
            if (model.Id is null)
            {
                return new();
            }

            using var context = new FactoryDatabase();

			return context.Stages
					.FirstOrDefault(x => x.Id.Equals(model.Id))
					?.GetViewModel;
		}

		public StageViewModel? Insert(StageBindingModel model)
		{
			using var context = new FactoryDatabase();

			var newStage = Stage.Create(context, model);

			if (newStage == null)
			{
				return null;
			}

			context.Stages.Add(newStage);
			context.SaveChanges();

			return newStage.GetViewModel;
		}

		public StageViewModel? Update(StageBindingModel model)
		{
			using var context = new FactoryDatabase();
			var Stage = context.Stages.FirstOrDefault(x => x.Id == model.Id);

			if (Stage == null)
			{
				return null;
			}

			Stage.Update(model);
			context.SaveChanges();

			return Stage.GetViewModel;
		}

		public StageViewModel? Delete(StageBindingModel model)
		{
			using var context = new FactoryDatabase();
			var element = context.Stages.FirstOrDefault(rec => rec.Id == model.Id);

			if (element != null)
			{
				context.Stages.Remove(element);
				context.SaveChanges();

				return element.GetViewModel;
			}

			return null;
		}
	}
}
