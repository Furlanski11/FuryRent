using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
using FuryRent.Core.Models.Vip;
using FuryRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FuryRent.Infrastructure.Data.Models;

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
				.AsNoTracking()
				.Where(u => u.RenterId == model.UserId)
				.CountAsync();

			var vipUsers = await db.VipUsers
				.AsNoTracking()
				.Select(vu => vu.UserId)
				.ToListAsync();

			if(model.UserId == null)
			{
				throw new Exception("UserId cannot be null!");
			}

			if(vipUsers.Contains(model.UserId))
			{
				throw new AlreadyVipException("You are a VIP member already!");
			}

			if(userRents >= 3)
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
				throw new InvalidOperationException("You must have 3 rents to become a VIP member!");
			}
		}
	}
}
