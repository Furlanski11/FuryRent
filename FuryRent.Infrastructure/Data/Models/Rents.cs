using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuryRent.Infrastructure.Data.Models
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        [Required]
        public string RenterId { get; set; } = string.Empty;

        [ForeignKey(nameof(RenterId))]
        public IdentityUser? Renter { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        [Required]
        public DateTime RentalStartDate { get; set; }

        [Required]
        public DateTime RentalEndDate { get; set; }

        [Required]
        public double TotalCost { get; set; }

    }
}
