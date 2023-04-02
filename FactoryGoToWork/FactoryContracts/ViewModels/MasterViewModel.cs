using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
	public class MasterViewModel : IMasterModel
	{
		public int Id { get; set; }

		public string Password { get; set; } = string.Empty;

		[DisplayName("Имя")]
		public string Fio { get; set; } = string.Empty;

		[DisplayName("Почта")]
		public string Email { get; set; } = string.Empty;
	}
}
