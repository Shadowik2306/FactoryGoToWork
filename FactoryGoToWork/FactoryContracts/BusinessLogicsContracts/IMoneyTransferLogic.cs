using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BusinessLogicsContracts
{
	public interface IMoneyTransferLogic
	{
		List<MoneyTransferViewModel>? ReadList(MoneyTransferSearchModel? model);

		MoneyTransferViewModel? ReadElement(MoneyTransferSearchModel model);

		bool Create(MoneyTransferBindingModel model);

		bool Update(MoneyTransferBindingModel model);

		bool Delete(MoneyTransferBindingModel model);
	}
}
