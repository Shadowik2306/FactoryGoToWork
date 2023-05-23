using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class StageBindingModel : IStageModel
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
	}
}
