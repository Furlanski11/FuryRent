using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Rent;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Core.Services
{
	public class RentService : IRentService
	{
		private readonly FuryRentDbContext db;

		public RentService(FuryRentDbContext _db)
		{
			db = _db;
		}

		public async Task<IEnumerable<RentViewModel>> All(string userId)
		{
			return await db.Rents
				.Where(r => r.RenterId == userId)
				.Select(r => new RentViewModel()
				{
					Id = r.RentId,
					CarId = r.CarId,
					ImageUrl = r.Car.ImageUrl,
					Make = r.Car.Make,
					Model = r.Car.Model,
					RentalStartDate = r.RentalStartDate,
					RentalEndDate = r.RentalEndDate,
					PricePerDay = $"{r.Car.PricePerDay:f0}",
					TotalCost = r.TotalCost.ToString()
				}).ToListAsync();

		}

		public async Task Add(AddRentViewModel rentModel,string userId, int carId)
		{
			var carPrice = await db.Cars
				.AsNoTracking()
				.Where(c => c.Id == carId)
				.Select(c => c.PricePerDay)
				.FirstOrDefaultAsync();

			var rents = await db.Rents
				.AsNoTracking()
				.Select(r => new RentServiceModel()
				{
					CarId = r.CarId,
					RentalStartDate = r.RentalStartDate,
					RentalEndDate = r.RentalEndDate,
				}).ToListAsync();

			var rent = new Rent
			{
				RentId = rentModel.RentId,
				RenterId = userId,
				CarId = carId,
				RentalStartDate = rentModel.RentalStartDate,
				RentalEndDate = rentModel.RentalEndDate,
			};

			if(rent.RentalStartDate > rent.RentalEndDate)
			{
				throw new InvalidOperationException("Start date cannot be after End date!");
			}

			if(rents.Any(r =>r.CarId == rent.CarId && rent.RentalStartDate.Date >= r.RentalStartDate.Date && r.RentalEndDate.Date <= rent.RentalEndDate.Date))
			{
				throw new InvalidOperationException("The car is currently not available!");
			}

			var difDates = rent.RentalEndDate - rentModel.RentalStartDate;

			rent.TotalCost = difDates.Days * (double)carPrice;

			await db.Rents.AddAsync(rent);
			await db.SaveChangesAsync();
		}
	}
}
