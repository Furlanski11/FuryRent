using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FuryRent.Infrastructure.Constants.DataConstants;

namespace FuryRent.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(CarMakeMaxLength)]
        public string Make { get; set; } = string.Empty;

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [MaxLength(CarColorMaxLength)]
        public string Color { get; set; } = string.Empty;

        [Required]
        public int Kilometers { get; set; }

        [Required]
        [Range(1, 4)]
        public int EngineType { get; set; }

        [Required]
        public int Horsepower { get; set; }

        [Required]
        [Range(1,2)]
        public int GearboxType { get; set; }

        [Required]
        public DateTime YearOfProduction { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal PricePerDay { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public bool IsAvailable { get; set; } = true;

        public bool IsVipOnly { get; set; } = false;
    }
}
