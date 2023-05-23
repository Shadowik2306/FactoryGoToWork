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
    public class PlanStorage : IPlanStorage
    {
        public List<PlanViewModel> GetFullList()
        {
            using var context = new FactoryDatabase();
			return context.Plans.Include(x => x.Reinforceds).ThenInclude(x => x.Reinforced).ToList()
					.Select(x => x.GetViewModel).ToList();
		}

        public List<PlanViewModel> GetFilteredList(PlanSearchModel model)
        {
            if (string.IsNullOrEmpty(model.PlanName))
            {
                return new();
            }
            using var context = new FactoryDatabase();
			return context.Plans.Include(x => x.Reinforceds).ThenInclude(x => x.Reinforced)
					.Where(x => x.PlanName.Contains(model.PlanName)).ToList().Select(x => x.GetViewModel).ToList();
		}

        public PlanViewModel? GetElement(PlanSearchModel model)
        {
            if (string.IsNullOrEmpty(model.PlanName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new FactoryDatabase();
            return context.Plans.Include(x => x.Reinforceds).ThenInclude(x => x.Reinforced)
                .FirstOrDefault(x => 
                (!string.IsNullOrEmpty(model.PlanName) && x.PlanName == model.PlanName) ||
                (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public PlanViewModel? Insert(PlanBindingModel model)
        {
            using var context = new FactoryDatabase();
            var newPlan = Plan.Create(context, model);
            if (newPlan == null)
            {
                return null;
            }
            context.Plans.Add(newPlan);
            context.SaveChanges();
            return newPlan.GetViewModel;
        }

        public PlanViewModel? Update(PlanBindingModel model)
        {
            using var context = new FactoryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var Plan = context.Plans.FirstOrDefault(rec => rec.Id == model.Id);
                if (Plan == null)
                {
                    return null;
                }
                Plan.Update(model);
                context.SaveChanges();
                Plan.UpdateReinforceds(context, model);
                transaction.Commit();
                return Plan.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public PlanViewModel? Delete(PlanBindingModel model)
        {
            using var context = new FactoryDatabase();
			var element = context.Plans.Include(x => x.Reinforceds).FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.Plans.Remove(element);
				context.SaveChanges();
				return element.GetViewModel;
			}
			return null;
		}
    }
}
