using FuryRent.Core;
using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Car;
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
        public async Task<IActionResult> Details(int Id)
        {
            var model = await cars.Details(Id);

            return View(model);
        }

        [HttpGet]
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

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await cars.GetById(id);

            if(car == null)
            {
                return BadRequest(CarConstants.NoSuchCarErrorMessage);
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
        public async Task<IActionResult> DeleteConfirmed(DeleteCarViewModel carModel)
        {
            var car = await cars.GetById(carModel.Id);

            if (car == null)
            {
				return BadRequest(CarConstants.NoSuchCarErrorMessage);
			}

            await cars.Delete(car.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var car = await cars.Edit(id);

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarFormModel formModel, int id)
        {
            var car = await cars.GetById(id);

            DateTime dateTime = DateTime.Now.Date;

            if (car == null)
            {
				return BadRequest(CarConstants.NoSuchCarErrorMessage);
			}

            if (!ModelState.IsValid)
            {
                formModel.Categories = await cars.GetCategories();
                formModel.EngineTypes = await cars.GetEngineTypes();
                formModel.GearboxTypes = await cars.GetGearboxTypes();

                RedirectToAction(nameof(Edit));
            }

            await cars.Edit(formModel, car.Id);

            return RedirectToAction(nameof(Details), new {id});
        }
    }
}
