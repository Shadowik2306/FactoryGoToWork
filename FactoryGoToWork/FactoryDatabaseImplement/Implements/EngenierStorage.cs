using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using FactoryImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Implements
{
	public class EngenierStorage
	{
        public EngenierViewModel? Delete(EngenierBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var element = context.Engeniers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Engeniers.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public EngenierViewModel? GetElement(EngenierSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Password) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new BankYouBancruptDatabase();
            return context.Engeniers.FirstOrDefault(x =>
            (!string.IsNullOrEmpty(model.Email) && x.Email == model.Email && !string.IsNullOrEmpty(model.Password) && x.Password == model.Password) ||
            (model.Id.HasValue && x.Id == model.Id))
                ?.GetViewModel;
        }

        public List<EngenierViewModel> GetFilteredList(EngenierSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return new();
            }
            using var context = new BankYouBancruptDatabase();
            return context.Engeniers.Where(x => x.Fio.Contains(model.Name)).Select(x => x.GetViewModel).ToList();
        }

        public List<EngenierViewModel> GetFullList()
        {
            using var context = new BankYouBancruptDatabase();
            return context.Engeniers.Select(x => x.GetViewModel).ToList();
        }

        public EngenierViewModel? Insert(EngenierBindingModel model)
        {
            var newComponent = Engenier.Create(model);
            if (newComponent == null)
            {
                return null;
            }
            using var context = new BankYouBancruptDatabase();
            context.Engeniers.Add(newComponent);
            context.SaveChanges();
            return newComponent.GetViewModel;
        }

        public EngenierViewModel? Update(EngenierBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var component = context.Engeniers.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            context.SaveChanges();
            return component.GetViewModel;
        }
    }
}
