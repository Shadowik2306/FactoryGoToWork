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
		public PlanLogic(ILogger<PlanLogic> logger, IPlanStorage planStorage)
		{
			_logger = logger;
			_planStorage = planStorage;
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

		public PlanViewModel? ReadElement(PlanSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. PlanId:{ PlanId}", model?.Id);
			var element = _planStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public List<PlanViewModel>? ReadList(PlanSearchModel? model)
		{
			_logger.LogInformation("ReadList.PlanId:{ PlanId}", model?.Id);
			var list = model == null ? _planStorage.GetFullList() : _planStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
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
			var element = _planStorage.GetElement(new PlanSearchModel
			{
				PlanName = model.PlanName,
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("план с таким именем уже есть");
			}
			_logger.LogInformation("Plan. Id:{ Id}.PlanName:{PlanName}.", model?.Id, model?.PlanName);
		}
	}
}
