using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	//банковская карта
	public interface ICardModel : IId
	{
		int ClientID { get; }

		int AccountId { get; }
		
		string Number { get; }

		string CVC { get; }

		DateTime Period { get; }
	}
}
