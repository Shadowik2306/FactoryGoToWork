using FactoryDataModels.Models;

namespace FactoryContracts.ViewModels
{
    public class StageViewModel : IStageModel
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
	}
}
