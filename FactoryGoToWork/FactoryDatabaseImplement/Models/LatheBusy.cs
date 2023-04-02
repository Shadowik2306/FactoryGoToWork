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
    public class LatheBusy : ILatheBusyModel
    {
		public int Id { get; set; }
		[Required]
		public int Percent { get; set; }
		
        [ForeignKey("BusyId")]
        public virtual List<Lathe> Lathes { get; set; } = new();

        public static LatheBusy Create(FactoryDatabase context, LatheBusyBindingModel model)
		{
			return new LatheBusy()
			{
				Id = model.Id,
				Percent = model.Percent,
			};
		}

		public void Update(LatheBusyBindingModel model)
		{
			Id = model.Id;
			Id = model.Id;
            Percent = model.Percent;
		}

		public LatheBusyViewModel GetViewModel => new()
		{
			Id = Id,
            Percent = Percent,
		};

		
	}
}
