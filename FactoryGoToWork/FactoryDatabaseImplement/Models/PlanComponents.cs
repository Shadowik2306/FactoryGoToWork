using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Models
{
    public class PlanComponents
    {
        public int Id { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        public int ComponentId { get; set; }


        public virtual Component Component { get; set; } = new();

        public virtual Plan Plan { get; set; } = new();
    }
}
