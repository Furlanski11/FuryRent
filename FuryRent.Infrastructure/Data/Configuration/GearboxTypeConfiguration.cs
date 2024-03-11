using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuryRent.Infrastructure.Data.Configuration
{
	internal class GearboxTypeConfiguration : IEntityTypeConfiguration<GearboxType>
	{
		public void Configure(EntityTypeBuilder<GearboxType> builder)
		{
			builder.HasData(CreateGearboxTypes());
		}

		private List<GearboxType> CreateGearboxTypes()
		{
			List<GearboxType> gearboxTypes = new List<GearboxType>()
			{
				new GearboxType()
				{
					Id = 1,
					Name = "Automatic"
				},
				new GearboxType()
				{
					Id = 2,
					Name = "Manual"
				},
			};
			return gearboxTypes;
		}
	}
}
