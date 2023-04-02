using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    public interface ILatheModel : IId
    {
        string LatheName { get; }
        int MasterId { get; }
        int BusyId { get; }
        Dictionary<int, (IReinforcedModel, int)> LatheReinforcedes { get; }
    }
}
