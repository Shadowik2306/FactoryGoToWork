using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.SearchModels
{
	public class MasterSearchModel
	{
		public int? Id { get; set; }

		public string? Name { get; set; }

		public string? Surname { get; set; }

		public string? Patronymic { get; set; }

		public string? Email { get; set; }

		public string? Password { get; set; }

		public string? Telephone { get; set; }
	}
}
