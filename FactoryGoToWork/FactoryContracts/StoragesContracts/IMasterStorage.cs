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
	public interface IMasterStorage
	{
		List<MasterViewModel> GetFullList();

		List<MasterViewModel> GetFilteredList(MasterSearchModel model);

		MasterViewModel? GetElement(MasterSearchModel model);

		MasterViewModel? Insert(MasterBindingModel model);

		MasterViewModel? Update(MasterBindingModel model);

		MasterViewModel? Delete(MasterBindingModel model);
	}
}
