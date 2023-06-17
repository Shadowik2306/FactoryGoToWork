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
    public class ComponentStorage : IComponentStorage
    {
        public List<ComponentViewModel> GetFullList()
        {
            using var context = new FactoryDatabase();
            return context.Components.Include(x => x.Plans).ThenInclude(x => x.Plan)
                .Select(x => x.GetViewModel).ToList();
        }

        public List<ComponentViewModel> GetFilteredList(ComponentSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ComponentName))
            {
                return new();
            }
            using var context = new FactoryDatabase();
            return context.Components.Include(x => x.Plans).ThenInclude(x => x.Plan)
                .Where(x => x.ComponentName.Contains(model.ComponentName)).Select(x => x.GetViewModel).ToList();
        }

        public ComponentViewModel? GetElement(ComponentSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ComponentName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new FactoryDatabase();
            return context.Components.Include(x => x.Plans).ThenInclude(x => x.Plan)
                .FirstOrDefault(x => 
            (!string.IsNullOrEmpty(model.ComponentName) && x.ComponentName == model.ComponentName) || 
            (model.Id.HasValue && x.Id == model.Id))
                ?.GetViewModel;
        }

        public ComponentViewModel? Insert(ComponentBindingModel model)
        {
			using var context = new FactoryDatabase();
			var newComponent = Component.Create(context, model);
			if (newComponent == null)
			{
				return null;
			}
			context.Components.Add(newComponent);
			context.SaveChanges();
			return newComponent.GetViewModel;
		}

        public ComponentViewModel? Update(ComponentBindingModel model)
        {
            using var context = new FactoryDatabase();
            var component = context.Components.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            context.SaveChanges();
            return component.GetViewModel;
        }

        public ComponentViewModel? Delete(ComponentBindingModel model)
        {
            using var context = new FactoryDatabase();
            var element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Components.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
