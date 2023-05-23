using Microsoft.Extensions.Logging;
using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FactoryBusinessLogic.BusinessLogics
{
	public class ReinforcedLogic : IReinforcedLogic
	{
		private readonly ILogger _logger;
		private readonly IReinforcedStorage _reinforcedStorage;
		public ReinforcedLogic(ILogger<ReinforcedLogic> logger, IReinforcedStorage reinforcedStorage)
		{
			_logger = logger;
			_reinforcedStorage = reinforcedStorage;
		}

		public bool Create(ReinforcedBindingModel model)
		{
			CheckModel(model);
			if (_reinforcedStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(ReinforcedBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_reinforcedStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		public ReinforcedViewModel? ReadElement(ReinforcedSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. ReinforcedId:{Id}", model?.Id);
			var element = _reinforcedStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public List<ReinforcedViewModel>? ReadList(ReinforcedSearchModel? model)
		{
			_logger.LogInformation("ReadList.ReinforcedId:{Id}", model?.Id);
			var list = model == null ? _reinforcedStorage.GetFullList() : _reinforcedStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public bool Update(ReinforcedBindingModel model)
		{
			CheckModel(model);
			if (_reinforcedStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(ReinforcedBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}

			var element = _reinforcedStorage.GetElement(new ReinforcedSearchModel
			{
				ReinforcedName=model.ReinforcedName
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Деталь с таким названием уже есть");
			}
			_logger.LogInformation("Reinforced. Id:{ Id}.Name:{Name}.", model?.Id, model?.ReinforcedName);
		}
	}
}
