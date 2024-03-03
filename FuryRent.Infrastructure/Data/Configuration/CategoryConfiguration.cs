using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuryRent.Infrastructure.Data.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<Category> CreateCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "German"
                },
                new Category()
                {
                    Id = 2,
                    Name = "American"
                },
                new Category()
                {
                    Id = 3,
                    Name = "JDM"
                },
                new Category()
                {
                    Id = 4,
                    Name = "British"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Italian"
                }
            };
            return categories;
        }
    }
}
