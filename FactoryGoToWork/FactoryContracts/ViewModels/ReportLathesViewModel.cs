using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
    public class ReportLatheViewModel
    {
        public string PlanName { get; set; } = string.Empty;

        public DateTime dateFrom { get; set; } = DateTime.Now;
        public DateTime dateTo { get; set; } = DateTime.Now;

        public string ComponentName { get; set; } = String.Empty;

        public string LatheName { get; set; } = String.Empty;
    }
}
