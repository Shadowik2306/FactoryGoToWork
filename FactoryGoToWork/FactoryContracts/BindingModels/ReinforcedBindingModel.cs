using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class ReinforcedBindingModel : IReinforcedModel
    {
        public int Id { get; set; } 
        public string ReinforcedName { get; set; } = string.Empty;
    }
}
