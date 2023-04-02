using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.StoragesContracts
{
    public interface IStageStorage
    {
        List<StageViewModel> GetFullList();
        List<StageViewModel> GetFilteredList(StageSearchModel model);
        StageViewModel? GetElement(StageSearchModel model);
        StageViewModel? Insert(StageBindingModel model);
        StageViewModel? Update(StageBindingModel model);
        StageViewModel? Delete(StageBindingModel model);
    }
}
