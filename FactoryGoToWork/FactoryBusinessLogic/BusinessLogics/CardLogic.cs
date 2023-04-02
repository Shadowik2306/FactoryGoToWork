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
    public class CardLogic : ICardLogic
    {
        private readonly ILogger _logger;
        private readonly ICardStorage _cardStorage;

        public CardLogic(ILogger<CardLogic> logger, ICardStorage cardStorage) {
            _logger = logger;
            _cardStorage = cardStorage;
        }

        public bool Create(CardBindingModel model)
        {
            CheckModel(model);
            if (_cardStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(CardBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_cardStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        public CardViewModel? ReadElement(CardSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. CardNumber:{Number}.Id:{ Id}", model.Number, model.Id);
            var element = _cardStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public List<CardViewModel>? ReadList(CardSearchModel? model)
        {
            _logger.LogInformation("ReadList. CardId:{Id}", model?.Id);
            var list = model == null ? _cardStorage.GetFullList() : _cardStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public bool Update(CardBindingModel model)
        {
            CheckModel(model);
            if (_cardStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(CardBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Number))
            {
                throw new ArgumentNullException("Нет номера карты", nameof(model.Number));
            }
            if (string.IsNullOrEmpty(model.CVC))
            {
                throw new ArgumentNullException("Нет СVC карты", nameof(model.CVC));
            }
            if (model.Period < DateTime.Now)
            {
                throw new ArgumentNullException("Нет периода действия", nameof(model.Period));
            }
            
            _logger.LogInformation("Card. Number:{Number}.CVC:{CVC}.ClientId:{ClientID}.Patronymic:{Period}.Id:{Id}",
                model.Number, model.CVC, model.Period.ToString(), model.ClientID, model.Id);
        }
    }
}
