using Microsoft.Extensions.Logging;
using FactoryContracts.BindingModels;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryBusinessLogic.BusinessLogics
{
    public class ClientLogic : IClientLogic
    {
        private readonly ILogger _logger;
        private readonly IClientStorage _clientStorage;

        public ClientLogic(ILogger<ClientLogic> logger, IClientStorage clientStorage) {
            _logger = logger;
            _clientStorage = clientStorage;
        }

        public bool Create(ClientBindingModel model)
        {
            CheckModel(model);
            if (_clientStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(ClientBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_clientStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        public ClientViewModel? ReadElement(ClientSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. Name:{Name}.Surname:{Surname}.Patronymic:{Patronymic}.Id:{ Id}", model.Name, model.Surname, model.Patronymic, model.Id);
            var element = _clientStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public List<ClientViewModel>? ReadList(ClientSearchModel? model)
        {
            _logger.LogInformation("ReadList. ClientId:{Id}", model?.Id);
            var list = model == null ? _clientStorage.GetFullList() : _clientStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public bool Update(ClientBindingModel model)
        {
            CheckModel(model);
            if (_clientStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(ClientBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Нет имени пользователя", nameof(model.Name));
            }
            if (string.IsNullOrEmpty(model.Surname))
            {
                throw new ArgumentNullException("Нет фамилии пользователя", nameof(model.Surname));
            }
            if (string.IsNullOrEmpty(model.Patronymic))
            {
                throw new ArgumentNullException("Нет отчества пользователя", nameof(model.Patronymic));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentNullException("Нет почты пользователя", nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Нет пароля пользователя", nameof(model.Password));
            }
            if (string.IsNullOrEmpty(model.Telephone))
            {
                throw new ArgumentNullException("Нет телефона пользователя", nameof(model.Telephone));
            }
            _logger.LogInformation("Client. Name:{Name}.Surname:{Surname}.Patronymic:{Patronymic}.Email:{Email}.Password:{Password}.Telephone:{Telephone}.Id:{Id}",
                model.Name, model.Surname, model.Patronymic, model.Email, model.Password, model.Telephone, model.Id);
            var element = _clientStorage.GetElement(new ClientSearchModel
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Surname,
                Email = model.Email,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Клиент с таким именем уже есть");
            }
        }
    }
}
