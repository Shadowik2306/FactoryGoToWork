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
    public interface ICardLogic
    {
        List<CardViewModel>? ReadList(CardSearchModel? model);

        CardViewModel? ReadElement(CardSearchModel model);

        bool Create(CardBindingModel model);

        bool Update(CardBindingModel model);

        bool Delete(CardBindingModel model);
    }
}
