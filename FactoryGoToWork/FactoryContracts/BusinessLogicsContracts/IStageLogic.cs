using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface IStageLogic
    {
        List<StageViewModel>? ReadList(StageSearchModel? model);
        StageViewModel? ReadElement(StageSearchModel model);
        bool Create(StageBindingModel model);
        bool Update(StageBindingModel model);
        bool Delete(StageBindingModel model);
    }
}
