﻿using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
	public class CashierBindingModel : ICashierModel
	{
		public int Id { get; set; }

		public string Password { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public string Surname { get; set; } = string.Empty;

		public string Patronymic { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Telephone { get; set; } = string.Empty;
	}
}
