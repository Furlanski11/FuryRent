using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuryRent.Infrastructure.Data.Models
{
    public class Rent
    {
        [Key]
        [Comment("Rent identifier")]
        public int RentId { get; set; }

        [Required]
        [Comment("ApplicationUser identifier")]
        public string RenterId { get; set; } = string.Empty;

        [ForeignKey(nameof(RenterId))]
        public ApplicationUser? Renter { get; set; }

        [Required]
        [Comment("Car identifier")]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        [Required]
        [Comment("Pickup date")]
        public DateTime RentalStartDate { get; set; }

        [Required]
        [Comment("Return date")]
        public DateTime RentalEndDate { get; set; }

        [Required]
        public double TotalCost { get; set; }

    }
}
