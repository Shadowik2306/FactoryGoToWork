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

namespace FactoryDatabaseImplement.Models
{
    public class Reinforced : IReinforcedModel
    {
        public int Id { get; set; }

        [Required]
        public string ReinforcedName { get; set; } = string.Empty;

        [Required]
        public int EngenierId { get; set; }

        private Dictionary<int, IComponentModel>? _reinforcedComponents = null;

        [NotMapped]
        public Dictionary<int, IComponentModel> ReinforcedComponents
        {
            get
            {
                if (_reinforcedComponents == null)
                {
                    _reinforcedComponents = Components
                            .ToDictionary(recPC => recPC.ComponentId, recPC => recPC.Component as IComponentModel);
                }
                return _reinforcedComponents;
            }
        }

        [ForeignKey("ReinforcedId")]
        public virtual List<ReinforcedComponent> Components { get; set; } = new();


        public static Reinforced Create(FactoryDatabase context, ReinforcedBindingModel model)
        {
            return new Reinforced()
            {
                Id = model.Id,
                ReinforcedName = model.ReinforcedName,
                EngenierId = model.EngenierId,
            };
        }

        public void Update(ReinforcedBindingModel model)
        {
            ReinforcedName = model.ReinforcedName;
        }

        public ReinforcedViewModel GetViewModel => new()
        {
            Id = Id,
            ReinforcedName = ReinforcedName,
        };

        public void UpdateComponents(FactoryDatabase context, ReinforcedBindingModel model)
        {
            var ReinforcedComponents = context.ReinforcedComponents.Where(rec => rec.ReinforcedId == model.Id).ToList();
            if (ReinforcedComponents != null && ReinforcedComponents.Count > 0)
            {  
                context.SaveChanges();
                foreach (var updateComponent in ReinforcedComponents)
                {
                    model.ReinforcedComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            var Reinforced = context.Reinforceds.First(x => x.Id == Id);
            foreach (var pc in model.ReinforcedComponents)
            {
                context.ReinforcedComponents.Add(new ReinforcedComponent
                {
                    Reinforced = Reinforced,
                    Component = context.Components.First(x => x.Id == pc.Key),
                });
                context.SaveChanges();
            }
            _reinforcedComponents = null;
        }
    }
}
