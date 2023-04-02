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
	public interface IAccountLogic
	{
		List<AccountViewModel>? ReadList(AccountSearchModel? model);

		AccountViewModel? ReadElement(AccountSearchModel model);

		bool Create(AccountBindingModel model);

		bool Update(AccountBindingModel model);

		bool Delete(AccountBindingModel model);
	}
}
