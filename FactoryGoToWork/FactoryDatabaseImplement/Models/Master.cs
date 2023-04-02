using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Models
{
    public class Master : IMasterModel
    {
		public int Id { get; set; }

		[Required]
		public string Password { get; set; } = string.Empty;

		[Required]
		public string Fio { get; set; } = string.Empty;

		[Required]
		public string Email { get; set; } = string.Empty;


		public static Master Create(BankYouBancruptDatabase context, MasterBindingModel model)
		{
			return new Master()
			{
				Id = model.Id,
				Fio = model.Fio,
				Email = model.Email,
				Password = model.Password
			};
		}

		public void Update(MasterBindingModel model)
		{
			Id = model.Id;
			Fio = model.Fio;
			Email = model.Email;
			Password = model.Password;
		}

		public MasterViewModel GetViewModel => new()
		{
			Id = Id,
			Fio = Fio,
			Email = Email,
			Password = Password
		};
	}
}
