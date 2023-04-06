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
        public List<(string Component, int Count)> Components { get; set; } = new();

        public List<(string Component, int Count)> Lathes { get; set; } = new();
    }
}
