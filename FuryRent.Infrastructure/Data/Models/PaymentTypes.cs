using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FuryRent.Infrastructure.Data.Models
{
    public class PaymentTypes
    {
        [Key]
        [Comment("PaymentType identifier")]
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; } = string.Empty;

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
