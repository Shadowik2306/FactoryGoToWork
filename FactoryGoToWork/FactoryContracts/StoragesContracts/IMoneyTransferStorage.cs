using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.StoragesContracts
{
	public interface IMoneyTransferStorage
	{
		List<MoneyTransferViewModel> GetFullList();

		List<MoneyTransferViewModel> GetFilteredList(MoneyTransferSearchModel model);

		MoneyTransferViewModel? GetElement(MoneyTransferSearchModel model);

		MoneyTransferViewModel? Insert(MoneyTransferBindingModel model);

		MoneyTransferViewModel? Update(MoneyTransferBindingModel model);

		MoneyTransferViewModel? Delete(MoneyTransferBindingModel model);
	}
}
