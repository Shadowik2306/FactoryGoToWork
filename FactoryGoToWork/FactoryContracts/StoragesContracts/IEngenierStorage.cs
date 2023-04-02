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
    public interface IEngenierStorage
    {
        List<EngenierViewModel> GetFullList();

        List<EngenierViewModel> GetFilteredList(EngenierSearchModel model);

        EngenierViewModel? GetElement(EngenierSearchModel model);

        EngenierViewModel? Insert(EngenierBindingModel model);

        EngenierViewModel? Update(EngenierBindingModel model);

        EngenierViewModel? Delete(EngenierBindingModel model);
    }
}
