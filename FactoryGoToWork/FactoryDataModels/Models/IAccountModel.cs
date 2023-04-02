using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	//банковский счёт
	public interface IAccountModel : IId
	{
		string AccountNumber { get; }

		int CashierId { get; }

		int ClientId { get; }

		string PasswordAccount { get; }

		double Balance { get; }

		DateTime DateOpen { get; }
	}
}
