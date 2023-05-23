using FactoryDataModels.Models;
using System.ComponentModel;

namespace FactoryContracts.ViewModels
{
    public class ReinforcedViewModel : IReinforcedModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string ReinforcedName { get; set; } = string.Empty;
    }
}
