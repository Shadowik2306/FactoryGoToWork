using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Models
{
    public class Plan : IPlanModel
    {
        public int Id { get; set; }
        [Required]
        public string PlanName { get; set; } = String.Empty;

        [Required]
        public DateTime date { get; set; }

        [Required]
        private Dictionary<int, ILatheModel>? _planLathes { get; set; } = null;

        
        [NotMapped]
        public Dictionary<int, ILatheModel> PlanLathes
        {
            get
            {
                if (_planLathes == null)
                {
                    _planLathes = Lathes.ToDictionary(recPL => recPL.LatheId, recPL =>
                    recPL.Lathe as ILatheModel);
                }
                return _planLathes;
            }
        }

        [ForeignKey("PlanId")]
        public virtual List<PlanLathe> Lathes { get; set; } = new();

        [Required]
        private Dictionary<int, IComponentModel> _planComponents { get; set; } = null;

        [NotMapped]
        public Dictionary<int, IComponentModel> PlanComponents
        {
            get
            {
                if (_planComponents == null)
                {
                    _planComponents = Components.ToDictionary(recPL => recPL.ComponentId, recPL =>
                    recPL.Component as IComponentModel);
                }
                return _planComponents;
            }
        }

        [ForeignKey("PlanId")]
        public virtual List<PlanComponents> Components { get; set; } = new();

        public static Plan Create(FactoryDatabase context, PlanBindingModel model)
        {
            return new Plan()
            {
                Id = model.Id,
                PlanName = model.PlanName,
            };
        }

        [ForeignKey("PlanId")]
        public virtual List<Stage> Stages { get; set; } = new();

        public void Update(PlanBindingModel model)
        {

            PlanName = model.PlanName;
        }

        public PlanViewModel GetViewModel => new()
        {
            Id = Id,
            PlanName = PlanName,
        };

        

        public void UpdateLathes(FactoryDatabase context, PlanBindingModel model)
        {
            var PlanLathes = context.PlanLathes.Where(rec => rec.PlanId == model.Id).ToList();
            if (PlanLathes != null && PlanLathes.Count > 0)
            {
                context.SaveChanges();
                foreach (var updateComponent in PlanLathes)
                {
                    model.PlanLathes.Remove(updateComponent.LatheId);
                }
                context.SaveChanges();
            }
            var Plans = context.Plans.First(x => x.Id == Id);
            foreach (var pc in model.PlanLathes)
            {
                context.PlanLathes.Add(new PlanLathe
                {
                    Plan = Plans,
                    Lathe = context.Lathes.First(x => x.Id == pc.Key)
                });
                context.SaveChanges();
            }
            _planLathes = null;
        }

        public void UpdateComponents(FactoryDatabase context, PlanBindingModel model)
        {
            var PlanComponents = context.PlanComponents.Where(rec => rec.PlanId == model.Id).ToList();
            if (PlanComponents != null && PlanComponents.Count > 0)
            {
                context.SaveChanges();
                PlanComponents = context.PlanComponents.Where(rec => rec.PlanId == model.Id).ToList();
                foreach (var updateComponent in PlanComponents)
                {
                    model.PlanComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            var Plans = context.Plans.First(x => x.Id == Id);
            foreach (var pc in model.PlanComponents)
            {
                context.PlanComponents.Add(new PlanComponents
                {
                    Plan = Plans,
                    Component = context.Components.First(x => x.Id == pc.Key),
                });
                context.SaveChanges();
            }
            _planComponents = null;
        }
    }
}
