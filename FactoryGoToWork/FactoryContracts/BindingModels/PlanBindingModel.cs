using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class PlanBindingModel : IPlanModel
    {
        public int Id { get; set; }

        public string PlanName { get; set; } = string.Empty;

        public Dictionary<int, ILatheModel> PlanLathes { get; set; } = new();

        public Dictionary<int, IComponentModel> PlanComponents { get; set; } = new();

        public DateTime date { get; set; }
    }
}
