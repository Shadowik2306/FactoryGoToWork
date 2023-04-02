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
	public class ClientStorage
	{
        public ClientViewModel? Delete(ClientBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public ClientViewModel? GetElement(ClientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Password) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new BankYouBancruptDatabase();
            return context.Clients.FirstOrDefault(x =>
            (!string.IsNullOrEmpty(model.Email) && x.Email == model.Email && !string.IsNullOrEmpty(model.Password) && x.Password == model.Password) ||
            (model.Id.HasValue && x.Id == model.Id))
                ?.GetViewModel;
        }

        public List<ClientViewModel> GetFilteredList(ClientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return new();
            }
            using var context = new BankYouBancruptDatabase();
            return context.Clients.Where(x => x.Name.Contains(model.Name)).Select(x => x.GetViewModel).ToList();
        }

        public List<ClientViewModel> GetFullList()
        {
            using var context = new BankYouBancruptDatabase();
            return context.Clients.Select(x => x.GetViewModel).ToList();
        }

        public ClientViewModel? Insert(ClientBindingModel model)
        {
            var newComponent = Client.Create(model);
            if (newComponent == null)
            {
                return null;
            }
            using var context = new BankYouBancruptDatabase();
            context.Clients.Add(newComponent);
            context.SaveChanges();
            return newComponent.GetViewModel;
        }

        public ClientViewModel? Update(ClientBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var component = context.Clients.FirstOrDefault(x => x.Id == model.Id);
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
