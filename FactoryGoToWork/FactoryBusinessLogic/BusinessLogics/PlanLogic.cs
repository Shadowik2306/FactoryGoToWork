using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using Microsoft.Extensions.Logging;
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryContracts.SearchModels;

namespace FactoryBusinessLogic.BusinessLogics
{
    public class PlanLogic : IPlanLogic
    {
        private readonly ILogger _logger;
        private readonly IPlanStorage _planStorage;
        private readonly IComponentStorage componentStorage;
        private readonly ILatheStorage latheStorage;
        public PlanLogic(ILogger<PlanLogic> logger, IPlanStorage planStorage, IComponentStorage componentStorage, ILatheStorage latheStorage)
        {
            _logger = logger;
            _planStorage = planStorage;
            this.componentStorage = componentStorage;
            this.latheStorage = latheStorage;
        }
        public List<PlanViewModel>? ReadList(PlanSearchModel? model)
        {
            _logger.LogInformation("ReadList. PlanName:{PlanName}.Id:{ Id}", model?.PlanName, model?.Id);
            var list = model == null ? _planStorage.GetFullList() : _planStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public PlanViewModel? ReadElement(PlanSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. ComponentName:{PlanName}.Id:{ Id}", model?.PlanName, model.Id);
            var element = _planStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(PlanBindingModel model)
        {
            CheckModel(model);
            if (_planStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(PlanBindingModel model)
        {
            CheckModel(model);
            if (_planStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(PlanBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_planStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(PlanBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.PlanName))
            {
                throw new ArgumentNullException("Нет названия плана", nameof(model.PlanName));
            }
            _logger.LogInformation("Component. PlanName:{PlanName}.Id: { Id}", model.PlanName, model.Id);
            var element = _planStorage.GetElement(new PlanSearchModel
            {
                PlanName = model.PlanName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Компонент с таким названием уже есть");
            }
        }


        public bool addComponent(int planId, int ComponentId)
        {
            try
            {
                var plan = _planStorage.GetElement(new PlanSearchModel() { Id = planId });
                var model = new PlanBindingModel()
                {
                    PlanName = plan.PlanName,
                    Id = plan.Id,
                    PlanLathes = plan.PlanLathes,
                    date = plan.date,
                    PlanComponents = plan.PlanComponents
                };
                var component = componentStorage.GetElement(new ComponentSearchModel() { Id = ComponentId });
                model.PlanComponents.Add(component.Id, component);
                
                Update(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool addLathe(int planId, int latheId)
        {
            try
            {
                var plan = _planStorage.GetElement(new PlanSearchModel() { Id = planId });
                var model = new PlanBindingModel()
                {
                    PlanName = plan.PlanName,
                    Id = plan.Id,
                    PlanLathes = plan.PlanLathes,
                    date = plan.date,
                    PlanComponents = plan.PlanComponents
                };
                var component = latheStorage.GetElement(new LatheSearchModel() { Id = latheId });
                model.PlanLathes.Add(component.Id, component);
                Update(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
