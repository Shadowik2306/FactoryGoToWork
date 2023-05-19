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
            return View(APIMaster.GetRequest<List<LatheViewModel>>($"api/Read/getLathes"));
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
            APIMaster.Master = APIMaster.GetRequest<MasterViewModel>($"api/Main/LoginMaster?login={login}&password={password}");
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
            APIMaster.PostRequest("api/Main/RegisterMaster", new MasterBindingModel
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
            return View();
        }

        [HttpPost]
        public void Create(string title)
        {
            if (APIMaster.Master == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            APIMaster.PostRequest("api/Main/CreateLathe", new LatheBindingModel
            {
                MasterId = APIMaster.Master.Id,
                LatheName = title
            });
            Response.Redirect("Index");
        }

    }
}