using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuryRent.Infrastructure.Data.Configuration
{
	internal class EngineTypeConfiguration : IEntityTypeConfiguration<EngineType>
	{
		public void Configure(EntityTypeBuilder<EngineType> builder)
		{
			builder.HasData(CreateEngineTypes());
		}

		private List<EngineType> CreateEngineTypes()
		{
			List<EngineType> engineTypes = new List<EngineType>()
			{
				new EngineType()
				{
					Id = 1,
					Name = "Petrol"
				},
				new EngineType()
				{
					Id = 2,
					Name = "Diesel"
				},
				new EngineType()
				{
					Id= 3,
					Name = "PlugInHybrid"
				},
				new EngineType()
				{
					Id = 4,
					Name = "Electric"
				}
			};
			return engineTypes;
		}
	}
}
