using FactoryDataModels.Models;
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace FactoryDatabaseImplement.Models
{
    public class Reinforced : IReinforcedModel
    {
        public int Id { get; set; }

        [Required]
        public string ReinforcedName { get; set; } = string.Empty;

		public static Reinforced Create(ReinforcedBindingModel model)
		{
			return new Reinforced
			{
				Id = model.Id,
				ReinforcedName = model.ReinforcedName,
			};
		}

		public void Update(ReinforcedBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			ReinforcedName = model.ReinforcedName;
		}

		public ReinforcedViewModel GetViewModel => new()
		{
			Id = Id,
			ReinforcedName = ReinforcedName,
		};
	}
}
