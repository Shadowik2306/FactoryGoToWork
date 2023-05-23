using FactoryDataModels.Models;

namespace FactoryContracts.SearchModels
{
    public class LatheSearchModel
    {
        public int? Id { get; set; }

        public string? LatheName { get; set; }

        public int? MasterId { get; set; }

        public int? BusyId { get; set; }

        public Dictionary<int, (IReinforcedModel, int)>? LatheReinforcedes { get; set; }

		public Dictionary<int, (IComponentModel, int)>? LatheComponents { get; set;  }
	}
}
