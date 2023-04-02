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
	public class MoneyTransferLogic : IMoneyTransferLogic
	{
		private readonly ILogger _logger;

		private readonly IMoneyTransferStorage _moneyTransferStorage;

		public MoneyTransferLogic(ILogger<MoneyTransferLogic> logger, IMoneyTransferStorage moneyTransferStorage)
		{
			_logger = logger;
			_moneyTransferStorage = moneyTransferStorage;
		}

		public MoneyTransferViewModel? ReadElement(MoneyTransferSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			_logger.LogInformation("ReadElement. AccountSenderId:{AccountSenderId}. AccountPayeeId:{AccountPayeeId}. Sum:{Sum}. Id:{Id}", 
				model.AccountSenderId, model.AccountPayeeId, model.Sum, model?.Id);

			var element = _moneyTransferStorage.GetElement(model);

			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");

				return null;
			}

			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

			return element;
		}

		public List<MoneyTransferViewModel>? ReadList(MoneyTransferSearchModel? model)
		{
			_logger.LogInformation("ReadElement. AccountSenderId:{AccountSenderId}. AccountPayeeId:{AccountPayeeId}. Sum:{Sum}. Id:{Id}",
				model.AccountSenderId, model.AccountPayeeId, model.Sum, model?.Id);

			//list хранит весь список в случае, если model пришло со значением null на вход метода
			var list = model == null ? _moneyTransferStorage.GetFullList() : _moneyTransferStorage.GetFilteredList(model);

			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}

			_logger.LogInformation("ReadList. Count:{Count}", list.Count);

			return list;
		}

		public bool Create(MoneyTransferBindingModel model)
		{
			CheckModel(model);

			if (_moneyTransferStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");

				return false;
			}

			return true;
		}

		public bool Update(MoneyTransferBindingModel model)
		{
			CheckModel(model);

			if (_moneyTransferStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");

				return false;
			}

			return true;
		}

		public bool Delete(MoneyTransferBindingModel model)
		{
			CheckModel(model, false);

			_logger.LogInformation("Delete. Id:{Id}", model.Id);

			if (_moneyTransferStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");

				return false;
			}

			return true;
		}

		//проверка входного аргумента для методов Insert, Update и Delete
		private void CheckModel(MoneyTransferBindingModel model, bool withParams = true)
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

			//проверка корректности Id счёта отправителя
			if (model.AccountSenderId < 0)
			{
				throw new ArgumentNullException("Отсутствие Id у счёта отправителя", nameof(model.AccountSenderId));
			}

			//проверка корректности Id счёта получателя
			if (model.AccountPayeeId < 0)
			{
				throw new ArgumentNullException("Отсутствие Id у счёта получателя", nameof(model.AccountPayeeId));
			}

			//проверка на корректную сумму перевода
			if (model.Sum <= 0)
			{
				throw new ArgumentNullException("Сумма перевода не может раняться нулю или быть меньше его", nameof(model.Sum));
			}

			//проверка на корректную дату открытия счёта
			if (model.DateOperation > DateTime.Now)
			{
				throw new ArgumentNullException("Дата операции не может быть ранее текущей", nameof(model.DateOperation));
			}

			_logger.LogInformation("ReadElement. AccountSenderId:{AccountSenderId}. AccountPayeeId:{AccountPayeeId}. Sum:{Sum}. DateOperation:{DateOperation}. Id:{Id}",
				model.AccountSenderId, model.AccountPayeeId, model.Sum, model.DateOperation, model?.Id);
		}
	}
}
