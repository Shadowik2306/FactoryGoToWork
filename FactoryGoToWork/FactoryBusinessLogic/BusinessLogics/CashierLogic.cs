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
	public class CashierLogic : ICashierLogic
	{
		private readonly ILogger _logger;

		private readonly ICashierStorage _cashierStorage;

		public CashierLogic(ILogger<CashierLogic> logger, ICashierStorage cashierStorage)
		{
			_logger = logger;
			_cashierStorage = cashierStorage;
		}

		public CashierViewModel? ReadElement(CashierSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			_logger.LogInformation("ReadElement. CashierName:{Name}. CashierSurname:{Surname}. CashierPatronymic:{Patronymic}. Id:{Id}", 
				model.Name, model.Surname, model.Patronymic, model?.Id);

			var element = _cashierStorage.GetElement(model);

			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");

				return null;
			}

			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

			return element;
		}

		public List<CashierViewModel>? ReadList(CashierSearchModel? model)
		{
			_logger.LogInformation("ReadList. CashierName:{Name}. CashierSurname:{Surname}. CashierPatronymic:{Patronymic}. Id:{Id}", 
				model.Name, model.Surname, model.Patronymic, model?.Id);

			//list хранит весь список в случае, если model пришло со значением null на вход метода
			var list = model == null ? _cashierStorage.GetFullList() : _cashierStorage.GetFilteredList(model);

			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}

			_logger.LogInformation("ReadList. Count:{Count}", list.Count);

			return list;
		}

		public bool Create(CashierBindingModel model)
		{
			CheckModel(model);

			if (_cashierStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");

				return false;
			}

			return true;
		}

		public bool Update(CashierBindingModel model)
		{
			CheckModel(model);

			if (_cashierStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");

				return false;
			}

			return true;
		}

		public bool Delete(CashierBindingModel model)
		{
			CheckModel(model, false);

			_logger.LogInformation("Delete. Id:{Id}", model.Id);

			if (_cashierStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");

				return false;
			}

			return true;
		}

		//проверка входного аргумента для методов Insert, Update и Delete
		private void CheckModel(CashierBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			//так как при удалении передаём как параметр false
			if (!withParams)
			{
				return;
			}

			//проверка на наличие имени
			if (string.IsNullOrEmpty(model.Name))
			{
				throw new ArgumentNullException("Отсутствие имени в учётной записи", nameof(model.Name));
			}

			//проверка на наличие фамилия
			if (string.IsNullOrEmpty(model.Surname))
			{
				throw new ArgumentNullException("Отсутствие фамилии в учётной записи", nameof(model.Name));
			}

			//проверка на наличие отчество
			if (string.IsNullOrEmpty(model.Patronymic))
			{
				throw new ArgumentNullException("Отсутствие отчества в учётной записи", nameof(model.Name));
			}

			//проверка на наличие почты
			if (string.IsNullOrEmpty(model.Email))
			{
				throw new ArgumentNullException("Отсутствие почты в учётной записи (логина)", nameof(model.Email));
			}

			//проверка на наличие пароля
			if (string.IsNullOrEmpty(model.Password))
			{
				throw new ArgumentNullException("Отсутствие пароля в учётной записи", nameof(model.Password));
			}

			_logger.LogInformation("Cashier. CashierName:{Name}. CashierSurname:{Surname}. CashierPatronymic:{Patronymic}. " +
				"Email:{Email}. Password:{Password}. Id:{Id}",
				model.Name, model.Surname, model.Patronymic, model.Email, model.Password, model.Id);

			//для проверка на наличие такого же аккаунта
			var element = _cashierStorage.GetElement(new CashierSearchModel
			{
				Email = model.Email,
			});

			//если элемент найден и его Id не совпадает с Id переданного объекта
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Аккаунт с таким логином уже есть");
			}
		}
	}
}
