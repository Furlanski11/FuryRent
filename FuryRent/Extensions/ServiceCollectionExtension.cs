using FuryRent.Core.Contracts;
using FuryRent.Core.Services;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
			services.AddScoped<IVipService, VipService>();
			services.AddScoped<IRentService, RentService>();
            services.AddScoped<IPayService, PayService>();

			return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<FuryRentDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 7;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<FuryRentDbContext>();

            return services;
        }
    }
}
