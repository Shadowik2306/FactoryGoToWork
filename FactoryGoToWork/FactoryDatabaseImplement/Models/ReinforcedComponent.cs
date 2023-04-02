using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Models
{
    public class ReinforcedComponent
    {
        public int Id { get; set; }

        [Required]
        public int ReinforcedId { get; set; }

        [Required]
        public int ComponentId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Component Component { get; set; } = new();

        public virtual Reinforced Reinforced { get; set; } = new();
    }
}
