using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.StoragesContracts
{
    public interface IPlanStorage
    {
        List<PlanViewModel> GetFullList();
        List<PlanViewModel> GetFilteredList(PlanSearchModel model);
        PlanViewModel? GetElement(PlanSearchModel model);
        PlanViewModel? Insert(PlanBindingModel model);
        PlanViewModel? Update(PlanBindingModel model);
        PlanViewModel? Delete(PlanBindingModel model);
    }
}
