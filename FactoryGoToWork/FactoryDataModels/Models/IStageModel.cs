using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    public interface IStageModel : IId
    {
        public int PlanId { get; }

        public int ReinforsedId { get; }
    }
}
