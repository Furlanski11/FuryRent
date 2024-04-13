using FuryRent.Core;
using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
using FuryRent.Core.Models.Car;
using FuryRent.Core.Services;
using FuryRent.Infrastructure.Data.Models;
using FuryRent.Infrastructure.Enumerators.Car;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Tests.UnitTests
{
	[TestFixture]
	public class CarServiceTests : UnitTestsBase
	{
		private ICarService _carService;

		[OneTimeSetUp]
		public void Setup()
			=> _carService = new CarService(_data);

		[Test]
		public async Task GetById_ShouldReturnCorrectCarId()
		{
			Car car = await _data.Cars.FirstAsync(c => c.Id == 1);
			var result = _carService.GetById(1);

			Assert.That(result.Id, Is.EqualTo(car.Id));
		}

		[Test]
		public async Task GetById_WithInvalidId_ThrowsNoSuchCarException()
		{
			var result = _carService.GetById(12);

			var ex = Assert.ThrowsAsync<NoSuchCarException>(async () => await result);
			Assert.That(CarConstants.NoSuchCarErrorMessage.ToString(), Is.EqualTo(ex.Message));
		}

		[Test]
		public async Task GetCarId_ShouldReturnInCorrectCarId()
		{
			Car car = await _data.Cars.FirstAsync(c => c.Id == 1);
			var result = _carService.GetById(2);

			Assert.IsNotNull(result);
			Assert.That(car.Id, Is.Not.EqualTo(result.Id));
		}

		[Test]
		public async Task GetCategories_ShouldReturnCategories()
		{
			var categories = await _data.Categories.ToListAsync();
			var result = _carService.GetCategories().Result.ToList();

			Assert.That(result.Count, Is.EqualTo(categories.Count())); // Ensure correct number of categories returned

			Assert.IsNotNull(result);

			var germanCategory = result.FirstOrDefault(t => t.Name == "german");
			Assert.IsNotNull(germanCategory);
			Assert.That(germanCategory.Id, Is.EqualTo(1));

			var americanCategory = result.FirstOrDefault(t => t.Name == "american");
			Assert.IsNotNull(americanCategory);
			Assert.That(americanCategory.Id, Is.EqualTo(2));
		}

		[Test]
		public async Task GetEngineTypes_ShouldReturnEngineTypes()
		{
			var engineTypes = await _data.EngineTypes.ToListAsync();
			var result = _carService.GetEngineTypes().Result.ToList();

			Assert.That(result.Count, Is.EqualTo(engineTypes.Count())); // Ensure correct number of engine types returned

			Assert.IsNotNull(result);

			var petrolEngine = result.FirstOrDefault(t => t.Name == "petrol");
			Assert.IsNotNull(petrolEngine);
			Assert.That(petrolEngine.Id, Is.EqualTo(1));

			var dieselEngine = result.FirstOrDefault(t => t.Name == "diesel");
			Assert.IsNotNull(dieselEngine);
			Assert.That(dieselEngine.Id, Is.EqualTo(2));
		}

		[Test]
		public async Task GetGearboxTypes_ShouldReturnGearboxTypes()
		{
			var gearboxTypes = await _data.GearboxTypes.ToListAsync();
			var result = _carService.GetGearboxTypes().Result.ToList();

			Assert.That(result.Count, Is.EqualTo(gearboxTypes.Count())); // Ensure correct number of engine types returned

			Assert.IsNotNull(result);

			var manualGearbox = result.FirstOrDefault(t => t.Name == "manual");
			Assert.IsNotNull(manualGearbox);
			Assert.That(manualGearbox.Id, Is.EqualTo(1));

			var automaticGearbox = result.FirstOrDefault(t => t.Name == "automatic");
			Assert.IsNotNull(automaticGearbox);
			Assert.That(automaticGearbox.Id, Is.EqualTo(2));
		}

		[Test]
		public async Task Add_AddsNewCarToDatabase()
		{
			int carsCount = await _data.Cars.CountAsync();

			Assert.That(carsCount, Is.EqualTo(3));

			var carModel = new AddCarViewModel
			{
				Id = 4,
				ImageUrl = "image_url",
				Make = "Toyota",
				Model = "Camry",
				Color = "Red",
				Kilometers = 50000,
				EngineTypeId = 1,
				Horsepower = 150,
				GearboxTypeId = 1,
				PricePerDay = 50.0M,
				CategoryId = 1,
				IsVipOnly = false
			};

			await _carService.Add(carModel);

			carsCount = await _data.Cars.CountAsync();

			Assert.That(carsCount, Is.EqualTo(4));
		}

		[Test]
		public async Task Delete_ExistingCar_RemovesFromDatabase()
		{

			int carsCount = await _data.Cars.CountAsync();

			Assert.That(carsCount, Is.EqualTo(4));

			await _carService.Delete(2);

			carsCount = await _data.Cars.CountAsync();

			Assert.That(carsCount, Is.EqualTo(3));
		}

		[Test]
		public async Task Delete_NonExistingCar_ThrowsNoSuchCarException()
		{
			int nonExistingCarId = 100;

			var ex = Assert.ThrowsAsync<NoSuchCarException>(async () => await _carService.Delete(nonExistingCarId));

			Assert.That(CarConstants.NoSuchCarErrorMessage.ToString(), Is.EqualTo(ex.Message));
		}

		[Test]
		public async Task Edit_ExistingCar_UpdatesPropertiesAndSavesChanges()
		{
			int carId = 1;
			var formModel = new EditCarFormModel
			{
				Id = carId,
				Make = "Audi",
				Model = "RS6",
				ImageUrl = "https://cdn4.focus.bg/fakti/photos/16x9/47f/specialno-audi-rs6-sas-750ks-3.jpg",
				Color = "Yellow", // changed the color from Red to Yellow
				Horsepower = 550,
				Kilometers = 51000, // increased the kilometers by 1000
				EngineTypeId = 1,
				GearboxTypeId = 2, //changed the gearbox type from 1 to 2
				PricePerDay = 350, // increased the price per day by 100
				CategoryId = 1,
				IsVipOnly = true
			};

			var existingCar = await _carService.GetById(carId);

			
			await _carService.Edit(formModel, carId);

			if(existingCar != null)
			{
				Assert.That(formModel.Id, Is.EqualTo(existingCar.Id));
				Assert.That(formModel.ImageUrl, Is.EqualTo(existingCar.ImageUrl));
				Assert.That(formModel.Make, Is.EqualTo(existingCar.Make));
				Assert.That(formModel.Model, Is.EqualTo(existingCar.Model));
				Assert.That(formModel.Color, Is.EqualTo(existingCar.Color));
				Assert.That(formModel.Kilometers, Is.EqualTo(existingCar.Kilometers));
				Assert.That(formModel.EngineTypeId, Is.EqualTo(existingCar.EngineTypeId));
				Assert.That(formModel.GearboxTypeId, Is.EqualTo(existingCar.GearboxTypeId));
				Assert.That(formModel.YearOfProduction, Is.EqualTo(existingCar.YearOfProduction));
				Assert.That(formModel.Horsepower, Is.EqualTo(existingCar.Horsepower));
				Assert.That(formModel.PricePerDay, Is.EqualTo(existingCar.PricePerDay));
				Assert.That(formModel.CategoryId, Is.EqualTo(existingCar.CategoryId));
				Assert.That(formModel.IsVipOnly, Is.EqualTo(existingCar.IsVipOnly));
			}
		}

		[Test]
		public async Task Edit_NonExistingCar_ThrowsNoSuchCarException()
		{
			int nonExistingCarId = 100;
			var formModel = new EditCarFormModel
			{
				Id = nonExistingCarId,
				Make = "Skoda",
				Model = "Fabia",
				ImageUrl = "https://cdn.actualno.eu/actualno_2013/upload/news/2020/10/28/0015632001603891172_1514159_920x708.webp",
				Color = "Red", 
				Horsepower = 200,
				Kilometers = 51000,
				EngineTypeId = 2,
				GearboxTypeId = 1,
				PricePerDay = 100,
				CategoryId = 1,
				IsVipOnly = false
			};

			var ex = Assert.ThrowsAsync<NoSuchCarException>(async () => await _carService.Edit(formModel, nonExistingCarId));
			Assert.That(CarConstants.NoSuchCarErrorMessage.ToString(), Is.EqualTo(ex.Message));
		}

		[Test]
		public async Task Details_ExistingCar_ReturnsDetailsViewModel()
		{
			int carId = 1;
			var car = await _data.Cars.FirstAsync(c => c.Id == carId);

			var result = await _carService.Details(carId);

			Assert.IsNotNull(result);
			Assert.That(car.Id,Is.EqualTo(result.Id));
			Assert.That(car.ImageUrl, Is.EqualTo(result.ImageUrl));
			Assert.That(car.Make, Is.EqualTo(result.Make));
			Assert.That(car.Model, Is.EqualTo(result.Model));
			Assert.That(car.Color, Is.EqualTo(result.Color));
			Assert.That(car.Kilometers, Is.EqualTo(result.Kilometers));
			Assert.That(car.EngineType.Name, Is.EqualTo(result.EngineType));
			Assert.That(car.Horsepower, Is.EqualTo(result.Horsepower));
			Assert.That(car.GearboxType.Name, Is.EqualTo(result.GearboxType));
			Assert.That(car.YearOfProduction, Is.EqualTo(result.YearOfProduction));
			Assert.That($"{car.PricePerDay:f2}", Is.EqualTo(result.PricePerDay));
			Assert.That(car.Category.Name, Is.EqualTo(result.Category));
		}

		[Test]
		public async Task Details_NonExistingCar_ThrowsNoSuchCarException()
		{
			int nonExistingCarId = 100;

			var ex = Assert.ThrowsAsync<NoSuchCarException>(async () => await _carService.Details(nonExistingCarId));
			Assert.That(CarConstants.NoSuchCarErrorMessage.ToString(), Is.EqualTo(ex.Message));
		}

		[Test]
		public void All_ReturnsCorrectCars()
		{
			var result = _carService.All(make: "Audi", sorting: CarSorting.Price, currentPage: 1, carsPerPage: 2);

			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Cars.Count());
			Assert.AreEqual(2, result.TotalCarsCount);
			Assert.AreEqual(3, result.Cars.First().Id); // First car should have lowest price
			Assert.AreEqual(1, result.Cars.Last().Id); // Last car should have higher price
		}

		[OneTimeTearDown]
		public void TearDown()
			=> _data.Dispose();
	}
}
