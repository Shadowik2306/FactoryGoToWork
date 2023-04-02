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
    public interface IDebitingLogic
    {
        List<DebitingViewModel>? ReadList(DebitingSearchModel? model);

        DebitingViewModel? ReadElement(DebitingSearchModel model);

        bool Create(DebitingBindingModel model);

        bool Update(DebitingBindingModel model);

        bool Delete(DebitingBindingModel model);
    }
}
