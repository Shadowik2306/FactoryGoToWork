using FactoryBusinessLogic.BusinessLogics;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoMaster
    {
        public string FileName { get; set; } = string.Empty; 

        public string Title { get; set; } = string.Empty;

        public List<ReportPlansByReinforcedes> Plans { get; set; } = new();

	}
}
