using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Infrastructure.Data
{
    public class FuryRentDbContext : IdentityDbContext
    {
        public FuryRentDbContext(DbContextOptions<FuryRentDbContext> options)
            : base(options)
        {
        }
    }
}
