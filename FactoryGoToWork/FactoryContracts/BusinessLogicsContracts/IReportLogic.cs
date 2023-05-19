
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using PrecastConcretePlantContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportLatheComponentViewModel> GetPlanLathesAndComponents(ReportBindingModel model);
        List<ReportLatheViewModel> GetLatheByBusy(ReportBindingModel model);
        void SaveToWordFile(ReportBindingModel model);
        void SaveToExcelFile(ReportBindingModel model);
        void SaveToPdfFile(ReportBindingModel model);
    }
}
