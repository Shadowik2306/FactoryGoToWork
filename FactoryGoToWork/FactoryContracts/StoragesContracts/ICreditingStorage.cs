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
    public interface IDebitingStorage
    {
        List<DebitingViewModel> GetFullList();

        List<DebitingViewModel> GetFilteredList(DebitingSearchModel model);

        DebitingViewModel? GetElement(DebitingSearchModel model);

        DebitingViewModel? Insert(DebitingBindingModel model);

        DebitingViewModel? Update(DebitingBindingModel model);

        DebitingViewModel? Delete(DebitingBindingModel model);
    }
}