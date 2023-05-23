using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class PlanBindingModel : IPlanModel
    {
        public int Id { get; set; }

        public string PlanName { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public Dictionary<int, (IReinforcedModel, int)> PlanReinforcedes { get; set; } = new();
	}
}
