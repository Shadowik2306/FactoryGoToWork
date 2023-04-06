using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    public interface IPlanModel : IId
    {
        string PlanName { get; }

        Dictionary<int, (ILatheModel, int)> PlanLathes { get; }

        Dictionary<int, (IComponentModel, int)> PlanComponents { get; }

        DateTime date { get; }
    }
}
