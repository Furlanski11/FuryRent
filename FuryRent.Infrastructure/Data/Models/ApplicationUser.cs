using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static FuryRent.Infrastructure.Constants.DataConstants;

namespace FuryRent.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;
    }
}
