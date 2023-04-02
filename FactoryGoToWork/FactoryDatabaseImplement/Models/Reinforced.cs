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
        public double Price { get; set; }

        [Required]
        public int EngenierId { get; set; }

        private Dictionary<int, (IComponentModel, int)>? _reinforcedComponents = null;

        [NotMapped]
        public Dictionary<int, (IComponentModel, int)> ReinforcedComponents
        {
            get
            {
                if (_reinforcedComponents == null)
                {
                    _reinforcedComponents = Components
                            .ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component as IComponentModel, recPC.Count));
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
                Components = model.ReinforcedComponents.Select(x => new ReinforcedComponent
                {
                    Component = context.Components.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList()
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
            ReinforcedComponents = ReinforcedComponents
        };

        public void UpdateComponents(FactoryDatabase context, ReinforcedBindingModel model)
        {
            var ReinforcedComponents = context.ReinforcedComponents.Where(rec => rec.ReinforcedId == model.Id).ToList();
            if (ReinforcedComponents != null && ReinforcedComponents.Count > 0)
            {  
                context.ReinforcedComponents.RemoveRange(ReinforcedComponents.Where(rec => !model.ReinforcedComponents.ContainsKey(rec.ComponentId)));
                context.SaveChanges();
                ReinforcedComponents = context.ReinforcedComponents.Where(rec => rec.ReinforcedId == model.Id).ToList();
                foreach (var updateComponent in ReinforcedComponents)
                {
                    updateComponent.Count = model.ReinforcedComponents[updateComponent.ComponentId].Item2;
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
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            _reinforcedComponents = null;
        }
    }
}
