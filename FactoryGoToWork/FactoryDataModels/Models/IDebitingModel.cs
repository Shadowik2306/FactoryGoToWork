using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    //снятие денег с карты
	public interface IDebitingModel : IId
	{
        int CardId { get; }

        int Sum { get; }

        DateTime Date { get; }
    }
}
