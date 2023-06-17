using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
    public class ReportLatheByComponents
    {
        public string ComponentName { get; set; } = string.Empty;
        public string LatheName { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}
