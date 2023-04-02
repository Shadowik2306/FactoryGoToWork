using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.StoragesContracts
{
    public interface ICardStorage
    {
        List<CardViewModel> GetFullList();

        List<CardViewModel> GetFilteredList(CardSearchModel model);

        CardViewModel? GetElement(CardSearchModel model);

        CardViewModel? Insert(CardBindingModel model);

        CardViewModel? Update(CardBindingModel model);

        CardViewModel? Delete(CardBindingModel model);
    }
}