using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuryRent.Infrastructure.Data.Models
{
    public class VipUser
    {
        [Key]
        [Comment("VipUser identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("ApplicationUser identifier")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;
    }
}
