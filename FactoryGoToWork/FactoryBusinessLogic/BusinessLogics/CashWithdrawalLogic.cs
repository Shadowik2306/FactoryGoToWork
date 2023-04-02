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
	public class CashWithdrawalLogic : ICashWithdrawalLogic
	{
		private readonly ILogger _logger;

		private readonly ICashWithdrawalStorage _cashWithdrawalStorage;

		public CashWithdrawalLogic(ILogger<CashWithdrawalLogic> logger, ICashWithdrawalStorage cashWithdrawalStorage)
		{
			_logger = logger;
			_cashWithdrawalStorage = cashWithdrawalStorage;
		}

		public CashWithdrawalViewModel? ReadElement(CashWithdrawalSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			_logger.LogInformation("ReadElement. AccountId:{AccountId}. Sum:{Sum}. DateOperation:{DateOperation}. Id:{Id}", 
				model.AccountId, model.Sum, model.DateOperation, model?.Id);

			var element = _cashWithdrawalStorage.GetElement(model);

			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");

				return null;
			}

			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

			return element;
		}

		public List<CashWithdrawalViewModel>? ReadList(CashWithdrawalSearchModel? model)
		{
			_logger.LogInformation("ReadElement. AccountId:{AccountId}. Sum:{Sum}. DateOperation:{DateOperation}. Id:{Id}",
				model.AccountId, model.Sum, model.DateOperation, model?.Id);

			//list хранит весь список в случае, если model пришло со значением null на вход метода
			var list = model == null ? _cashWithdrawalStorage.GetFullList() : _cashWithdrawalStorage.GetFilteredList(model);

			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}

			_logger.LogInformation("ReadList. Count:{Count}", list.Count);

			return list;
		}

		public bool Create(CashWithdrawalBindingModel model)
		{
			CheckModel(model);

			if (_cashWithdrawalStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");

				return false;
			}

			return true;
		}

		public bool Update(CashWithdrawalBindingModel model)
		{
			CheckModel(model);

			if (_cashWithdrawalStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");

				return false;
			}

			return true;
		}

		public bool Delete(CashWithdrawalBindingModel model)
		{
			CheckModel(model, false);

			_logger.LogInformation("Delete. Id:{Id}", model.Id);

			if (_cashWithdrawalStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");

				return false;
			}

			return true;
		}

		//проверка входного аргумента для методов Insert, Update и Delete
		private void CheckModel(CashWithdrawalBindingModel model, bool withParams = true)
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

			//проверка на корректность Id счёта
			if (model.AccountId > 0)
			{
				throw new ArgumentNullException("Некорректный Id счёта", nameof(model.AccountId));
			}

			//проверка на корректность снимаемой суммы
			if (model.Sum <= 0)
			{
				throw new ArgumentNullException("Снимаемая сумма не может раняться нулю или быть меньше его", nameof(model.Sum));
			}

			//проверка на корректную дату операции
			if (model.DateOperation > DateTime.Now)
			{
				throw new ArgumentNullException("Дата операции не может быть позднее текущей", nameof(model.DateOperation));
			}

			_logger.LogInformation("CashWithdrawal: AccountId:{AccountId}. Sum:{Sum}. DateOperation:{DateOperation}. Id:{Id}", 
				model.AccountId, model.Sum, model.DateOperation, model?.Id);
		}
	}
}
