using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FuryRent.Infrastructure.Constants.DataConstants;

namespace FuryRent.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        [Comment("Car identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(CarMakeMaxLength, MinimumLength = CarMakeMinLength)]
        public string Make { get; set; } = string.Empty;

        [Required]
        [StringLength(CarModelMaxLength, MinimumLength = CarModeleMinLength)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(CarColorMaxLength, MinimumLength = CarColorMinLength)]
        public string Color { get; set; } = string.Empty;

        [Required]
        public int Kilometers { get; set; }

        [Required]
        [Comment("EngineType identifier")]
        public int EngineTypeId { get; set; }

        [ForeignKey(nameof(EngineTypeId))]
        public EngineType EngineType { get; set; } = null!;

        [Required]
        public int Horsepower { get; set; }

        [Required]
        [Comment("GearboxType identifier")]
        public int GearboxTypeId { get; set; }

        [ForeignKey(nameof(GearboxTypeId))]
        public GearboxType GearboxType { get; set; } = null!;

        [Required]
        public DateTime YearOfProduction { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal PricePerDay { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public bool IsAvailable { get; set; } = true;

        public bool IsVipOnly { get; set; } = false;
    }
}
