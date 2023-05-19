using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryBusinessLogic.BusinessLogics
{
	public class MasterLogic : IMasterLogic
	{
		private readonly ILogger _logger;

		private readonly IMasterStorage _masterStorage;

		public MasterLogic(ILogger<MasterLogic> logger, IMasterStorage masterStorage)
		{
			_logger = logger;
			_masterStorage = masterStorage;
		}

		public MasterViewModel? ReadElement(MasterSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			_logger.LogInformation("ReadElement. MasterName:{Name}. MasterSurname:{Surname}. MasterPatronymic:{Patronymic}. Id:{Id}", 
				model?.Name, model?.Surname, model?.Patronymic, model?.Id);

			var element = _masterStorage.GetElement(model);

			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");

				return null;
			}

			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

			return element;
		}

		public List<MasterViewModel>? ReadList(MasterSearchModel? model)
		{
			_logger.LogInformation("ReadList. MasterName:{Name}. MasterSurname:{Surname}. MasterPatronymic:{Patronymic}. Id:{Id}", 
				model?.Name, model?.Surname, model?.Patronymic, model?.Id);

			var list = model == null ? _masterStorage.GetFullList() : _masterStorage.GetFilteredList(model);

			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}

			_logger.LogInformation("ReadList. Count:{Count}", list.Count);

			return list;
		}

		public bool Create(MasterBindingModel model)
		{
			CheckModel(model);

			if (_masterStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");

				return false;
			}

			return true;
		}

		public bool Update(MasterBindingModel model)
		{
			CheckModel(model);

			if (_masterStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");

				return false;
			}

			return true;
		}

		public bool Delete(MasterBindingModel model)
		{
			CheckModel(model, false);

			_logger.LogInformation("Delete. Id:{Id}", model.Id);

			if (_masterStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");

				return false;
			}

			return true;
		}

		private void CheckModel(MasterBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			if (!withParams)
			{
				return;
			}
			if (string.IsNullOrEmpty(model.Fio))
			{
				throw new ArgumentNullException("Отсутствие имени в учётной записи", nameof(model.Fio));
			}

			if (string.IsNullOrEmpty(model.Email))
			{
				throw new ArgumentNullException("Отсутствие почты в учётной записи (логина)", nameof(model.Email));
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				throw new ArgumentNullException("Отсутствие пароля в учётной записи", nameof(model.Password));
			}

			_logger.LogInformation("Master. MasterName:{Fio}.Email:{Email}.Password:{Password}.Id:{Id}",
				model.Fio, model.Email, model.Password, model.Id);
			var element = _masterStorage.GetElement(new MasterSearchModel
			{
				Email = model.Email,
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Аккаунт с таким логином уже есть");
			}
		}
	}
}
