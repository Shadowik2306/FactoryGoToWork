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
	public class CardStorage
	{
        public CardViewModel? Delete(CardBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            var element = context.Cards.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Cards.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public CardViewModel? GetElement(CardSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Number) && !model.Id.HasValue)
            {
                return null;
            }

            using var context = new BankYouBancruptDatabase();

            return context.Cards
                    .FirstOrDefault(x => (!string.IsNullOrEmpty(model.Number) && x.Number == model.Number) ||
                                        (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public List<CardViewModel> GetFilteredList(CardSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Number))
            {
                return new();
            }

            using var context = new BankYouBancruptDatabase();

            return context.Cards
                    .Where(x => x.Number.Contains(model.Number))
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<CardViewModel> GetFullList()
        {
            using var context = new BankYouBancruptDatabase();

            return context.Cards
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public CardViewModel? Insert(CardBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();

            var newCard = Card.Create(context, model);

            if (newCard == null)
            {
                return null;
            }

            context.Cards.Add(newCard);
            context.SaveChanges();

            return newCard.GetViewModel;
        }

        public CardViewModel? Update(CardBindingModel model)
        {
            using var context = new BankYouBancruptDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var card = context.Cards.FirstOrDefault(rec => rec.Id == model.Id);

                if (card == null)
                {
                    return null;
                }

                card.Update(model);
                context.SaveChanges();
                transaction.Commit();

                return card.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
