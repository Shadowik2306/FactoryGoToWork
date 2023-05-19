using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface ILatheLogic
    {
        List<LatheViewModel>? ReadList(LatheSearchModel? model);
        LatheViewModel? ReadElement(LatheSearchModel model);
        bool Create(LatheBindingModel model);
        bool Update(LatheBindingModel model);
        bool Delete(LatheBindingModel model);

        bool addReinforced(int latheId, int ReinforcedId);
    }
}
