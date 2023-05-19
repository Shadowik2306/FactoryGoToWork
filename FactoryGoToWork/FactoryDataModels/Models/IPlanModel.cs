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

        Dictionary<int, ILatheModel> PlanLathes { get; }

        Dictionary<int, IComponentModel> PlanComponents { get; }

        DateTime date { get; }
    }
}
