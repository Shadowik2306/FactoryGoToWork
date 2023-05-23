using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
	public class ComponentBindingModel : IComponentModel
	{
		public int Id { get; set; }

		public string ComponentName { get; set; } = string.Empty;
		public double Cost { get; set; }

		public int EngenierId { get; set; }

		public Dictionary<int, (IPlanModel, int)> ComponentPlans { get; set; } = new();
	}
}
