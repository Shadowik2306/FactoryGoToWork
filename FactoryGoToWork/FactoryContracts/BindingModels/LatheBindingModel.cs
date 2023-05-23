using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class LatheBindingModel : ILatheModel
    {
        public int Id { get; set; }

        public string LatheName { get; set; } = String.Empty;

        public int MasterId { get; set; }

        public int BusyId { get; set; }

        public Dictionary<int, (IReinforcedModel, int)> LatheReinforcedes { get; set; } = new();

		public Dictionary<int, (IComponentModel, int)> LatheComponents { get; set; } = new();
	}
}
