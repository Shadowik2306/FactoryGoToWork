using FactoryDataModels.Models;
using System.ComponentModel;

namespace FactoryContracts.ViewModels
{
    public class LatheViewModel : ILatheModel
    {
        public int Id { get; set; }

        [DisplayName("Название станка")]
        public string LatheName { get; set; } = String.Empty;

        public int MasterId { get; set; }

        public int BusyId { get; set; }

        public Dictionary<int, (IReinforcedModel, int)> LatheReinforcedes { get; set; } = new();

		public Dictionary<int, (IComponentModel, int)> LatheComponents { get; set; } = new();
	}
}
