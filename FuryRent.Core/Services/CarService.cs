using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
using FuryRent.Core.Models.Car;
using FuryRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FuryRent.Infrastructure.Data.Models;
using FuryRent.Infrastructure.Enumerators.Car;

namespace FuryRent.Core.Services
{
	public class CarService : ICarService
	{
		private readonly FuryRentDbContext db;

		public CarService(FuryRentDbContext _db)
		{
			db = _db;
		}

		public CarQueryServiceModel All(string? make, CarSorting sorting = CarSorting.Make, int currentPage = 1, int carsPerPage = 3)
		{
			var carsQuery = db.Cars.AsQueryable();

			if (!string.IsNullOrWhiteSpace(make))
			{
				carsQuery = carsQuery
					.Where(c => c.Make == make);
			}

			carsQuery = sorting switch
			{
				CarSorting.Price => carsQuery.OrderBy(c => c.PricePerDay),
				CarSorting.Year => carsQuery.OrderBy(c => c.YearOfProduction),
				CarSorting.Make => carsQuery.OrderBy(c => c.Make),
				CarSorting.HorsePower => carsQuery.OrderByDescending(c => c.Horsepower),
				_ => carsQuery.OrderBy(c => c.Id)
			};

			var cars = carsQuery
				.Skip((currentPage - 1) * carsPerPage)
				.Take(carsPerPage)
				.Select(c => new CarServiceModel
				{
					Id = c.Id,
					ImageUrl = c.ImageUrl,
					Make = c.Make,
					Model = c.Model,
					PricePerDay = $"{c.PricePerDay:f2}",
					IsVipOnly = c.IsVipOnly
				}).ToList();
				
			var totalCars = carsQuery.Count();

			return new CarQueryServiceModel()
			{
				TotalCarsCount = totalCars,
				Cars = cars
			};
		}

		//Adding a car
		public async Task Add(AddCarViewModel carModel)
		{
			var car = new Car()
			{
				Id = carModel.Id,
				ImageUrl = carModel.ImageUrl,
				Make = carModel.Make,
				Model = carModel.Model,
				Color = carModel.Color,
				Kilometers = carModel.Kilometers,
				EngineTypeId = carModel.EngineTypeId,
				Horsepower = carModel.Horsepower,
				GearboxTypeId = carModel.GearboxTypeId,
				YearOfProduction = carModel.YearOfProduction,
				PricePerDay = carModel.PricePerDay,
				CategoryId = carModel.CategoryId,
				IsVipOnly = carModel.IsVipOnly
			};

			await db.Cars.AddAsync(car);
			await db.SaveChangesAsync();
		}

		//Details about a car by given Id
		public async Task<DetailsViewModel> Details(int Id)
		{
			var car = await db.Cars
				.Include(c => c.Category)
				.Include(c => c.EngineType)
				.Include(c => c.GearboxType)
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == Id);

			if (car == null)
			{
				throw new NoSuchCarException(CarConstants.NoSuchCarErrorMessage);
			}

			var model = new DetailsViewModel()
			{
				Id = car.Id,
				ImageUrl = car.ImageUrl,
				Make = car.Make,
				Model = car.Model,
				Color = car.Color,
				Kilometers = car.Kilometers,
				EngineType = car.EngineType.Name,
				Horsepower = car.Horsepower,
				GearboxType = car.GearboxType.Name,
				YearOfProduction = car.YearOfProduction,
				IsAvailable = car.IsAvailable,
				PricePerDay = $"{car.PricePerDay:f2}",
				Category = car.Category.Name
			};

			return model;
		}

		//Editing a car by a given Id
		public async Task<EditCarFormModel> Edit(int id)
		{
			var car = await GetById(id);

			if (car == null)
			{
				throw new NoSuchCarException(CarConstants.NoSuchCarErrorMessage);
			}

			var editedCar = new EditCarFormModel()
			{
				Id = id,
				ImageUrl = car.ImageUrl,
				Make = car.Make,
				Model = car.Model,
				Color = car.Color,
				Kilometers = car.Kilometers,
				EngineTypeId = car.EngineTypeId,
				Horsepower = car.Horsepower,
				GearboxTypeId = car.GearboxTypeId,
				YearOfProduction = car.YearOfProduction,
				PricePerDay = car.PricePerDay,
				CategoryId = car.CategoryId,
				Categories = await GetCategories(),
				EngineTypes = await GetEngineTypes(),
				GearboxTypes = await GetGearboxTypes()
			};

			return editedCar;
		}

		public async Task Edit(EditCarFormModel formModel, int id)
		{
			var car = await this.GetById(id);

			if (car == null)
			{
				throw new NoSuchCarException(CarConstants.NoSuchCarErrorMessage);
			}

			car.Id = formModel.Id;
			car.ImageUrl = formModel.ImageUrl;
			car.Make = formModel.Make;
			car.Model = formModel.Model;
			car.Color = formModel.Color;
			car.Kilometers = formModel.Kilometers;
			car.EngineTypeId = formModel.EngineTypeId;
			car.GearboxTypeId = formModel.GearboxTypeId;
			car.YearOfProduction = formModel.YearOfProduction;
			car.Horsepower = formModel.Horsepower;
			car.PricePerDay = formModel.PricePerDay;
			car.CategoryId = formModel.CategoryId;
			car.IsVipOnly = formModel.IsVipOnly;

			await db.SaveChangesAsync();
		}

		//Deleting a car by a given Id
		public async Task Delete(int id)
		{
			var car = await db.Cars
				.Where(s => s.Id == id)
				.FirstOrDefaultAsync();

			if (car == null)
			{
				throw new NoSuchCarException(CarConstants.NoSuchCarErrorMessage);
			}

			db.Cars.Remove(car);
			await db.SaveChangesAsync();
		}

		//Returns a car by a given Id
		public async Task<Car?> GetById(int? id)
		{
			var car = await db.Cars.FirstOrDefaultAsync(x => x.Id == id);

			if(car == null)
			{
				throw new NoSuchCarException(CarConstants.NoSuchCarErrorMessage);
			}

			return car;
		}

		//Returns all categories
		public async Task<IEnumerable<CategoryViewModel>> GetCategories()
		{
			return await db.Categories
				.AsNoTracking()
				.Select(t => new CategoryViewModel
				{
					Id = t.Id,
					Name = t.Name,
				}).ToListAsync();

		}

		//Returns all Engine types
		public async Task<IEnumerable<EngineTypeViewModel>> GetEngineTypes()
		{
			return await db.EngineTypes
			   .AsNoTracking()
			   .Select(t => new EngineTypeViewModel
			   {
				   Id = t.Id,
				   Name = t.Name,
			   }).ToListAsync();
		}

		//Returns all Gearbox types
		public async Task<IEnumerable<GearboxTypeViewModel>> GetGearboxTypes()
		{
			return await db.GearboxTypes
				.AsNoTracking()
				.Select(t => new GearboxTypeViewModel
				{
					Id = t.Id,
					Name = t.Name,
				}).ToListAsync();
		}
	}
}
