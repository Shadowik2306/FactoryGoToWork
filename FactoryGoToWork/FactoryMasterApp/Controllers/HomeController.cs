using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryMasterApp;
using FactoryMasterApp.Models;
using Microsoft.AspNetCore.Mvc;

using PrecastConcretePlantContracts.ViewModels;
using System.Diagnostics;

namespace PrecastConcretePlantClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (APIMaster.Master == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIMaster.GetRequest<List<LatheViewModel>>($"api/main/getorders?clientId={APIMaster.Master.Id}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (APIMaster.Master == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIMaster.Master);
        }

        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (APIMaster.Master == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fio))
            {
                throw new Exception("Введите логин, пароль и ФИО");
            }
            APIMaster.PostRequest("api/master/updatedata", new MasterBindingModel
            {
                Id = APIMaster.Master.Id,
                Fio = fio,
                Email = login,
                Password = password
            });

            APIMaster.Master.Fio = fio;
            APIMaster.Master.Email = login;
            APIMaster.Master.Password = password;
            Response.Redirect("Index");
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
            APIMaster.Master = APIMaster.GetRequest<MasterViewModel>($"api/master/login?login={login}&password={password}");
            if (APIMaster.Master == null)
            {
                throw new Exception("Неверный логин/пароль");
            }
            Response.Redirect("Index");
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
            APIMaster.PostRequest("api/master/register", new MasterBindingModel
            {
                Fio = fio,
                Email = login,
                Password = password
            });
            Response.Redirect("Enter");
            return;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Reinforceds = APIMaster.GetRequest<List<ReinforcedViewModel>>("api/main/getlathelist");
            return View();
        }

        [HttpPost]
        public void Create(int busy, string title)
        {
            if (APIMaster.Master == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            APIMaster.PostRequest("api/main/createLathe", new LatheBindingModel
            {
                MasterId = APIMaster.Master.Id,
                LatheName = title,
                BusyId = busy
            });
            Response.Redirect("Index");
        }

    }
}