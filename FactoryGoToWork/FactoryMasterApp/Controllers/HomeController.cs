using FactoryBusinessLogic.BusinessLogics;
using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using FactoryMasterApp;
using FactoryMasterApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PrecastConcretePlantClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static MasterViewModel Master { get; set; } = null;

        public readonly IComponentLogic _componentLogic;
        public readonly IEngenierLogic _engenierLogic;
        public readonly ILatheBusyLogic _latheBusyLogic;
        public readonly ILatheLogic _latheLogic;
        public readonly IMasterLogic _masterLogic;
        public readonly IPlanLogic _planLogic;
        public readonly IReinforcedLogic _reinforcedLogic;
        public readonly IStageLogic _stageLogic;
        public readonly IReportLogic _reportLogic;

        public HomeController(ILogger<HomeController> logger, IComponentLogic componentLogic, IEngenierLogic engenierLogic,
            ILatheBusyLogic latheBusyLogic, ILatheLogic latheLogic, IMasterLogic masterLogic, IPlanLogic planLogic,
             IReinforcedLogic reinforcedLogicz, IStageLogic stageLogic, IReportLogic reportLogic)
        {
            _logger = logger;
            _componentLogic = componentLogic;
            _engenierLogic = engenierLogic;
            _latheBusyLogic = latheBusyLogic;
            _latheLogic = latheLogic;
            _masterLogic = masterLogic;
            _planLogic = planLogic;
            _reinforcedLogic = reinforcedLogicz;
            _stageLogic = stageLogic;
            _reportLogic = reportLogic;
        }

        public IActionResult IndexLathe()
        {
            if (Master == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(_latheLogic.ReadList(null));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Master == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Master);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Введите логин и пароль");
            }
            Master = _masterLogic.ReadElement(new MasterSearchModel() { 
                Email = login,
                Password = password
            });
            if (Master == null)
            {
                throw new Exception("Неверный логин/пароль");
            }
            Response.Redirect("IndexLathe");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fio))
            {
                throw new Exception("Введите логин, пароль и ФИО");
            }
            _masterLogic.Create(new MasterBindingModel()
            {
                Fio = fio,
                Email = login,
                Password = password
            });
            Response.Redirect("Enter");
            return;
        }

        [HttpGet]
        public IActionResult CreateLathe()
        {
            if (Master == null) {
                Response.Redirect("Enter");
            }

            CheckboxLatheViewModel model = new CheckboxLatheViewModel();

            model.Reinforcedes = _reinforcedLogic.ReadList(null).Select(x => new CheckboxViewModel() { 
                Id = x.Id,
                LabelName = x.ReinforcedName,
                IsChecked = false,
                Count = 0,
                Object = x
            }).ToList();

            model.Components = _componentLogic.ReadList(null).Select(x => new CheckboxViewModel()
            {
                Id = x.Id,
                LabelName = x.ComponentName,
                IsChecked = false,
                Count = 0,
                Object = x
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public void CreateLathe(string title, int percent, CheckboxLatheViewModel comAndRei)
        {
            if (Master == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            _latheBusyLogic.Create(new LatheBusyBindingModel()
            {
                Date = DateTime.Now,
                Percent = percent
            });
            _latheLogic.Create(new LatheBindingModel() 
            {
                MasterId = Master.Id,
                BusyId = _latheBusyLogic.ReadList(null).Last().Id,
                LatheName = title,
                LatheComponents = comAndRei.Components.Where(x => x.IsChecked).ToDictionary(x => x.Id, x => (x.Object as IComponentModel, x.Count)),
                LatheReinforcedes = comAndRei.Reinforcedes.Where(x => x.IsChecked).ToDictionary(x => x.Id, x => (x.Object as IReinforcedModel, x.Count))
            });
            Response.Redirect("IndexLathe");
        }

        [HttpGet]
        public IActionResult IndexReinforcedes() {
            if (Master == null)
            {
                Response.Redirect("Enter");
            }

            return View(_reinforcedLogic.ReadList(null));
        }

        [HttpGet]
        public IActionResult CreateReinforcedes()
        {
            if (Master == null)
            {
                Response.Redirect("Enter");
            }

            return View();
        }

        [HttpPost]
        public void CreateReinforcedes(string title)
        {
            if (Master == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            _reinforcedLogic.Create(new ReinforcedBindingModel() { 
                ReinforcedName = title,
            });
            Response.Redirect("IndexLathe");
        }

        [HttpGet]
        public IActionResult ReportsDock() {
            ViewBag.Reinforced = _reinforcedLogic.ReadList(null);
            return View();
        }

        [HttpPost]
        public IActionResult ReportsDock(int reinforcedId) {
            ViewBag.Reinforced = _reinforcedLogic.ReadList(null);
            return View(_reportLogic.GetPlansByReinforcedes(_reinforcedLogic.ReadElement(new ReinforcedSearchModel() { Id = reinforcedId })));
        }


        [HttpPost]
        public IActionResult CreateWordReport(int reinforcedId)
        {
            _reportLogic.SaveToWordFileMaster(new ReportBindingModel(), _reinforcedLogic.ReadElement(new ReinforcedSearchModel() { Id = reinforcedId }));
            return Redirect("ReportsDock");
        }

        [HttpPost]
        public IActionResult CreateExcelReport(int reinforcedId)
        {
            _reportLogic.SaveToExcelFileMaster(new ReportBindingModel(), _reinforcedLogic.ReadElement(new ReinforcedSearchModel() { Id = reinforcedId }));
            return Redirect("ReportsDock");
        }

        [HttpGet]
        public IActionResult ReportsPdf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportsPdf(DateTime dateFrom, DateTime dateTo)
        {
            return View(_reportLogic.GetLatheByBusy(new ReportBindingModel(), dateFrom, dateTo));
        }

        [HttpPost]
        public IActionResult CreatePdfReport(DateTime dateFrom, DateTime dateTo)
        {
            _reportLogic.SaveToPdfFileMaster(new ReportBindingModel(), dateFrom, dateTo, Master);
            return View(_reportLogic.GetLatheByBusy(new ReportBindingModel(), dateFrom, dateTo));
        }
    }
}