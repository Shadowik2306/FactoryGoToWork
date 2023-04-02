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
	public interface IAccountStorage
	{
		List<AccountViewModel> GetFullList();

		List<AccountViewModel> GetFilteredList(AccountSearchModel model);

		AccountViewModel? GetElement(AccountSearchModel model);

		AccountViewModel? Insert(AccountBindingModel model);

		AccountViewModel? Update(AccountBindingModel model);

		AccountViewModel? Delete(AccountBindingModel model);
	}
}
