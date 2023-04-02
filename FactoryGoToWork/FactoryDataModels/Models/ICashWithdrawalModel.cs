using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	//выдача наличных
	public interface ICashWithdrawalModel : IId
	{
		int AccountId { get; }

		int Sum { get; }

		DateTime DateOperation { get; }
	}
}
