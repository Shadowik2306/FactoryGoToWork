using System.ComponentModel.DataAnnotations;

namespace FactoryDatabaseImplement.Models
{
	public class LatheComponent
	{
		public int Id { get; set; }

		[Required]
		public int LatheId { get; set; }

		[Required]
		public int ComponentId { get; set; }

		[Required]
		public int Count { get; set; }

		public virtual Lathe Lathe { get; set; } = new();

		public virtual Component Component { get; set; } = new();
	}
}
