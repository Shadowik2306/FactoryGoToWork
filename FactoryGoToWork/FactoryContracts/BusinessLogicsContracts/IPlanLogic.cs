using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface IPlanLogic
    {
        List<PlanViewModel>? ReadList(PlanSearchModel? model);
        PlanViewModel? ReadElement(PlanSearchModel model);
        bool Create(PlanBindingModel model);
        bool Update(PlanBindingModel model);
        bool Delete(PlanBindingModel model);


    }
}
