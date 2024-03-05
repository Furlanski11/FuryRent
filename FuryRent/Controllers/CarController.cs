using FuryRent.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using FuryRent.Core.Models.Car;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using FuryRent.Infrastructure.Data.Enumerators;

namespace FuryRent.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly FuryRentDbContext context;

        public CarController(FuryRentDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> All(string order)
        {
            var model = await context.Cars
                .Select(c => new AllCarsQueryModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    PricePerDay = $"{c.PricePerDay:f2}",
                }).ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> ByMake(string make)
        {
            if(make == null)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await context.Cars
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

            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var car = await context.Cars
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == Id);

            if(car == null)
            {
                    throw new InvalidOperationException("There is no such car!");
            }

            var model = new DetailsViewModel()
            {
                ImageUrl = car.ImageUrl,
                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                Kilometers = car.Kilometers,
                EngineType = ((EngineTypeEnum)car.EngineType).ToString(),
                Horsepower = car.Horsepower,
                GearboxType = ((GearboxTypeEnum)car.GearboxType).ToString(),
                YearOfProduction = car.YearOfProduction.ToString(FuryRent.Core.DateConstant.DateFormat),
                IsAvailable = car.IsAvailable,
                PricePerDay = $"{car.PricePerDay:f2}",
                Category = car.Category.Name
            };

            return View(model);
        }
    }
}
