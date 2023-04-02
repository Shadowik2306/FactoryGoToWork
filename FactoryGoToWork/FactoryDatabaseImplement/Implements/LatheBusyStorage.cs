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

namespace FactoryDatabaseImplement.Implements
{
    public class LatheBusyStorage : ILatheBusyStorage
    {
        public List<LatheBusyViewModel> GetFullList()
        {
            using var context = new FactoryDatabase();
            return context.LatheBusies.Select(x => x.GetViewModel).ToList();
        }

        public List<LatheBusyViewModel> GetFilteredList(LatheBusySearchModel model)
        {
            if (model.Id is null)
            {
                return new();
            }
            using var context = new FactoryDatabase();
            return context.LatheBusies.Where(x => x.Percent == model.Percent).Select(x => x.GetViewModel).ToList();
        }

        public LatheBusyViewModel? GetElement(LatheBusySearchModel model)
        {
            if (model.Id is null)
            {
                return new();
            }
            using var context = new FactoryDatabase();
            return context.LatheBusies.FirstOrDefault(x => x.Percent == model.Percent)?.GetViewModel;
        }

        public LatheBusyViewModel? Insert(LatheBusyBindingModel model)
        {
            var newLatheBusy = LatheBusy.Create(new FactoryDatabase(), model);
            if (newLatheBusy == null)
            {
                return null;
            }
            using var context = new FactoryDatabase();
            context.LatheBusies.Add(newLatheBusy);
            context.SaveChanges();
            return newLatheBusy.GetViewModel;
        }

        public LatheBusyViewModel? Update(LatheBusyBindingModel model)
        {
            using var context = new FactoryDatabase();
            var latheBusy = context.LatheBusies.FirstOrDefault(x => x.Id == model.Id);
            if (latheBusy == null)
            {
                return null;
            }
            latheBusy.Update(model);
            context.SaveChanges();
            return latheBusy.GetViewModel;
        }

        public LatheBusyViewModel? Delete(LatheBusyBindingModel model)
        {
            using var context = new FactoryDatabase();
            var element = context.LatheBusies.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.LatheBusies.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
