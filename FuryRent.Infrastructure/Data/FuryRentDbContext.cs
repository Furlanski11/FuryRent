using FuryRent.Infrastructure.Data.Configuration;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Infrastructure.Data
{
    public class FuryRentDbContext : IdentityDbContext<ApplicationUser>
    {
        public FuryRentDbContext(DbContextOptions<FuryRentDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new PaymentTypesConfiguration());
            builder.ApplyConfiguration(new EngineTypeConfiguration());
            builder.ApplyConfiguration(new GearboxTypeConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Rent> Rents { get; set; } = null!;

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;

        public DbSet<PaymentTypes> PaymentTypes { get; set; } = null!;

        public DbSet<EngineType> EngineTypes { get; set; } = null!;
        
        public DbSet<GearboxType> GearboxTypes { get; set; } = null!;

        public DbSet<VipUser> VipUsers { get; set; } = null!;

    }
}
