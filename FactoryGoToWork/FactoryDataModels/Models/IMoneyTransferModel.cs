using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	//перевод денег
	public interface IMoneyTransferModel : IId
	{
		int Sum { get; }

		int AccountSenderId { get; }

		int AccountPayeeId { get; }

		DateTime DateOperation { get; }
	}
}
