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
		
		//Adds the User to the Vip members in the database if the User covers the given criteria
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

			//Checks if the user is a Vip member already
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
				//Throws an exception if the user has less that 3 rents
				throw new InvalidOperationException("You must have 3 rents to become a VIP member!");
			}
		}
	}
}
