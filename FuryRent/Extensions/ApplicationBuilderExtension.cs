using FuryRent.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtension
    {
        //Creates Admin Role
        public static async Task CreateAdminRoleAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(userManager != null && roleManager != null && await roleManager.RoleExistsAsync("Admin") == false)
            {
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync("antoan_f@abv.bg");

                if(admin != null)
                {
                    await userManager.AddToRoleAsync(admin, role.Name);
                }
            }
        }
        //Creates User role
		public static async Task CreateUserRoleAsync(this IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync("User") == false)
			{
				var role = new IdentityRole("User");
				await roleManager.CreateAsync(role);
			}
		}
	}
}
