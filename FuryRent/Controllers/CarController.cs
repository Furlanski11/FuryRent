using FuryRent.Core;
using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
using FuryRent.Core.Models.Car;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuryRent.Controllers
{
	[Authorize()]
    public class CarController : Controller
    {
        private readonly ICarService cars;

        public CarController(ICarService _cars)
        {
            cars = _cars;
        }

        [HttpGet]
		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = cars.All(
                query.Make,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            query.TotalCarsCount = queryResult.TotalCarsCount;
            query.Cars = queryResult.Cars;

            return View(query);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Details(int Id)
        {
            DetailsViewModel model = new();

            try
            {
				 model = await cars.Details(Id);
			}
            catch (NoSuchCarException)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var carModel = new AddCarViewModel
            {
                Categories = await cars.GetCategories(),
                EngineTypes = await cars.GetEngineTypes(),
                GearboxTypes = await cars.GetGearboxTypes()
            };

            return View(carModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddCarViewModel carModel)
        {
            DateTime dateTime = DateTime.Now;


            if (!ModelState.IsValid)
            {
                carModel.Categories = await cars.GetCategories();
                carModel.GearboxTypes = await cars.GetGearboxTypes();
                carModel.EngineTypes = await cars.GetEngineTypes();

                return RedirectToAction(nameof(Add));
            }

            await cars.Add(carModel);

            TempData["message"] = "You have successfully added a car";

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Car? car = new();

            try
            {
                car = await cars.GetById(id);
            }
            catch (NoSuchCarException)
            {

                return NotFound();
            }

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(DeleteCarViewModel carModel)
        {
            var car = await cars.GetById(carModel.Id);

            if (car == null)
            {
				return BadRequest(CarConstants.NoSuchCarErrorMessage);
			}

            await cars.Delete(car.Id);

            TempData["message"] = "You have successfully deleted a car";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            EditCarFormModel? car = new();
            try
            {
                 car = await cars.Edit(id);
            }
            catch (NoSuchCarException)
            {

                return NotFound();
            }
            
            return View(car);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditCarFormModel formModel, int id)
        {
            var car = await cars.GetById(id);

            DateTime dateTime = DateTime.Now.Date;

            if (car == null)
            {
				return NotFound();
			}

            if (!ModelState.IsValid)
            {
                formModel.Categories = await cars.GetCategories();
                formModel.EngineTypes = await cars.GetEngineTypes();
                formModel.GearboxTypes = await cars.GetGearboxTypes();

                RedirectToAction(nameof(Edit));
            }

            try
            {
                await cars.Edit(formModel, car.Id);
            }
            catch (NoSuchCarException)
            {

                return NotFound();
            }

            TempData["message"] = "You have successfully edited a car";

            return RedirectToAction(nameof(Details), new {id});
        }
    }
}
