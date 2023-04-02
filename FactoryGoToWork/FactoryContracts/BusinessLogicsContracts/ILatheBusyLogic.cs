using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface ILatheBusyLogic
    {
        List<LatheBusyViewModel>? ReadList(LatheBusySearchModel? model);
        LatheBusyViewModel? ReadElement(LatheBusySearchModel model);
        bool Create(LatheBusyBindingModel model);
        bool Update(LatheBusyBindingModel model);
        bool Delete(LatheBusyBindingModel model);
    }
}
