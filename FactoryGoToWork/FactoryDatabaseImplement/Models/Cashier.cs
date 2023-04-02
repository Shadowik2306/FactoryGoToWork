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
    public class Cashier : ICashierModel
    {
		public int Id { get; set; }

		[Required]
		public string Password { get; set; } = string.Empty;

		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		public string Surname { get; set; } = string.Empty;

		[Required]
		public string Patronymic { get; set; } = string.Empty;

		[Required]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Telephone { get; set; } = string.Empty;

		//для реализации связи один ко многим со Счётом
		[ForeignKey("CashierId")]
		public virtual List<Account> Accounts { get; set; } = new();

		public static Cashier Create(BankYouBancruptDatabase context, CashierBindingModel model)
		{
			return new Cashier()
			{
				Id = model.Id,
				Name = model.Name,
				Surname = model.Surname,
				Patronymic = model.Patronymic,
				Email = model.Email,
				Telephone = model.Telephone,
				Password = model.Password
			};
		}

		public void Update(CashierBindingModel model)
		{
			Id = model.Id;
			Name = model.Name;
			Surname = model.Surname;
			Patronymic = model.Patronymic;
			Email = model.Email;
			Telephone = model.Telephone;
			Password = model.Password;
		}

		public CashierViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name,
			Surname = Surname,
			Patronymic = Patronymic,
			Email = Email,
			Telephone = Telephone,
			Password = Password
		};
	}
}
