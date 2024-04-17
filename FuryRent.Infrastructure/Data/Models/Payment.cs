using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuryRent.Infrastructure.Data.Models
{
    public class Payment
    {
        [Key]
        [Comment("Payment identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Rent identifier")]
        public int RentId { get; set; }

        [ForeignKey(nameof(RentId))]
        public Rent Rents { get; set; } = null!;

        [Required]
        public double PaymentAmount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Comment("PaymentType identifier")]
        public int PaymentTypeId { get; set; }

        [ForeignKey(nameof(PaymentTypeId))]
        public PaymentTypes PaymentType { get; set; } = null!;
    }
}
