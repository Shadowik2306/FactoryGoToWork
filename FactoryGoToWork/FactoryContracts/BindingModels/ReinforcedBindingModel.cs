using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class ReinforcedBindingModel : IReinforcedModel
    {
        public int Id { get; set; } 
        public string ReinforcedName { get; set; } = string.Empty;

        public int EngenierId { get; set; }
        public Dictionary<int, (IComponentModel, int)> ReinforcedComponents { get; set; } = new();

    }
}
