
using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryDataModels.Models;

namespace FactoryDatabaseImplement.Models
{
    public class Component : IComponentModel
    {
        public int Id { get; private set; }

        [Required]
        public string ComponentName { get; private set; } = string.Empty;

        [Required]
        public double Cost { get; set; }


        [ForeignKey("ComponentId")]
        public virtual List<ReinforcedComponent> ReinforcedComponents { get; set; } = new();

        [ForeignKey("ComponentId")]
        public virtual List<Stage> Stages { get; set; } = new();


        public static Component Create(ComponentBindingModel model)
        {
            return new Component
            {
                Id = model.Id,
                ComponentName = model.ComponentName,
                Cost = model.Cost,
            };
        }

        public void Update(ComponentBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            ComponentName = model.ComponentName;
        }

        public ComponentViewModel GetViewModel => new()
        {
            Id = Id,
            ComponentName = ComponentName,
        };

        
    }
}
