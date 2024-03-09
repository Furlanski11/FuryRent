using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FuryRent.Infrastructure.Constants.DataConstants;
using static FuryRent.Core.CarConstants;

namespace FuryRent.Core.Models.Car
{
	public class EditCarFormModel
	{
        public int Id { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		[MaxLength(ImageUrlMaxLength)]
		public string ImageUrl { get; set; } = null!;

		[Required(ErrorMessage = RequireErrorMessage)]
		[MaxLength(CarMakeMaxLength)]
		public string Make { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[MaxLength(CarModelMaxLength)]
		public string Model { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[MaxLength(CarColorMaxLength)]
		public string Color { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		public int Kilometers { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		[Range(1, 4)]
		public int EngineType { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		[MinLength(100)]
		public int Horsepower { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		[Range(1, 2)]
		public int GearboxType { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		[DisplayFormat(DataFormatString = CarConstants.DateFormat)]
		public string YearOfroduction { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[Column(TypeName = "money")]
		[Precision(18, 2)]
		public decimal PricePerDay { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		public int CategoryId { get; set; }

		public bool IsVipOnly { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}
