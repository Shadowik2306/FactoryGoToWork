using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class StageBindingModel : IStageModel
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public int ReinforsedId { get; set; }
    }
}
