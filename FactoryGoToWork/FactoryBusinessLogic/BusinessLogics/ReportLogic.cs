
using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using PrecastConcretePlantContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FactoryBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IPlanStorage _planStorage;
        private readonly ILatheStorage _latheStorage;
        private readonly ILatheBusyStorage _latheBusyStorage;


        public ReportLogic(IComponentStorage componentStorage, IPlanStorage planStorage, ILatheStorage latheStorage, ILatheBusyStorage latheBusyStorage)
        {
            _planStorage = planStorage;
            _componentStorage = componentStorage;
            _latheStorage = latheStorage;
            _latheBusyStorage = latheBusyStorage;
        }

        public List<ReportLatheComponentViewModel> GetPlanLathesAndComponents(ReportBindingModel model)
        {
            return _planStorage.GetFilteredList(new PlanSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo})
                .Select(x => new ReportLatheComponentViewModel { 
                    PlanName = x.PlanName,
                    Components = x.PlanComponents.Select(x => (x.Value.Item1.ComponentName, x.Value.Item2)).ToList(),
                    Lathes = x.PlanLathes.Select(x => (x.Value.Item1.LatheName, x.Value.Item2)).ToList()
                })
            .ToList();
        }

        public List<ReportLatheViewModel> GetLatheByBusy(ReportBindingModel model)
        {
            return _latheBusyStorage.GetFilteredList(new LatheBusySearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
                    .Select(x => new ReportLatheViewModel
                    {
                        Lathes = _latheStorage.GetFilteredList(new LatheSearchModel { BusyId = x.Id}).Select(y => y.LatheName).ToList(),
                    })
                    .ToList();
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveReinforcedComponentToExcelFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveReinforcedesToWordFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
