using System.ComponentModel.DataAnnotations;

namespace FactoryDatabaseImplement.Models
{
    public class ComponentPlans
    {
        public int Id { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        public int ComponentId { get; set; }
		[Required]
		public int Count { get; set; }

		public virtual Component Component { get; set; } = new();

        public virtual Plan Plan { get; set; } = new();
    }
}
