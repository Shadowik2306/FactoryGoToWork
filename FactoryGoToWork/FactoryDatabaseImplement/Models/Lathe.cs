using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Models
{
    public class Lathe : ILatheModel
    {
		public int Id { get; set; }
		[Required]
		public string LatheName { get; set; } = String.Empty;
        [Required]
        public int MasterId { get; set; }
        [Required]
        public int BusyId { get; set; }

		private Dictionary<int, (IReinforcedModel, int)>?_latheReinforcedes { get; set; } = null;
		[NotMapped]
		public Dictionary<int, (IReinforcedModel, int)> LatheReinforcedes
		{
			get
			{
				if (_latheReinforcedes == null)
				{
					_latheReinforcedes = Reinforcedes.
						ToDictionary(recLR => recLR.ReinforcedId, recLR => (recLR.Reinforced as IReinforcedModel, recLR.Count));
				}
				return _latheReinforcedes;
			}
		}
		[ForeignKey("LatheId")]
		public virtual List<LatheReinforced> Reinforcedes { get; set; } = new();

		private Dictionary<int, (IComponentModel, int)>? _latheComponents { get; set; } = null;
		[NotMapped]
		public Dictionary<int, (IComponentModel, int)> LatheComponents
		{
			get
			{
				if (_latheComponents == null)
				{
					_latheComponents = Components.ToDictionary(recWM => recWM.ComponentId, recWM =>
					(recWM.Component as IComponentModel, recWM.Count));
				}
				return _latheComponents;
			}
		}
		[ForeignKey("LatheId")]
		public virtual List<LatheComponent> Components { get; set; } = new();

		public static Lathe Create(FactoryDatabase context, LatheBindingModel model)
		{
			return new Lathe()
			{
                Id = model.Id,
                MasterId = model.MasterId,
				BusyId = model.BusyId,
				Reinforcedes = model.LatheReinforcedes.Select(x => new LatheReinforced
				{
					Reinforced = context.Reinforceds.First(y => y.Id == x.Key),
					Count = x.Value.Item2
				}).ToList(),
				Components = model.LatheComponents.Select(x => new LatheComponent
				{
					Component = context.Components.First(y => y.Id == x.Key),
					Count = x.Value.Item2
				}).ToList(),
				LatheName = model.LatheName,
			};
		}

		public void Update(LatheBindingModel model)
		{
			LatheName = model.LatheName;
		}

		public LatheViewModel GetViewModel => new()
		{
			Id = Id,
			LatheName = LatheName,
			MasterId = MasterId,
			BusyId = BusyId,
			LatheComponents = LatheComponents,
			LatheReinforcedes = LatheReinforcedes
		};



		public void UpdateReinforcedes(FactoryDatabase context, LatheBindingModel model)
		{
			var LatheReinforcedes = context.LatheReinforcedes.Where(rec => rec.LatheId == model.Id).ToList();
			if (LatheReinforcedes != null && LatheReinforcedes.Count > 0)
			{
				context.LatheReinforcedes.RemoveRange(LatheReinforcedes.Where(rec => !model.LatheReinforcedes.ContainsKey(rec.ReinforcedId)));
				context.SaveChanges();
				foreach (var updateComponent in LatheReinforcedes)
				{
					updateComponent.Count = model.LatheReinforcedes[updateComponent.ReinforcedId].Item2;
					model.LatheReinforcedes.Remove(updateComponent.ReinforcedId);
				}
				context.SaveChanges();
			}
			var Lathe = context.Lathes.First(x => x.Id == Id);
			foreach (var pc in model.LatheReinforcedes)
			{
				context.LatheReinforcedes.Add(new LatheReinforced
				{
					Lathe = Lathe,
					Reinforced = context.Reinforceds.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_latheReinforcedes = null;
		}

		public void UpdateComponents(FactoryDatabase context, LatheBindingModel model)
		{
			var LatheComponent = context.LatheComponents.Where(rec => rec.LatheId == model.Id).ToList();
			if (LatheComponent != null && LatheComponent.Count > 0)
			{
				context.LatheComponents.RemoveRange(LatheComponent.Where(rec => !model.LatheComponents.ContainsKey(rec.ComponentId)));
				context.SaveChanges();
				foreach (var updateComponent in LatheComponent)
				{
					updateComponent.Count = model.LatheComponents[updateComponent.ComponentId].Item2;
					model.LatheComponents.Remove(updateComponent.ComponentId);
				}
				context.SaveChanges();
			}
			var Lathe = context.Lathes.First(x => x.Id == Id);
			foreach (var pc in model.LatheComponents)
			{
				context.LatheComponents.Add(new LatheComponent
				{
					Lathe = Lathe,
					Component = context.Components.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_latheComponents = null;
		}
	}
}
