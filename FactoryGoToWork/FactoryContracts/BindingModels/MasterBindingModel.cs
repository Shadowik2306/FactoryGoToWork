using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
	public class MasterBindingModel : IMasterModel
	{
		public int Id { get; set; }

		public string Password { get; set; } = string.Empty;

		public string Fio { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;
	}
}
