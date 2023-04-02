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
	public interface IMasterLogic
	{
		List<MasterViewModel>? ReadList(MasterSearchModel? model);

		MasterViewModel? ReadElement(MasterSearchModel model);

		bool Create(MasterBindingModel model);

		bool Update(MasterBindingModel model);

		bool Delete(MasterBindingModel model);
	}
}
