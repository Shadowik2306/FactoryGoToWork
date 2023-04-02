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
    public class DebitingLogic : IDebitingLogic
    {
        private readonly ILogger _logger;
        private readonly IDebitingStorage _debitingStorage;

        public DebitingLogic(ILogger<DebitingLogic> logger, IDebitingStorage debitingStorage) {
            _logger = logger;
            _debitingStorage = debitingStorage;
        }

        public bool Create(DebitingBindingModel model)
        {
            CheckModel(model);
            if (_debitingStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(DebitingBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_debitingStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        public DebitingViewModel? ReadElement(DebitingSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. DebitingId:{ Id }", model.Id);
            var element = _debitingStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public List<DebitingViewModel>? ReadList(DebitingSearchModel? model)
        {
            _logger.LogInformation("ReadList. DebitingId:{Id}", model?.Id);
            var list = model == null ? _debitingStorage.GetFullList() : _debitingStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public bool Update(DebitingBindingModel model)
        {
            CheckModel(model);
            if (_debitingStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(DebitingBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (model.Sum <= 0)
            {
                throw new ArgumentNullException("Сумма операции должна быть больше 0", nameof(model.Sum));
            }
            if (model.Date < DateTime.Now)
            {
                throw new ArgumentNullException("Дата не может быть меньше текущего времени", nameof(model.Date));
            }
            
            _logger.LogInformation("Debiting. Sum:{Sum}.CardId:{CardId}.Date:{date}.Id:{Id}",
                model.Sum, model.CardId, model.Date.ToString(), model.Id);
        }
    }
}
