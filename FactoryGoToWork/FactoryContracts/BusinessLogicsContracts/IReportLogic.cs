
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
        public List<ReportLatheViewModel> GetLatheByBusy(ReportBindingModel model, DateTime dateFrom, DateTime dateTo);
        public List<ReportPlansByReinforcedes> GetPlansByReinforcedes(ReinforcedViewModel reinforceds);
        void SaveToWordFileMaster(ReportBindingModel model, ReinforcedViewModel reinforceds);
        void SaveToExcelFileMaster(ReportBindingModel model, ReinforcedViewModel reinforceds);
        public void SaveToPdfFileMaster(ReportBindingModel model, DateTime dateFrom, DateTime dateTo, MasterViewModel master);
        public List<ReportLatheByComponents> GetLatheByComponent(ComponentViewModel reinforceds);
        void SaveToWordFileEngenier(ReportBindingModel model, ComponentViewModel reinforceds);
        void SaveToExcelFileEngenier(ReportBindingModel model, ComponentViewModel reinforceds);
        public void SaveToPdfFileEngenier(ReportBindingModel model, DateTime dateFrom, DateTime dateTo, EngenierViewModel master);
    }
}
