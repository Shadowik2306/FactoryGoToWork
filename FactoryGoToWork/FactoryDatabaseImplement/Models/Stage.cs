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

        [Required]
        public int ReinforsedId { get; set; }

        public static Stage Create(FactoryDatabase context, StageBindingModel model)
        {
            return new Stage()
            {
                Id = model.Id,
                ReinforsedId = model.ReinforsedId,
                PlanId = model.PlanId
            };
        }

        public void Update(StageBindingModel model)
        {
            ReinforsedId = model.ReinforsedId;
            PlanId = model.PlanId;
        }
    

        public StageViewModel GetViewModel => new()
        {
            Id = Id,
            ReinforsedId = ReinforsedId,
            PlanId = PlanId
        };
    }
}
