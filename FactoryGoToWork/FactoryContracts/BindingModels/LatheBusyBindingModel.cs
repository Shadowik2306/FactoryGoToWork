using FactoryDataModels.Models;

namespace FactoryContracts.BindingModels
{
    public class LatheBusyBindingModel : ILatheBusyModel
    {
        public int Id { get; set; }

        public int Percent { get; set; }
    }
}
