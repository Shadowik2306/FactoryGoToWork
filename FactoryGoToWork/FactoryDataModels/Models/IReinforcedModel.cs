using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    public interface IReinforcedModel : IId
    {
        string ReinforcedName { get; }
        int EngenierId { get; }
        Dictionary<int, (IComponentModel, int)> ReinforcedComponents { get; }
    }
}
