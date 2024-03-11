using System.ComponentModel.DataAnnotations;
using static FuryRent.Infrastructure.Constants.DataConstants;

namespace FuryRent.Infrastructure.Data.Models

{
	public class GearboxType
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(GearboxTypeNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		public List<Car> Cars { get; set; } = new List<Car>();
	}
}
