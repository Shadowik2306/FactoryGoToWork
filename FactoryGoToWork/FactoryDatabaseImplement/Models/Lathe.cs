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
        [Required]
		private Dictionary<int, IReinforcedModel>? _latheReinforcedes = null;

		[NotMapped]
        public Dictionary<int, IReinforcedModel> LatheReinforcedes {
			get {
                if (_latheReinforcedes == null)
                {
                    _latheReinforcedes = Reinforceds.ToDictionary(recLR => recLR.ReinforcedId, recLR =>
                    recLR.Reinforced as IReinforcedModel);
                }
                return _latheReinforcedes;
            }
		}
        [ForeignKey("LatheId")]
        public virtual List<LatheReinforced> Reinforceds { get; set; } = new();

        public static Lathe Create(FactoryDatabase context, LatheBindingModel model)
		{
			return new Lathe()
			{
				Id = model.Id,
				LatheName = model.LatheName,
                MasterId = model.MasterId,
                BusyId = model.BusyId
			};
		}

		public void Update(LatheBindingModel model)
		{
			Id = model.Id;
			LatheName = model.LatheName;
		}

		public LatheViewModel GetViewModel => new()
		{
			Id = Id,
            BusyId = BusyId,
            MasterId = MasterId,
			LatheName = LatheName,
        };

        public void UpdateComponents(FactoryDatabase context, LatheBindingModel model)
        {
            var LatheReinforsed = context.LatheReinforcedes.Where(rec => rec.LatheId == model.Id).ToList();
            if (LatheReinforsed != null && LatheReinforsed.Count > 0)
            {
                context.LatheReinforcedes.RemoveRange(LatheReinforsed.Where(rec => !model.LatheReinforcedes.ContainsKey(rec.LatheId)));
                context.SaveChanges();
                LatheReinforsed = context.LatheReinforcedes.Where(rec => rec.LatheId == model.Id).ToList();
                foreach (var updateComponent in LatheReinforsed)
                {
                    model.LatheReinforcedes.Remove(updateComponent.LatheId);
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
                });
                context.SaveChanges();
            }
            _latheReinforcedes = null;
        }
    }
}
