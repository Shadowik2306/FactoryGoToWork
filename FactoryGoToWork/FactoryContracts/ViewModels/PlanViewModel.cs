using FactoryDataModels.Models;
using System.ComponentModel;

namespace FactoryContracts.ViewModels
{
    public class PlanViewModel : IPlanModel
    {
        public int Id { get; set; }

        [DisplayName("Название производства")]
        public string PlanName { get; set; } = string.Empty;

        public Dictionary<int, (ILatheModel, int)> PlanLathes { get; set; } = new();

        public Dictionary<int, (IComponentModel, int)> PlanComponents { get; set; } = new();
    }
}
