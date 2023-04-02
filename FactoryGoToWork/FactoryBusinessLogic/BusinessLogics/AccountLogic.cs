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
    public class AccountLogic : IAccountLogic
    {
        private readonly ILogger _logger;

        private readonly IAccountStorage _accountStorage;

        public AccountLogic(ILogger<AccountLogic> logger, IAccountStorage accountStorage)
        {
            _logger = logger;
            _accountStorage = accountStorage;
        }

        public AccountViewModel? ReadElement(AccountSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _logger.LogInformation("ReadElement. AccountNumber:{Name}. Id:{Id}", model.AccountNumber, model?.Id);

            var element = _accountStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");

                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

            return element;
        }

        public List<AccountViewModel>? ReadList(AccountSearchModel? model)
        {
            _logger.LogInformation("ReadList. AccountNumber:{Name}. Id:{Id}", model.AccountNumber, model?.Id);

            //list хранит весь список в случае, если model пришло со значением null на вход метода
            var list = model == null ? _accountStorage.GetFullList() : _accountStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);

            return list;
        }

        public bool Create(AccountBindingModel model)
        {
            CheckModel(model);

            if (_accountStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");

                return false;
            }

            return true;
        }

        public bool Update(AccountBindingModel model)
        {
            CheckModel(model);

            if (_accountStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");

                return false;
            }

            return true;
        }

        public bool Delete(AccountBindingModel model)
        {
            CheckModel(model, false);

            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_accountStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");

                return false;
            }

            return true;
        }

        //проверка входного аргумента для методов Insert, Update и Delete
        private void CheckModel(AccountBindingModel model, bool withParams = true)
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

            //проверка на наличие номера счёта
            if (string.IsNullOrEmpty(model.AccountNumber))
            {
                throw new ArgumentNullException("Отсутствие номера у счёта", nameof(model.AccountNumber));
            }

            //проверка на наличие id владельца
            if (model.CashierId < 0)
            {
                throw new ArgumentNullException("Некорректный Id владельца счёта", nameof(model.CashierId));
            }

            //проверка на наличие id кассира, создавшего счёт
            if (model.CashierId < 0)
            {
                throw new ArgumentNullException("Некорректный Id кассира, открывшего счёт", nameof(model.CashierId));
            }

            //проверка на наличие пароля счёта
            if (string.IsNullOrEmpty(model.PasswordAccount))
            {
                throw new ArgumentNullException("Некорректный пароль счёта", nameof(model.PasswordAccount));
            }

            //проверка на корректную дату открытия счёта
            if (model.DateOpen > DateTime.Now)
            {
                throw new ArgumentNullException("Дата открытия счёта не может быть ранее текущей", nameof(model.DateOpen));
            }

            _logger.LogInformation("Account. AccountNumber:{AccountNumber}. PasswordAccount:{PasswordAccount}. ClientId:{ClientId}. " +
                "CashierId:{CashierId}. DateOpen:{DateOpen}. Id:{Id}",
                model.AccountNumber, model.PasswordAccount, model.ClientId, model.CashierId, model.DateOpen, model.Id);

            //для проверка на наличие такого же счёта
            var element = _accountStorage.GetElement(new AccountSearchModel
            {
                AccountNumber = model.AccountNumber,
            });

            //если элемент найден и его Id не совпадает с Id переданного объекта
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Счёт с таким номером уже существует");
            }
        }
    }
}
