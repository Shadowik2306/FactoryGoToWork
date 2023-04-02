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
	public interface IEngenierLogic
	{
		List<EngenierViewModel>? ReadList(EngenierSearchModel? model);

        EngenierViewModel? ReadElement(EngenierSearchModel model);

		bool Create(EngenierBindingModel model);

		bool Update(EngenierBindingModel model);

		bool Delete(EngenierBindingModel model);
	}
}
