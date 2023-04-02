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
	public interface ICashierStorage
	{
		List<CashierViewModel> GetFullList();

		List<CashierViewModel> GetFilteredList(CashierSearchModel model);

		CashierViewModel? GetElement(CashierSearchModel model);

		CashierViewModel? Insert(CashierBindingModel model);

		CashierViewModel? Update(CashierBindingModel model);

		CashierViewModel? Delete(CashierBindingModel model);
	}
}
