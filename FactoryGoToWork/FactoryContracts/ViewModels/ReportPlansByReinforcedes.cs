using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
    public class ReportPlansByReinforcedes
    {
        public string PlanName { get; set; } = string.Empty;
        public string ReinforcedName { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}
