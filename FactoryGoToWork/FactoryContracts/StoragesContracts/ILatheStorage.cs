using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.StoragesContracts
{
    public interface ILatheStorage
    {
        List<LatheViewModel> GetFullList();
        List<LatheViewModel> GetFilteredList(LatheSearchModel model);
        LatheViewModel? GetElement(LatheSearchModel model);
        LatheViewModel? Insert(LatheBindingModel model);
        LatheViewModel? Update(LatheBindingModel model);
        LatheViewModel? Delete(LatheBindingModel model);
    }
}
