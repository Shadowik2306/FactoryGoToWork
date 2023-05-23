using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using Microsoft.Extensions.Logging;
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryContracts.SearchModels;

namespace FactoryBusinessLogic.BusinessLogics
{
	public class LatheLogic : ILatheLogic
	{
		private readonly ILogger _logger;
		private readonly ILatheStorage _latheStorage;
		public LatheLogic(ILogger<LatheLogic> logger, ILatheStorage latheStorage)
		{
			_logger = logger;
			_latheStorage = latheStorage;
		}
		public bool Create(LatheBindingModel model)
		{
			CheckModel(model);
			if (_latheStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}

		public bool Delete(LatheBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_latheStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}

		public LatheViewModel? ReadElement(LatheSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. LatheId:{ Id}", model?.Id);
			var element = _latheStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}

		public List<LatheViewModel>? ReadList(LatheSearchModel? model)
		{
			_logger.LogInformation("ReadList.LatheId:{Id}", model?.Id);
			var list = model == null ? _latheStorage.GetFullList() : _latheStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}

		public bool Update(LatheBindingModel model)
		{
			CheckModel(model);
			if (_latheStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(LatheBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (string.IsNullOrEmpty(model.LatheName))
			{
				throw new ArgumentNullException("Нет названия станка", nameof(model.LatheName));
			}
			var element = _latheStorage.GetElement(new LatheSearchModel
			{
				LatheName = model.LatheName,
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Станок с таким именем уже есть");
			}
			_logger.LogInformation("Lathe. Id:{ Id}.Title:{Title}.", model?.Id, model?.LatheName);
		}
	}
}
