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
    public class ReadController : ControllerBase
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

        public ReadController(IMasterLogic masterLogic, IComponentLogic componentLogic, IEngenierLogic engenierLogic,
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


        [HttpGet]
        public List<MasterViewModel> getMasters() {
            try
            {
                return _masterLogic.ReadList(null);
            }
            catch (Exception ex){
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpGet]
        public List<ComponentViewModel> getComponents()
        {
            try
            {
                return _componentLogic.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }


        [HttpGet]
        public List<EngenierViewModel> getEngeniers()
        {
            try
            {
                return _engenierLogic.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }

        [HttpGet]
        public List<LatheBusyViewModel> getLatheBusies()
        {
            try
            {
                return _latheBusyLogic.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }


        [HttpGet]
        public List<LatheViewModel> getLathes()
        {
            try
            {
                return _latheLogic.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }


        [HttpGet]
        public List<PlanViewModel> getPlans()
        {
            try
            {
                return _planLogic.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }


        [HttpGet]
        public List<ReinforcedViewModel> getReinforcedes()
        {
            try
            {
                return _reinforcedLogic.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации");
                throw;
            }
        }
    }
}