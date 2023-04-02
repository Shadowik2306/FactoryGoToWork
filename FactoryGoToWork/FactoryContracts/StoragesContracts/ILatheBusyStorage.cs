using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.StoragesContracts
{
    public interface ILatheBusyStorage
    {
        List<LatheBusyViewModel> GetFullList();
        List<LatheBusyViewModel> GetFilteredList(LatheBusySearchModel model);
        LatheBusyViewModel? GetElement(LatheBusySearchModel model);
        LatheBusyViewModel? Insert(LatheBusyBindingModel model);
        LatheBusyViewModel? Update(LatheBusyBindingModel model);
        LatheBusyViewModel? Delete(LatheBusyBindingModel model);
    }
}
