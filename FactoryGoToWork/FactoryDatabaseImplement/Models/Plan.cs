using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		[ForeignKey("PlanId")]
		public virtual List<Stage> Stages { get; set; } = new();

		private Dictionary<int, (IReinforcedModel, int)>? _planReinforcedes = null;
		[NotMapped]
		public Dictionary<int, (IReinforcedModel, int)> PlanReinforcedes
		{
			get
			{
				if (_planReinforcedes == null)
				{
					_planReinforcedes = Reinforceds.ToDictionary(recCS => recCS.ReinforceId, recCS => (recCS.Reinforced as IReinforcedModel, recCS.Count));
				}
				return _planReinforcedes;
			}
		}
		[ForeignKey("PlanId")]
		public virtual List<PlanReinforced> Reinforceds { get; set; } = new();

		public static Plan Create(FactoryDatabase context, PlanBindingModel model)
		{
			return new Plan()
			{
				Id = model.Id,
				StartDate = model.StartDate,
				EndDate = model.EndDate,
				Reinforceds = model.PlanReinforcedes.Select(x => new PlanReinforced
				{
					Count = x.Value.Item2,
					Reinforced = context.Reinforceds.First(y => y.Id == x.Key)
				}).ToList()
			};
		}
		public void Update(PlanBindingModel model)
		{
			PlanName=model.PlanName;
		}
		public PlanViewModel GetViewModel => new()
		{
			Id = Id,
			StartDate = StartDate,
			EndDate= EndDate,
			PlanReinforcedes=PlanReinforcedes
		};
		public void UpdateReinforceds(FactoryDatabase context, PlanBindingModel model)
		{
			var PlanReinforceds = context.PlanReinforceds.Where(recCS => recCS.PlanId == model.Id).ToList();
			if (PlanReinforceds != null && PlanReinforceds.Count > 0)
			{
				context.PlanReinforceds.RemoveRange(PlanReinforceds.Where(rec => !model.PlanReinforcedes.ContainsKey(rec.PlanId)));
				context.SaveChanges();
				PlanReinforceds = context.PlanReinforceds.Where(rec => rec.PlanId == model.Id).ToList();
				foreach (var updateComponent in PlanReinforceds)
				{
					updateComponent.Count = model.PlanReinforcedes[updateComponent.PlanId].Item2;
					model.PlanReinforcedes.Remove(updateComponent.PlanId);
				}
				context.SaveChanges();
			}
			var Plan = context.Plans.First(x => x.Id == Id);
			foreach (var pc in model.PlanReinforcedes)
			{
				context.PlanReinforceds.Add(new PlanReinforced
				{
					Plan = Plan,
					Reinforced = context.Reinforceds.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_planReinforcedes = null;
		}
	}
}
