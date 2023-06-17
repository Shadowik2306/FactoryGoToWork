using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using FactoryEngenierApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FactoryEngenierApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static EngenierViewModel Engenier { get; set; } = null;

        public readonly IComponentLogic _componentLogic;
        public readonly IEngenierLogic _engenierLogic;
        public readonly ILatheBusyLogic _latheBusyLogic;
        public readonly ILatheLogic _latheLogic;
        public readonly IMasterLogic _masterLogic;
        public readonly IPlanLogic _planLogic;
        public readonly IReinforcedLogic _reinforcedLogic;
        public readonly IStageLogic _stageLogic;

        public HomeController(ILogger<HomeController> logger, IComponentLogic componentLogic, IEngenierLogic engenierLogic,
            ILatheBusyLogic latheBusyLogic, ILatheLogic latheLogic, IMasterLogic masterLogic, IPlanLogic planLogic,
             IReinforcedLogic reinforcedLogicz, IStageLogic stageLogic)
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
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Engenier);
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
            Engenier = _engenierLogic.ReadElement(new EngenierSearchModel() { 
                Email = login,
                Password = password
            });
            if (Engenier == null)
            {
                throw new Exception("Неверный логин/пароль");
            }
            Response.Redirect("IndexComponent");
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
            _engenierLogic.Create(new EngenierBindingModel
            {
                Fio = fio,
                Email = login,
                Password = password
            });
            Response.Redirect("Enter");
            return;
        }


        public IActionResult IndexComponent()
        {
            if (Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(_componentLogic.ReadList(null));
        }

        [HttpGet]
        public IActionResult CreateComponent()
        {
            if (Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(_planLogic.ReadList(null).Select(x => new CheckboxViewModel() { 
                Id = x.Id,
                Count = 0,
                IsChecked = false,
                LabelName = x.PlanName,
                Object = x
            }).ToList());
        }


        [HttpPost]
        public void CreateComponent(string title, int cost, List<CheckboxViewModel> plans)
        {
            if (Engenier == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
           _componentLogic.Create(new ComponentBindingModel
            {
                ComponentName = title,
                EngenierId = Engenier.Id,
                Cost = cost,
                ComponentPlans = plans.Where(x => x.IsChecked).ToDictionary(x => x.Id, x => (x.Object as IPlanModel, x.Count))
           });;
            Response.Redirect("IndexComponent");
        }

        public IActionResult IndexPlan()
        {
            if (Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(_planLogic.ReadList(null));
        }

        [HttpGet]
        public IActionResult CreatePlan()
        {
            if (Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(_reinforcedLogic.ReadList(null).Select(x => new CheckboxViewModel()
            {
                Id = x.Id,
                Count = 0,
                IsChecked = false,
                LabelName = x.ReinforcedName,
                Object = x
            }).ToList());
        }


        [HttpPost]
        public void CreatePlan(string title, DateTime start, DateTime end, List<CheckboxViewModel> reinforcedes)
        {
            if (Engenier == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            _planLogic.Create(new PlanBindingModel() { 
                PlanName = title,
                StartDate = start,
                EndDate = end,
                PlanReinforcedes = reinforcedes.Where(x => x.IsChecked).ToDictionary(x => x.Id, x => (x.Object as IReinforcedModel, x.Count))
            });
            Response.Redirect("IndexComponent");
        }

        
    }
}