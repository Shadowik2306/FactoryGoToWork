using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	//пополнение карты
	public interface ICreditingModel : IId
	{
		int CardId { get; }

		int Sum { get; }

		DateTime Date { get; }
	}
}
