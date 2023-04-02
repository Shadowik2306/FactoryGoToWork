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
    public interface IClientLogic
    {
        List<ClientViewModel>? ReadList(ClientSearchModel? model);

        ClientViewModel? ReadElement(ClientSearchModel model);

        bool Create(ClientBindingModel model);

        bool Update(ClientBindingModel model);

        bool Delete(ClientBindingModel model);
    }
}
