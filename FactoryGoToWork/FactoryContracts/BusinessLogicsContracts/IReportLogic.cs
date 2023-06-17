
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryContracts.ViewModels;
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
        public List<ReportLatheViewModel> GetLatheByBusy(ReportBindingModel model, DateTime dateFrom, DateTime dateTo);

        public List<ReportPlansByReinforcedes> GetPlansByReinforcedes(ReinforcedViewModel reinforceds);
        void SaveToWordFileMaster(ReportBindingModel model, ReinforcedViewModel reinforceds);
        void SaveToExcelFileMaster(ReportBindingModel model, ReinforcedViewModel reinforceds);
        public void SaveToPdfFileMaster(ReportBindingModel model, DateTime dateFrom, DateTime dateTo, MasterViewModel master);
    }
}
