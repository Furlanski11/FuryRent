using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Rent;
using FuryRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static FuryRent.Core.CarConstants;
using FuryRent.Infrastructure.Data.Models;

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
			var payments = await db.Payments
                .AsNoTracking()
                .Select(p => p.RentId)
                .ToListAsync();

            var rents = await db.Rents
				.Where(r => r.RenterId == userId)
				.OrderByDescending(r => r.Car.Horsepower)
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
				})
				.ToListAsync();

            foreach (var rent in rents)
            {
                if (payments.Contains(rent.Id))
                {
                    rent.IsPaid = true;
                }
				else
				{
					rent.IsPaid = false;
				}
            }

			return rents;
        }

		public async Task Add(AddRentViewModel rentModel, string userId, int carId)
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

			if (rent.RentalStartDate > rent.RentalEndDate)
			{
				throw new InvalidOperationException("Pick up date cannot be after Return date!");
			}

			if(rent.RentalStartDate < DateTime.Now || rent.RentalEndDate < DateTime.Now)
			{
				throw new InvalidOperationException("Cannot rent a car in past date");
			}

			//Checks if the car has been returned
			if (rents.Any(r => r.CarId == rent.CarId && rent.RentalStartDate.Date <= r.RentalEndDate.Date && rent.RentalStartDate >= r.RentalStartDate))
			{
				throw new InvalidOperationException(CarNotAvailableErrorMessage);
			}

			var difDates = rent.RentalEndDate - rentModel.RentalStartDate;

			rent.TotalCost = (difDates.Days + 1) * (double)carPrice;

			await db.Rents.AddAsync(rent);
			await db.SaveChangesAsync();
		}
	}
}
