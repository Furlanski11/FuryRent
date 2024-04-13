using FuryRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Tests.Mocks
{
	public static class DatabaseMock
	{
		public static FuryRentDbContext Instance
		{
			get
			{
				var dbContextOptions = new DbContextOptionsBuilder<FuryRentDbContext>()
					.UseInMemoryDatabase("FuryRentInMemoryDb"
					+ DateTime.Now.Ticks.ToString())
					.Options;

				return new FuryRentDbContext(dbContextOptions, false);
			}
		}
	}
}
