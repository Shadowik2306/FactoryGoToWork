using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.StoragesContracts
{
    public interface IReinforcedStorage
    {
        List<ReinforcedViewModel> GetFullList();
        List<ReinforcedViewModel> GetFilteredList(ReinforcedSearchModel model);
        ReinforcedViewModel? GetElement(ReinforcedSearchModel model);
        ReinforcedViewModel? Insert(ReinforcedBindingModel model);
        ReinforcedViewModel? Update(ReinforcedBindingModel model);
        ReinforcedViewModel? Delete(ReinforcedBindingModel model);
    }
}
