using FactoryDataModels.Models;

namespace FactoryContracts.SearchModels
{
    public class PlanSearchModel
    {
        public int? Id { get; set; }

        public string? PlanName { get; set; } = string.Empty;

        public Dictionary<int, (ILatheModel, int)>? PlanLathes { get; set; } = new();
    }
}
