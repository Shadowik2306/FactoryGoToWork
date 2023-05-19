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

        public bool addComponent(int planId, int ComponentId);

        public bool addLathe(int planId, int LatheId);
    }
}
