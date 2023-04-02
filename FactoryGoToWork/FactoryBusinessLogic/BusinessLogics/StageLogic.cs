using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using Microsoft.Extensions.Logging;
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryContracts.SearchModels;

namespace FactoryBusinessLogic.BusinessLogics
{
    public class StageLogic : IStageLogic
    {
        private readonly ILogger _logger;
        private readonly IStageStorage _stageStorage;
        public StageLogic(ILogger<StageLogic> logger, IStageStorage stageStorage)
        {
            _logger = logger;
            _stageStorage = stageStorage;
        }
        public List<StageViewModel>? ReadList(StageSearchModel? model)
        {
            _logger.LogInformation("ReadList. StageId:{Id}", model?.Id);
            var list = model == null ? _stageStorage.GetFullList() : _stageStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public StageViewModel? ReadElement(StageSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. StageId:{Id}", model?.Id);
            var element = _stageStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(StageBindingModel model)
        {
            CheckModel(model);
            if (_stageStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(StageBindingModel model)
        {
            CheckModel(model);
            if (_stageStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(StageBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_stageStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(StageBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            _logger.LogInformation("Stage.Id:{Id}", model?.Id);
            var element = _stageStorage.GetElement(new StageSearchModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Компонент с таким названием уже есть");
            }
        }
    }
}
