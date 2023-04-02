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
    public interface ICreditingStorage
    {
        List<CreditingViewModel> GetFullList();

        List<CreditingViewModel> GetFilteredList(CreditingSearchModel model);

        CreditingViewModel? GetElement(CreditingSearchModel model);

        CreditingViewModel? Insert(CreditingBindingModel model);

        CreditingViewModel? Update(CreditingBindingModel model);

        CreditingViewModel? Delete(CreditingBindingModel model);
    }
}