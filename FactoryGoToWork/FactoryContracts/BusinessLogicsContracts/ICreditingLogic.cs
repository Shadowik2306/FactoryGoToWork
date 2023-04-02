using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface ICreditingLogic
    {
        List<CreditingViewModel>? ReadList(CreditingSearchModel? model);

        CreditingViewModel? ReadElement(CreditingSearchModel model);

        bool Create(CreditingBindingModel model);

        bool Update(CreditingBindingModel model);

        bool Delete(CreditingBindingModel model);
    }
}
