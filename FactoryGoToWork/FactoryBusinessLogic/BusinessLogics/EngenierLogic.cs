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
    public class EngenierLogic : IEngenierLogic
    {
        private readonly ILogger _logger;
        private readonly IEngenierStorage _engenierStorage;

        public EngenierLogic(ILogger<EngenierLogic> logger, IEngenierStorage engenierStorage) {
            _logger = logger;
            _engenierStorage = engenierStorage;
        }

        public bool Create(EngenierBindingModel model)
        {
            CheckModel(model);
            if (_engenierStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(EngenierBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_engenierStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        public EngenierViewModel? ReadElement(EngenierSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. Name:{Name}.Surname:{Surname}.Patronymic:{Patronymic}.Id:{ Id}", model.Name, model.Surname, model.Patronymic, model.Id);
            var element = _engenierStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public List<EngenierViewModel>? ReadList(EngenierSearchModel? model)
        {
            _logger.LogInformation("ReadList. EngenierId:{Id}", model?.Id);
            var list = model == null ? _engenierStorage.GetFullList() : _engenierStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public bool Update(EngenierBindingModel model)
        {
            CheckModel(model);
            if (_engenierStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(EngenierBindingModel model, bool withParams = true)
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
                throw new ArgumentNullException("Нет имени пользователя", nameof(model.Fio));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentNullException("Нет почты пользователя", nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Нет пароля пользователя", nameof(model.Password));
            }
            _logger.LogInformation("Engenier. Name:{Fio}.Email:{Email}.Password:{Password}.Id:{Id}",
                model.Fio, model.Email, model.Password, model.Id);
            var element = _engenierStorage.GetElement(new EngenierSearchModel
            {
                Name = model.Fio,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Клиент с таким именем уже есть");
            }
        }
    }
}
