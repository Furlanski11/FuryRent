using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Car;
using FuryRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Core.Services
{
    public class CarService : ICarService
    {
        private readonly FuryRentDbContext db;

        public CarService(FuryRentDbContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<AllCarsQueryModel>> All()
        {
            return await db.Cars
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
                YearOfProduction = car.YearOfProduction.ToString(FuryRent.Core.CarConstants.DateFormat),
                IsAvailable = car.IsAvailable,
                PricePerDay = $"{car.PricePerDay:f2}",
                Category = car.Category.Name
            };

            return model;
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

        public Task<IEnumerable<EngineTypeViewModel>> GetEngineTypes()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GearboxTypeViewModel>> GetGearboxTypes()
        {
            throw new NotImplementedException();
        }
    }
}
