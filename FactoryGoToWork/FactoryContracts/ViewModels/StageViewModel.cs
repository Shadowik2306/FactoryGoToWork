using FactoryDataModels.Models;

namespace FactoryContracts.ViewModels
{
    public class StageViewModel : IStageModel
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public int ReinforsedId { get; set; }
    }
}
