using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.SearchModels
{
	public class CashWithdrawalSearchModel
	{
		public int? Id { get; set; }

		public int? AccountId { get; set; }

		public int? Sum { get; set; }

		public DateTime? DateOperation { get; set; }
	}
}
