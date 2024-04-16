using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Rent;
using FuryRent.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Tests.UnitTests
{
	[TestFixture]
	public class RentServiceTests : UnitTestsBase
	{
		private IRentService _rentService;

		[OneTimeSetUp]
		public void Setup()
			=> _rentService = new RentService(_data);

		[Test]
		public async Task All_ReturnsCorrectRents()
		{
			var userId = "RenterUserId";

			var result = await _rentService.All(userId);

			Assert.IsNotNull(result);
			Assert.That(result.Count(), Is.EqualTo(3)); // Three rents should be returned

			var rent1 = result.FirstOrDefault(r => r.Id == 1);
			Assert.IsNotNull(rent1);
			Assert.That(rent1.CarId, Is.EqualTo(1));
			Assert.That(rent1.Make, Is.EqualTo("Audi"));
			Assert.That(rent1.Model, Is.EqualTo("RS6"));
			Assert.That(rent1.ImageUrl, Is.EqualTo("https://cdn4.focus.bg/fakti/photos/16x9/47f/specialno-audi-rs6-sas-750ks-3.jpg"));
			Assert.IsTrue(rent1.IsPaid); // Rent1 has payment associated

			var rent2 = result.FirstOrDefault(r => r.Id == 2);
			Assert.IsNotNull(rent2);
			Assert.That(rent2.CarId, Is.EqualTo(3));
			Assert.That(rent2.Make, Is.EqualTo("Audi"));
			Assert.That(rent2.Model, Is.EqualTo("RS5"));
			Assert.That(rent2.ImageUrl, Is.EqualTo("RS5.jpg"));
			Assert.IsFalse(rent2.IsPaid); // Rent2 has no payment associated

            var rent3 = result.FirstOrDefault(r => r.Id == 3);
            Assert.IsNotNull(rent3);
            Assert.That(rent3.CarId, Is.EqualTo(2));
            Assert.That(rent3.Make, Is.EqualTo("BMW"));
            Assert.That(rent3.Model, Is.EqualTo("M5"));
            Assert.That(rent3.ImageUrl, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/2018_BMW_M5_Automatic_4.4.jpg/280px-2018_BMW_M5_Automatic_4.4.jpg"));
            Assert.IsFalse(rent3.IsPaid); // Rent3 has no payment associated
        }

		[Test]
		public void IsUserVip_ReturnsTrue_WhenUserIsVip()
		{
			var userId = "vipRenterUserId";

			var result = _rentService.IsUserVip(userId);

			Assert.IsTrue(result);
		}

		[Test]
		public void IsUserVip_ReturnsFalse_WhenUserIsNotVip()
		{
			var userId = "RenterUserId";

			var result = _rentService.IsUserVip(userId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task Add_AddsRent_WhenAllConditionsAreMet()
		{
			var allRents = _data.Rents.Count();

			Assert.That(allRents, Is.EqualTo(3));

			var startDate = new DateTime(2024, 5, 25);
			var endDate = new DateTime(2024, 5, 27);

			var rentModel = new AddRentViewModel
			{
				RentId = 4,
				RentalStartDate = startDate,
				RentalEndDate = endDate
			};

			var userId = "vipRenterUserId";

			var car = await _data
				.Cars.
				FirstAsync(c => c.Id == 1);

			await _rentService.Add(rentModel, userId, car.Id);

			allRents = _data.Rents.Count();

			Assert.That(allRents, Is.EqualTo(4));

		}

		[Test]
		public async Task Add_ThrowsException_WhenUserIsNotVip()
		{
			var startDate = new DateTime(2024, 6, 25);
			var endDate = new DateTime(2024, 6, 27);

			var rentModel = new AddRentViewModel
			{
				RentId = 4,
				RentalStartDate = startDate,
				RentalEndDate = endDate
			};

			var userId = "RenterUserId";

			var car = await _data
				.Cars.
				FirstAsync(c => c.Id == 1);

			var ex = Assert.ThrowsAsync<Exception>(async () => await _rentService.Add(rentModel, userId, car.Id));
			Assert.That(ex.Message, Is.EqualTo("You must be a VIP user to rent this car"));
		}

		[Test]
		public async Task Add_ThrowsInvalidOperationException_WhenPickUpDateIsAfterReturnDate()
		{
			var startDate = new DateTime(2024, 6, 27);
			var endDate = new DateTime(2024, 6, 25);

			var rentModel = new AddRentViewModel
			{
				RentId = 5,
				RentalStartDate = startDate,
				RentalEndDate = endDate
			};

			var userId = "RenterUserId";

			var car = await _data
				.Cars.
				FirstAsync(c => c.Id == 2);

			var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _rentService.Add(rentModel, userId, car.Id));
			Assert.That(ex.Message, Is.EqualTo("Pick up date cannot be after Return date!"));
		}

		[Test]
		public async Task Add_ThrowsInvalidOperationException_WhenPickUpDateIsInPastDay()
		{
			var startDate = new DateTime(2024, 3, 7);
			var endDate = new DateTime(2024, 5, 4);

			var rentModel = new AddRentViewModel
			{
				RentId = 5,
				RentalStartDate = startDate,
				RentalEndDate = endDate
			};

			var userId = "RenterUserId";

			var car = await _data
				.Cars.
				FirstAsync(c => c.Id == 2);

			var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _rentService.Add(rentModel, userId, car.Id));
			Assert.That(ex.Message, Is.EqualTo("Cannot rent a car in past date"));
		}

		[Test]
		public async Task Add_ThrowsInvalidOperationException_WhenCarIsNotAvailable()
		{
			var startDate = new DateTime(2024, 5, 6);
			var endDate = new DateTime(2024, 5, 7);

			var rentModel = new AddRentViewModel
			{
				RentId = 5,
				RentalStartDate = startDate,
				RentalEndDate = endDate
			};

			var userId = "vipRenterUserId";

			var car = await _data
			.Cars.
			FirstAsync(c => c.Id == 1);

			var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _rentService.Add(rentModel, userId, car.Id));
			Assert.That(ex.Message, Is.EqualTo("The car is currently not available!"));
		}

			[OneTimeTearDown]
		public void TearDown()
			=> _data.Dispose();
	}
}