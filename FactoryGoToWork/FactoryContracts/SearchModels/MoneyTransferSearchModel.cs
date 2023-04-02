using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.SearchModels
{
	public class MoneyTransferSearchModel
	{
		public int? Id { get; set; }

		public int? Sum { get; set; }

		public int? AccountSenderId { get; set; }

		public int? AccountPayeeId { get; set; }

		public DateTime? DateOperation { get; set; }
	}
}
