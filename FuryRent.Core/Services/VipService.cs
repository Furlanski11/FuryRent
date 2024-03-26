using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Vip;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Core.Services
{
	public class VipService : IVipService
	{
		private readonly FuryRentDbContext db;

		public VipService(FuryRentDbContext _db)
		{
			db = _db;
		}

		public async Task Become(VipUserServiceModel model)
		{
			var userRents = await db.Rents
				.Where(u => u.RenterId == model.UserId)
				.CountAsync();

			if(userRents >= 3 && model.UserId != null)
			{
				VipUser vipUser = new VipUser()
				{
					UserId = model.UserId,
				};

				await db.VipUsers.AddAsync(vipUser);
				await db.SaveChangesAsync();
			}
			else
			{
				throw new Exception("You should have 3 rents to become a VIP member!");
			}
		}
	}
}
