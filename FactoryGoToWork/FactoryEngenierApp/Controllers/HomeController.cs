using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryEngenierApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FactoryEngenierApp.Controllers
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
            if (APIEngenier.Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEngenier.GetRequest<List<ReinforcedViewModel>>($"api/Read/getReinforcedes"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (APIEngenier.Engenier == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEngenier.Engenier);
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
            APIEngenier.Engenier = APIEngenier.GetRequest<EngenierViewModel>($"api/Main/LoginEngenier?login={login}&password={password}");
            if (APIEngenier.Engenier == null)
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
            APIEngenier.PostRequest("api/Main/RegisterEngenier", new EngenierBindingModel
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
            if (APIEngenier.Engenier == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            APIEngenier.PostRequest("api/Main/CreateReinforced", new ReinforcedBindingModel
            {
                EngenierId = APIEngenier.Engenier.Id,
                ReinforcedName = title
            });
            Response.Redirect("Index");
        }

    }
}