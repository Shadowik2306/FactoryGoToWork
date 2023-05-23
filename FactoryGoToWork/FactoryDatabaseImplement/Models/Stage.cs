using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Models
{
    public class Stage : IStageModel
    {
        public int Id { get; set; }

        [Required]
        public int PlanId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public static Stage Create(StageBindingModel model)
		{
			return new Stage()
			{
				Id = model.Id,
				PlanId = model.PlanId,
				Name = model.Name,
				StartDate = model.StartDate,
				EndDate = model.EndDate,
			};
		}
		public void Update(StageBindingModel model)
		{
			Name = model.Name;
		}
		public StageViewModel GetViewModel => new()
		{
			Id = Id,
			PlanId = PlanId,
			Name = Name,
			StartDate = StartDate,
			EndDate = EndDate,
		};
	}
}
