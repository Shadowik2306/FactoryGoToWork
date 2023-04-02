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
    public class DebitingStorage : IDebitingStorage
    {

        public List<DebitingViewModel> GetFullList()
        {
            using var context = new BankYouBancruptDatabase();

            return context.Debitings
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<DebitingViewModel> GetFilteredList(DebitingSearchModel model)
        {
            if (model.CardId < 0)
            {
                return new();
            }

            using var context = new BankYouBancruptDatabase();

            return context.Debitings
                    .Where(x => x.CardId == model.CardId)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public DebitingViewModel? GetElement(DebitingSearchModel model)
        {
            if (model.CardId < 0 && !model.Id.HasValue)
            {
                return null;
            }

            using var context = new BankYouBancruptDatabase();

            return context.Debitings
                    .FirstOrDefault(x => (!(model.CardId < 0) && x.CardId == model.CardId) ||
                                        (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public DebitingViewModel? Insert(DebitingBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();

            var newDebiting = Debiting.Create(context, model);

            if (newDebiting == null)
            {
                return null;
            }

            context.Debitings.Add(newDebiting);
            context.SaveChanges();

            return newDebiting.GetViewModel;
        }

        public DebitingViewModel? Update(DebitingBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var debiting = context.Debitings.FirstOrDefault(rec => rec.Id == model.Id);

                if (debiting == null)
                {
                    return null;
                }

                debiting.Update(model);
                context.SaveChanges();
                transaction.Commit();

                return debiting.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public DebitingViewModel? Delete(DebitingBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var element = context.Debitings
                .Include(x => x.CardId)
                .FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Debitings.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
