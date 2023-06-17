
using DocumentFormat.OpenXml.ExtendedProperties;
using FactoryBusinessLogic.Mail;
using FactoryBusinessLogic.OfficePackage;
using FactoryBusinessLogic.OfficePackage.HelperModels;
using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
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
        private readonly IComponentLogic _componentLogic;
        private readonly IPlanLogic _planLogic;
        private readonly ILatheLogic _latheLogic;
        private readonly ILatheBusyStorage _latheBusyStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        private readonly MailWorker _mailKitWorker;


        public ReportLogic(IComponentLogic componentLogic, IPlanLogic planLogic, ILatheLogic latheLogic, ILatheBusyStorage latheBusyStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf, MailWorker mailKitWorker)
        {
            _planLogic = planLogic;
            _componentLogic = componentLogic;
            _latheLogic = latheLogic;
            _latheBusyStorage = latheBusyStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
            _mailKitWorker = mailKitWorker;
        }


        public List<ReportLatheViewModel> GetLatheByBusy(ReportBindingModel model, DateTime dateFrom, DateTime dateTo)
        {
            var plans = _planLogic.ReadList(new PlanSearchModel()
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            }).ToList();

            var result = new List<ReportLatheViewModel>();

            foreach (var plan in plans) {
                foreach (var component in _componentLogic.ReadList(null).Where(x => x.ComponentPlans.ContainsKey(plan.Id))) {
                    foreach (var lathe in _latheLogic.ReadList(null).Where(x => x.LatheComponents.ContainsKey(component.Id))) {
                        result.Add(new ReportLatheViewModel()
                        {
                            PlanName = plan.PlanName,
                            dateFrom = plan.StartDate,
                            dateTo = plan.EndDate,
                            ComponentName = component.ComponentName,
                            LatheName = lathe.LatheName
                        });
                    }
                }
            }

            return result;
        }


        public List<ReportPlansByReinforcedes> GetPlansByReinforcedes(ReinforcedViewModel reinforced) { 
            var result = new List<ReportPlansByReinforcedes>();


            foreach (var plan in _planLogic.ReadList(null).Where(x => x.PlanReinforcedes.ContainsKey(reinforced.Id)))
            {
                result.Add(new ReportPlansByReinforcedes()
                {
                    ReinforcedName = reinforced.ReinforcedName,
                    PlanName = plan.PlanName,
                    Count = plan.PlanReinforcedes[reinforced.Id].Item2
                });
            }


            return result;
        }

        public void SaveToExcelFileMaster(ReportBindingModel model, ReinforcedViewModel reinforceds)
        {
            _saveToExcel.CreateReportMaster(new ExcelInfoMaster()
            {
                FileName = "Отчет по изделию.xls",
                Title = "Отчет по изделию " + reinforceds.ReinforcedName,
                Plans = GetPlansByReinforcedes(reinforceds)
            });
        }

        public void SaveToWordFileMaster(ReportBindingModel model, ReinforcedViewModel reinforceds)
        {
            _saveToWord.CreatePlanReport(new WordInfoMaster()
            {
                FileName = "Отчет по изделию.doc",
                Title = "Отчет по изделию " + reinforceds.ReinforcedName,
                Plans = GetPlansByReinforcedes(reinforceds)
            });
        }

        public void SaveToPdfFileMaster(ReportBindingModel model, DateTime dateFrom, DateTime dateTo, MasterViewModel master)
        {
            _saveToPdf.CreatePDFMaster(new PdfInfoMaster() { 
                FileName = "Отчет по движению станков.pdf",
                Title = "Отчет по движению станков",
                DateFrom = dateFrom,
                DateTo = dateTo,
                Lathe = GetLatheByBusy(model, dateFrom, dateTo)
            });

            _mailKitWorker.SendMailAsync(new()
            {
                MailAddress = master.Email,
                Subject = "Отчет по движению станков",
                Text = $"Отчёт по состоянию на {DateTime.Now}",
                File = System.IO.File.ReadAllBytes("Отчет по движению станков.pdf"),
                FileName = "Отчет по движению станков.pdf"
            });
        }

        

        public List<ReportLatheComponentViewModel> GetPlanLathesAndComponents(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
