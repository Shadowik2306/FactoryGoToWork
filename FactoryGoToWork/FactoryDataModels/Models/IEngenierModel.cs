using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	public interface IEngenierModel : IId
	{
		string Password { get; }

		string Fio { get; }

		string Email { get; }
	}
}
