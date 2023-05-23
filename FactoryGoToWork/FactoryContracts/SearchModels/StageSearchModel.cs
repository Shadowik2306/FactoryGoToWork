using FactoryDataModels.Models;

namespace FactoryContracts.SearchModels
{
    public class StageSearchModel
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? PlanId { get; set; }

		public DateTime? DateTo { get; set; }

		public DateTime? DateFrom { get; set; }
	}
}
