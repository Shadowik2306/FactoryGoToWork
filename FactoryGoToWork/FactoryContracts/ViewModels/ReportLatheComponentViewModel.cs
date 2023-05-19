using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
    public class ReportLatheComponentViewModel
    {
        public string PlanName { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public List<string> Components { get; set; } = new();

        public List<string> Lathes { get; set; } = new();
    }
}
