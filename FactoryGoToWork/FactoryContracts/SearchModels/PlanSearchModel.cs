using FactoryDataModels.Models;

namespace FactoryContracts.SearchModels
{
    public class PlanSearchModel
    {
        public int? Id { get; set; }

        public string? PlanName { get; set; } = string.Empty;

        public Dictionary<int, ILatheModel>? PlanLathes { get; set; } = new();

        public Dictionary<int, IComponentModel>? PlanComponent { get; set; } = new();

        public DateTime? DateTo { get; set; }

        public DateTime? DateFrom { get; set; }
    }
}
