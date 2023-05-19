using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using FactoryDatabaseImplement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace FactoryRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IMasterLogic _masterLogic;
        private readonly IComponentLogic _componentLogic;
        private readonly IEngenierLogic _engenierLogic;
        private readonly ILatheBusyLogic _latheBusyLogic;
        private readonly ILatheLogic _latheLogic;
        private readonly IPlanLogic _planLogic;
        private readonly IReinforcedLogic _reinforcedLogic;
        private readonly IStageLogic _stageLogic;

        public MainController(IMasterLogic masterLogic, IComponentLogic componentLogic, IEngenierLogic engenierLogic,
            ILatheBusyLogic latheBusyLogic, ILatheLogic latheLogic, IPlanLogic planLogic, IReinforcedLogic reinforced,
            IStageLogic stageLogic, ILogger<MainController> logger)
        {
            _logger = logger;
            _masterLogic = masterLogic;
            _componentLogic = componentLogic;
            _engenierLogic = engenierLogic;
            _latheBusyLogic = latheBusyLogic;
            _latheLogic = latheLogic;
            _planLogic = planLogic;
            _reinforcedLogic = reinforced;
            _stageLogic = stageLogic;
        }


        [HttpPost]
        public void RegisterMaster(MasterBindingModel model)
        {
            try
            {
                _masterLogic.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void RegisterEngenier(EngenierBindingModel model)
        {
            try
            {
                _engenierLogic.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpGet]
        public MasterViewModel? LoginMaster(string login, string password)
        {
            try
            {
                return _masterLogic.ReadElement(new MasterSearchModel
                {
                    Email = login,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка входа в систему");
                throw;
            }
        }

        [HttpGet]
        public EngenierViewModel? LoginEngenier(string login, string password)
        {
            try
            {
                return _engenierLogic.ReadElement(new EngenierSearchModel
                {
                    Email = login,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка входа в систему");
                throw;
            }
        }

        [HttpPost]
        public void CreateLathe(LatheBindingModel model)
        {
            try
            {
                var latheBusy = _latheBusyLogic.Create(new LatheBusyBindingModel()
                {
                    Percent = 0,
                    Date = DateTime.Now
                });
                _latheLogic.Create(new LatheBindingModel() {
                    LatheName = model.LatheName,
                    MasterId = model.MasterId,
                    BusyId = _latheBusyLogic.ReadList(null).Last().Id,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void CreateComponent(ComponentBindingModel model) {
            try
            {
                _componentLogic.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void CreateReinforced(ReinforcedBindingModel model)
        {
            try
            {
                _reinforcedLogic.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void AddComponentToReinforced(int reinforcedId, int componentId)
        {
            try
            {
                _reinforcedLogic.addComponent(reinforcedId, componentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void CreatePlan(PlanBindingModel model) {
            try
            {
                _planLogic.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }


        [HttpPost]
        public void CreatePlanComponent(int planId, int componentId)
        {
            try
            {
                _planLogic.addComponent(planId, componentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void CreatePlanLathe(int planId, int latheId)
        {
            try
            {
                _planLogic.addLathe(planId, latheId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpPost]
        public void CreateLatheReinforsed(int latheId, int reinforcedId) {
            try
            {
                _latheLogic.addReinforced(latheId, reinforcedId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }
    }
}