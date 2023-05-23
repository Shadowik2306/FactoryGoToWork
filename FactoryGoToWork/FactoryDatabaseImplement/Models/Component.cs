using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FactoryDataModels.Models;

namespace FactoryDatabaseImplement.Models
{
	public class Component : IComponentModel
	{
		public int Id { get; set; }
		[Required]
		public string ComponentName { get; set; } = string.Empty;
		[Required]
		public double Cost { get; set; }
		[Required]
		public int EngenierId { get; set; }
		
		private Dictionary<int, (IPlanModel, int)> _componentPlans = null;
		[NotMapped]
		public Dictionary<int, (IPlanModel, int)> ComponentPlans
		{
			get
			{
				if (_componentPlans == null)
				{
					_componentPlans = Plans
						.ToDictionary(recCP => recCP.PlanId, recCM => (recCM.Plan as IPlanModel, recCM.Count));
				}
				return _componentPlans;
			}
		}
		[ForeignKey("ComponentId")]
		public virtual List<ComponentPlans> Plans { get; set; } = new();
		public static Component Create(FactoryDatabase context, ComponentBindingModel model)
		{
			return new Component()
			{
				Id = model.Id,
				EngenierId = model.EngenierId,
				Plans = model.ComponentPlans.Select(x => new ComponentPlans
				{
					Plan = context.Plans.First(y => y.Id == x.Key),
					Count = x.Value.Item2
				}).ToList(),
				Cost = model.Cost,
				ComponentName = model.ComponentName
			};
		}

		public void Update(ComponentBindingModel model)
		{
			Cost = model.Cost;
		}

		public ComponentViewModel GetViewModel => new()
		{
			Id = Id,
			EngenierId = EngenierId,
			Cost = Cost,
			ComponentName = ComponentName,
			ComponentPlans = ComponentPlans
		};
		public void UpdateComponents(FactoryDatabase context, ComponentBindingModel model)
		{
			var ComponentPlans = context.ComponentPlans.Where(rec => rec.ComponentId == model.Id).ToList();
			if (ComponentPlans != null && ComponentPlans.Count > 0)
			{
				context.ComponentPlans.RemoveRange(ComponentPlans.Where(rec => !model.ComponentPlans.ContainsKey(rec.PlanId)));
				context.SaveChanges();
				ComponentPlans = context.ComponentPlans.Where(rec => rec.ComponentId == model.Id).ToList();
				foreach (var updateComponent in ComponentPlans)
				{
					updateComponent.Count = model.ComponentPlans[updateComponent.PlanId].Item2;
					model.ComponentPlans.Remove(updateComponent.PlanId);
				}
				context.SaveChanges();
			}
			var Component = context.Components.First(x => x.Id == Id);
			foreach (var pc in model.ComponentPlans)
			{
				context.ComponentPlans.Add(new ComponentPlans
				{
					Component = Component,
					Plan = context.Plans.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_componentPlans = null;
		}
	}
}
