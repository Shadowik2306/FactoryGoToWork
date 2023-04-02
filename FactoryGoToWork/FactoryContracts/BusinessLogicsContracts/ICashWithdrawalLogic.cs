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
	public interface ICashWithdrawalLogic
	{
		List<CashWithdrawalViewModel>? ReadList(CashWithdrawalSearchModel? model);

		CashWithdrawalViewModel? ReadElement(CashWithdrawalSearchModel model);

		bool Create(CashWithdrawalBindingModel model);

		bool Update(CashWithdrawalBindingModel model);

		bool Delete(CashWithdrawalBindingModel model);
	}
}
