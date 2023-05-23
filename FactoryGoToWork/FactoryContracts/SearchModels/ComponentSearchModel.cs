using FactoryDataModels.Models;

namespace FactoryContracts.SearchModels
{
    public class ComponentSearchModel
    {
        public int? Id { get; set; }
        public string? ComponentName { get; set; }

        public double? Cost { get; set; }
		public int? EngenierId { get; set; }
		public Dictionary<int, (IPlanModel, int)>? ComponentPlans { get; set;  }
	}
}
