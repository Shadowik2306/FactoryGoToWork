using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Models
{
    public class LatheReinforced
    {
        public int Id { get; set; }
        [Required]
        public int LatheId { get; set; }
        [Required]
        public int ReinforcedId { get; set; }

        public virtual Lathe Lathe { get; set; } = new();

        public virtual Reinforced Reinforced { get; set; } = new();

    }
}
