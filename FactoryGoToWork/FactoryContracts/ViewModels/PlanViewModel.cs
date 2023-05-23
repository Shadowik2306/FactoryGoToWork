using FactoryDataModels.Models;
using System.ComponentModel;

namespace FactoryContracts.ViewModels
{
    public class PlanViewModel : IPlanModel
    {
        public int Id { get; set; }

        [DisplayName("Название производства")]
        public string PlanName { get; set; } = string.Empty;

        [DisplayName("Дата начала")]
        public DateTime StartDate { get; set; }
		[DisplayName("Дата выполнения")]
		public DateTime EndDate { get; set; }

		public Dictionary<int, (IReinforcedModel, int)> PlanReinforcedes { get; set; } = new();
	}
}
