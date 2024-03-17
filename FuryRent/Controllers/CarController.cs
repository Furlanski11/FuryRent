using FuryRent.Core;
using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Car;
using FuryRent.Core.Services;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FuryRent.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService cars;

        public CarController(ICarService _cars)
        {
            cars = _cars;
        }

        private readonly FuryRentDbContext context;

        public CarController(FuryRentDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = cars.All();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ByMake(string make)
        {
            if (make == null)
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

        [HttpGet]
        public async Task<IActionResult> OrderBy(string data)
        {
            if (data == null)
            {
                return RedirectToAction(nameof(All));
            }

            var model = new List<AllCarsQueryModel>();

            switch (data)
            {
                case "Price":
                    model = await context.Cars
                    .OrderBy(c => c.PricePerDay)
                     .AsNoTracking()
                     .Select(c => new AllCarsQueryModel()
                     {
                         Id = c.Id,
                         Make = c.Make,
                         Model = c.Model,
                         ImageUrl = c.ImageUrl,
                         PricePerDay = $"{c.PricePerDay:f2}",
                     })
                     .ToListAsync();
                    break;

                case "Make":
                    model = await context.Cars
                    .OrderBy(c => c.Make)
                     .AsNoTracking()
                     .Select(c => new AllCarsQueryModel()
                     {
                         Id = c.Id,
                         Make = c.Make,
                         Model = c.Model,
                         ImageUrl = c.ImageUrl,
                         PricePerDay = $"{c.PricePerDay:f2}",
                     })
                     .ToListAsync();
                    break;

                case "Year":
                    model = await context.Cars
                     .OrderBy(c => c.YearOfProduction)
                      .AsNoTracking()
                       .Select(c => new AllCarsQueryModel()
                       {
                           Id = c.Id,
                           Make = c.Make,
                           Model = c.Model,
                           ImageUrl = c.ImageUrl,
                           PricePerDay = $"{c.PricePerDay:f2}",
                       })
                       .ToListAsync();
                    break;

                case "HorsePower":
                    model = await context.Cars
                     .OrderBy(c => c.Horsepower)
                      .AsNoTracking()
                       .Select(c => new AllCarsQueryModel()
                       {
                           Id = c.Id,
                           Make = c.Make,
                           Model = c.Model,
                           ImageUrl = c.ImageUrl,
                           PricePerDay = $"{c.PricePerDay:f2}",
                       })
                       .ToListAsync();
                    break;

                default:
                    throw new InvalidDataException();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var model = await cars.Details(Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var carModel = new AddCarViewModel();

            carModel.Categories = await GetCategories();
            carModel.EngineTypes = await GetEngineTypes();
            carModel.GearboxTypes = await GetGearboxTypes();

            return View(carModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarViewModel carModel)
        {
            DateTime dateTime = DateTime.Now;

            if (!DateTime.TryParseExact(carModel.YearOfProduction,
                CarConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateTime))
            {
                ModelState.AddModelError(nameof(carModel.YearOfProduction), $"Invalid date! format must be {CarConstants.DateFormat}");
            }

            bool isVipOnly = false;

            switch (carModel.IsVipOnly)
            {
                case "Yes":
                    isVipOnly = true;
                    break;

                case "No":
                    isVipOnly = false;
                    break;

                default:
                    ModelState.AddModelError(nameof(carModel.IsVipOnly), "Invalid input, the input must be 'Yes' or 'No'");
                    break;
            }

            if (!ModelState.IsValid)
            {
                carModel.Categories = await GetCategories();
                carModel.GearboxTypes = await GetGearboxTypes();
                carModel.EngineTypes= await GetEngineTypes();

                return RedirectToAction(nameof(Add));
            }

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
                YearOfProduction = dateTime,
                PricePerDay = carModel.PricePerDay,
                CategoryId = carModel.CategoryId,
                IsVipOnly = isVipOnly
            };

            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await context.Cars
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            var model = new DeleteCarViewModel()
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                ImageUrl = car.ImageUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DeleteCarViewModel carModel)
        {
            var car = await context.Cars
                .Where(s => s.Id == carModel.Id)
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return BadRequest();
            }

             context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await context.Cars.FindAsync(id);

            if (car == null)
            {
                return BadRequest();
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
                YearOfProduction = car.YearOfProduction.ToString(FuryRent.Core.CarConstants.DateFormat),
                PricePerDay = car.PricePerDay,
                CategoryId = car.CategoryId,
                
            };

            editedCar.Categories = await GetCategories();
            editedCar.EngineTypes = await GetEngineTypes();
            editedCar.GearboxTypes = await GetGearboxTypes();

            return View(editedCar);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarFormModel formModel, int id)
        {
            var car = await context.Cars.FindAsync(id);

            DateTime dateTime = DateTime.Now.Date;

            if (!DateTime.TryParseExact(formModel.YearOfProduction,
               CarConstants.DateFormat,
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out dateTime))
            {
                ModelState.AddModelError(nameof(formModel.YearOfProduction), $"Invalid date! format must be {CarConstants.DateFormat}");
            }

            if (car == null)
            {
                return BadRequest();
            }

            bool isVipOnly = false;

            switch (formModel.IsVipOnly)
            {
                case "Yes":
                    isVipOnly = true;
                    break;

                case "No":
                    isVipOnly = false;
                    break;

                default:
                    ModelState.AddModelError(nameof(formModel.IsVipOnly), "Invalid input, the input must be 'Yes' or 'No'");
                    break;
            }

            if (!ModelState.IsValid)
            {
                formModel.Categories = await GetCategories();
                formModel.EngineTypes = await GetEngineTypes();
                formModel.GearboxTypes = await GetGearboxTypes();

                RedirectToAction(nameof(Edit));
            }

            car.Id = formModel.Id;
            car.ImageUrl = formModel.ImageUrl;
            car.Make = formModel.Make;
            car.Model = formModel.Model;
            car.Color = formModel.Color;
            car.Kilometers = formModel.Kilometers;
            car.EngineTypeId = formModel.EngineTypeId;
            car.GearboxTypeId = formModel.GearboxTypeId;
            car.YearOfProduction = dateTime;
            car.Horsepower = formModel.Horsepower;
            car.PricePerDay = formModel.PricePerDay;
            car.CategoryId = formModel.CategoryId;
            car.IsVipOnly = isVipOnly;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await context.Categories
                .AsNoTracking()
                .Select(t => new CategoryViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToListAsync();
        }

        private async Task<IEnumerable<EngineTypeViewModel>> GetEngineTypes()
        {
            return await context.EngineTypes
                .AsNoTracking()
                .Select(t => new EngineTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToListAsync();
        }

        private async Task<IEnumerable<GearboxTypeViewModel>> GetGearboxTypes()
        {
            return await context.GearboxTypes
                .AsNoTracking()
                .Select(t => new GearboxTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToListAsync();
        }
    }
}
