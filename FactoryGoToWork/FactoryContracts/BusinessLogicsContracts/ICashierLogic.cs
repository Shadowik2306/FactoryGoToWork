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
	public interface ICashierLogic
	{
		List<CashierViewModel>? ReadList(CashierSearchModel? model);

		CashierViewModel? ReadElement(CashierSearchModel model);

		bool Create(CashierBindingModel model);

		bool Update(CashierBindingModel model);

		bool Delete(CashierBindingModel model);
	}
}
