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
            return context.Reinforceds.Include(x => x.Components).ThenInclude(x => x.Component).ToList()
                    .Select(x => x.GetViewModel).ToList();
        }

        public List<ReinforcedViewModel> GetFilteredList(ReinforcedSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ReinforcedName))
            {
                return new();
            }
            using var context = new FactoryDatabase();
            return context.Reinforceds.Include(x => x.Components).ThenInclude(x => x.Component)
                    .Where(x => x.ReinforcedName.Contains(model.ReinforcedName)).ToList().Select(x => x.GetViewModel).ToList();
        }

        public ReinforcedViewModel? GetElement(ReinforcedSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ReinforcedName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new FactoryDatabase();
            return context.Reinforceds.Include(x => x.Components).ThenInclude(x => x.Component)
                .FirstOrDefault(x => 
                (!string.IsNullOrEmpty(model.ReinforcedName) && x.ReinforcedName == model.ReinforcedName) ||
                (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public ReinforcedViewModel? Insert(ReinforcedBindingModel model)
        {
            using var context = new FactoryDatabase();
            var newReinforced = Reinforced.Create(context, model);
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
                Reinforced.UpdateComponents(context, model);
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
            var element = context.Reinforceds.Include(x => x.Components).FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Reinforceds.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
