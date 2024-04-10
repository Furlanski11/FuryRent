using FuryRent.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuryRent.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "userOne@mail.com",
                NormalizedUserName = "USERONE@MAIL.COM",
                Email = "userOne@mail.com",
                NormalizedEmail = "USERONE@MAIL.COM",
                FirstName = "Bruce",
                LastName = "Wayne",
                PhoneNumber = "0878658932"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "userOne123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "GUEST@MAIL.COM",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                FirstName = "Georgi",
                LastName = "Peev",
                PhoneNumber = "0879487568"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "guest123");

            users.Add(user);

            return users;
        }
    }
}