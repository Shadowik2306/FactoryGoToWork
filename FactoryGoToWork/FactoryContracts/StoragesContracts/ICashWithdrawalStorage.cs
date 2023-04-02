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
	public interface ICashWithdrawalStorage
	{
		List<CashWithdrawalViewModel> GetFullList();

		List<CashWithdrawalViewModel> GetFilteredList(CashWithdrawalSearchModel model);

		CashWithdrawalViewModel? GetElement(CashWithdrawalSearchModel model);

		CashWithdrawalViewModel? Insert(CashWithdrawalBindingModel model);

		CashWithdrawalViewModel? Update(CashWithdrawalBindingModel model);

		CashWithdrawalViewModel? Delete(CashWithdrawalBindingModel model);
	}
}
