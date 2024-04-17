using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static FuryRent.Infrastructure.Constants.DataConstants;

namespace FuryRent.Infrastructure.Data.Models

{
	public class GearboxType
	{
		[Key]
		[Comment("GearboxType identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(GearboxTypeNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		public List<Car> Cars { get; set; } = new List<Car>();
	}
}
