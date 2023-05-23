using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface IReinforcedLogic
    {
        List<ReinforcedViewModel>? ReadList(ReinforcedSearchModel? model);
        ReinforcedViewModel? ReadElement(ReinforcedSearchModel model);
        bool Create(ReinforcedBindingModel model);
        bool Update(ReinforcedBindingModel model);
        bool Delete(ReinforcedBindingModel model);
    }
}
