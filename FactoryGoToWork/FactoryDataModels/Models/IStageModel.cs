using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    public interface IStageModel : IId
    {
        string Name { get; }
		DateTime StartDate { get; }
		DateTime EndDate { get; }
		int PlanId { get; }
	}
}
