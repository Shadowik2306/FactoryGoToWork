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
    public class CreditingStorage : ICreditingStorage
    {

        public List<CreditingViewModel> GetFullList()
        {
            using var context = new BankYouBancruptDatabase();

            return context.Creditings
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<CreditingViewModel> GetFilteredList(CreditingSearchModel model)
        {
            if (model.CardId < 0)
            {
                return new();
            }

            using var context = new BankYouBancruptDatabase();

            return context.Creditings
                    .Where(x => x.CardId == model.CardId)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public CreditingViewModel? GetElement(CreditingSearchModel model)
        {
            if (model.CardId < 0 && !model.Id.HasValue)
            {
                return null;
            }

            using var context = new BankYouBancruptDatabase();

            return context.Creditings
                    .FirstOrDefault(x => (!(model.CardId < 0) && x.CardId == model.CardId) ||
                                        (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public CreditingViewModel? Insert(CreditingBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();

            var newCrediting = Crediting.Create(context, model);

            if (newCrediting == null)
            {
                return null;
            }

            context.Creditings.Add(newCrediting);
            context.SaveChanges();

            return newCrediting.GetViewModel;
        }

        public CreditingViewModel? Update(CreditingBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var crediting = context.Creditings.FirstOrDefault(rec => rec.Id == model.Id);

                if (crediting == null)
                {
                    return null;
                }

                crediting.Update(model);
                context.SaveChanges();
                transaction.Commit();

                return crediting.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public CreditingViewModel? Delete(CreditingBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var element = context.Creditings
                .Include(x => x.CardId)
                .FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Creditings.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
