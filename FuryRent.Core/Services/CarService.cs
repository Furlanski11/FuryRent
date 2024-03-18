using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Car;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Globalization;


//moq proekt

namespace FuryRent.Core.Services
{
    public class CarService : ICarService
    {
        private readonly FuryRentDbContext db;

        public CarService(FuryRentDbContext _db)
        {
            db = _db;
        }

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
                YearOfProduction = DateTime.Parse(carModel.YearOfProduction),
                PricePerDay = carModel.PricePerDay,
                CategoryId = carModel.CategoryId,
                IsVipOnly = carModel.IsVipOnly
            };

            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();
        }



        public async Task<IEnumerable<AllCarsQueryModel>> All(string? make, string? criteria)
        {
            var query = db.Cars.AsQueryable();

            if (make != null)
            {
                query = query.Where(x => x.Make == make);
            }

            if (criteria != null)
            {
                if (criteria == "Price")
                {
                    query = query.OrderBy(x => x.PricePerDay);
                }
                else if (criteria == "Year")
                {
                    query = query.OrderBy(x => x.YearOfProduction);
                }
                else if (criteria == "HorsePower")
                {
                    query = query.OrderByDescending(x => x.Horsepower);
                }
            }

            return await query
               .AsNoTracking()
               .Select(c => new AllCarsQueryModel()
               {
                   Id = c.Id,
                   Make = c.Make,
                   Model = c.Model,
                   ImageUrl = c.ImageUrl,
                   PricePerDay = $"{c.PricePerDay:f2}",
               }).ToListAsync();


        }

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
                throw new InvalidOperationException("There is no such car!");
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

        public async Task<EditCarFormModel> Edit(int id)
        {
            var car = await GetById(id);

            if(car == null)
            {
                throw new InvalidOperationException("There is no such car!");
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

        public async Task<Car?> GetById(int? id)
        {
            var car = await db.Cars.FirstOrDefaultAsync(x => x.Id == id);

            return car;
        }

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

        public async Task<IEnumerable<AllCarsQueryModel>> GetByMake(string make)
        {
            var model = await db.Cars
                .Where(c => c.Make == make)
                .AsNoTracking()
                .Select(c => new AllCarsQueryModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    PricePerDay = $"{c.PricePerDay:f2}",
                }).ToListAsync();

            return model;
        }
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

        public async Task Delete(int id)
        {
            var car = await db.Cars
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            if (car == null)
            {
                throw new Exception("No such car");
            }

            db.Cars.Remove(car);
            await db.SaveChangesAsync();
        }
    }
}
