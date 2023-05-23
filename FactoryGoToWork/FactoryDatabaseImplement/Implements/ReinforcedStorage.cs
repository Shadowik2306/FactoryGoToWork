using Microsoft.EntityFrameworkCore;
using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using FactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryDatabaseImplement;

namespace PrecastConcretePlantDatabaseImplement.Implements
{
    public class ReinforcedStorage : IReinforcedStorage
    {
        public List<ReinforcedViewModel> GetFullList()
        {
            using var context = new FactoryDatabase();
			return context.Reinforceds.Select(x => x.GetViewModel).ToList();
		}

        public List<ReinforcedViewModel> GetFilteredList(ReinforcedSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ReinforcedName))
            {
                return new();
            }
            using var context = new FactoryDatabase();
			return context.Reinforceds.Select(x => x.GetViewModel).ToList();
		}

        public ReinforcedViewModel? GetElement(ReinforcedSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            using var context = new FactoryDatabase();
			return context.Reinforceds.FirstOrDefault(x => (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
		}

        public ReinforcedViewModel? Insert(ReinforcedBindingModel model)
        {
            using var context = new FactoryDatabase();
            var newReinforced = Reinforced.Create(model);
            if (newReinforced == null)
            {
                return null;
            }
            context.Reinforceds.Add(newReinforced);
            context.SaveChanges();
            return newReinforced.GetViewModel;
        }

        public ReinforcedViewModel? Update(ReinforcedBindingModel model)
        {
            using var context = new FactoryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var Reinforced = context.Reinforceds.FirstOrDefault(rec => rec.Id == model.Id);
                if (Reinforced == null)
                {
                    return null;
                }
                Reinforced.Update(model);
                context.SaveChanges();
                transaction.Commit();
                return Reinforced.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public ReinforcedViewModel? Delete(ReinforcedBindingModel model)
        {
            using var context = new FactoryDatabase();
			var reinforced = context.Reinforceds.FirstOrDefault(x => x.Id == model.Id);
			if (reinforced != null)
			{
				context.Reinforceds.Remove(reinforced);
				context.SaveChanges();
				return reinforced.GetViewModel;
			}
			return null;
		}
    }
}
