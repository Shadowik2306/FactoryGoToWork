using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
	//клиент
	public interface IClientModel : IId
	{
		string Password { get; }

		string Name { get; }

		string Surname { get; }

		string Patronymic { get; }

		string Email { get; }

		string Telephone { get; }
	}
}
