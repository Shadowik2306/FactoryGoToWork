using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using Microsoft.Extensions.Logging;
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryContracts.SearchModels;

namespace FactoryBusinessLogic.BusinessLogics
{
	public class LatheBusyLogic : ILatheBusyLogic
	{
		private readonly ILogger _logger;
		private readonly ILatheBusyStorage _latheBusyStorage;
		public LatheBusyLogic(ILogger<LatheBusyLogic> logger, ILatheBusyStorage latheBusyStorage)
		{
			_logger = logger;
			_latheBusyStorage = latheBusyStorage;
		}
		public bool Create(LatheBusyBindingModel model)
		{
			CheckModel(model);
			if (_latheBusyStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(LatheBusyBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_latheBusyStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		public LatheBusyViewModel? ReadElement(LatheBusySearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. LatheBusyId:{Id}", model?.Id);
			var element = _latheBusyStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public List<LatheBusyViewModel>? ReadList(LatheBusySearchModel? model)
		{
			_logger.LogInformation("ReadList.LatheBusyId:{Id}", model?.Id);
			var list = model == null ? _latheBusyStorage.GetFullList() : _latheBusyStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public bool Update(LatheBusyBindingModel model)
		{
			CheckModel(model);
			if (_latheBusyStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(LatheBusyBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			_logger.LogInformation("LatheBusy. Id:{ Id}.", model?.Id);
		}
	}
}
