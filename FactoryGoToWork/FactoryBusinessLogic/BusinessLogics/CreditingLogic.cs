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
    public class CreditingLogic : ICreditingLogic
    {
        private readonly ILogger _logger;
        private readonly ICreditingStorage _creditingStorage;

        public CreditingLogic(ILogger<CreditingLogic> logger, ICreditingStorage creditingStorage) {
            _logger = logger;
            _creditingStorage = creditingStorage;
        }

        public bool Create(CreditingBindingModel model)
        {
            CheckModel(model);
            if (_creditingStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(CreditingBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_creditingStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        public CreditingViewModel? ReadElement(CreditingSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. CreditingId:{ Id }", model.Id);
            var element = _creditingStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public List<CreditingViewModel>? ReadList(CreditingSearchModel? model)
        {
            _logger.LogInformation("ReadList. CreditingId:{Id}", model?.Id);
            var list = model == null ? _creditingStorage.GetFullList() : _creditingStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public bool Update(CreditingBindingModel model)
        {
            CheckModel(model);
            if (_creditingStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(CreditingBindingModel model, bool withParams = true)
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
            
            _logger.LogInformation("Crediting. Sum:{Sum}.CardId:{CardId}.Date:{date}.Id:{Id}",
                model.Sum, model.CardId, model.Date.ToString(), model.Id);
        }
    }
}
