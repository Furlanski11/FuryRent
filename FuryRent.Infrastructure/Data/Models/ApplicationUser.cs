using Microsoft.AspNetCore.Identity;

namespace FuryRent.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsVip { get; set; } = false;

        public List<Rent> Rents { get; set; } = new List<Rent>();

        public bool IsActive { get; set; }

    }
}
