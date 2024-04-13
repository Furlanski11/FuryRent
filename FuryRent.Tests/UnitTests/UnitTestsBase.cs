using AutoMapper;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using FuryRent.Tests.Mocks;

namespace FuryRent.Tests.UnitTests
{
	public class UnitTestsBase
	{
		protected FuryRentDbContext _data;

        [OneTimeSetUp]
        public void SetUpBase()
		{
			_data = DatabaseMock.Instance;
			SeedDatabase();
		}

		[OneTimeTearDown]
		public void TearDownBase()
			=>_data.Dispose();

		public ApplicationUser Renter { get; private set; } = null!;

		public ApplicationUser vipRenter { get; private set; } = null!;

		public Car vipCar { get; private set; } = null!;

		public Car nonVipCar { get; private set; } = null!;

		public Car bonusCar { get; private set; } = null!;

		private void SeedDatabase()
		{
			Renter = new ApplicationUser()
			{
				Id = "RenterUserId",
				Email = "renter@mail.com",
				FirstName = "Rent",
				LastName = "Er"
			};
			_data.Users.Add(Renter);

			vipRenter = new ApplicationUser()
			{
				Id = "vipRenterUserId",
				Email = "vipRenter@mail.com",
				FirstName = "Vip",
				LastName = "Renter"
			};
			_data.Users.Add(Renter);

			vipCar = new Car()
			{
				Id = 1,
				Make = "Audi",
				Model = "RS6",
				ImageUrl = "https://cdn4.focus.bg/fakti/photos/16x9/47f/specialno-audi-rs6-sas-750ks-3.jpg",
				Color = "Red",
				Horsepower = 550,
				Kilometers = 50000,
				EngineTypeId = 1,
				GearboxTypeId = 1,
				PricePerDay = 250,
				CategoryId = 1,
				IsVipOnly = true
			};
			_data.Cars.Add(vipCar);

			nonVipCar = new Car()
			{
				Id = 2,
				Make = "BMW",
				Model = "M5",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/2018_BMW_M5_Automatic_4.4.jpg/280px-2018_BMW_M5_Automatic_4.4.jpg",
				Color = "Blue",
				Horsepower = 600,
				Kilometers = 17000,
				EngineTypeId = 1,
				GearboxTypeId = 1,
				PricePerDay = 250,
				CategoryId = 1,
				IsVipOnly = false
			};
			_data.Cars.Add(nonVipCar);

			bonusCar = new Car()
			{
				Id = 3,
				Make = "Audi",
				Model = "RS5",
				ImageUrl = "RS5.jpg",
				Color = "Blue",
				Horsepower = 500,
				Kilometers = 17000,
				EngineTypeId = 1,
				GearboxTypeId = 1,
				PricePerDay = 200,
				CategoryId = 1,
				IsVipOnly = false
			};
			_data.Cars.Add(bonusCar);

			VipUser vipUser = new VipUser()
			{
				UserId = vipRenter.Id
			};
			_data.VipUsers.Add(vipUser);

			Category firstCategory = new Category()
			{
				Id = 1,
				Name = "german"
			};
			_data.Categories.Add(firstCategory);

			Category secondCategory = new Category()
			{
				Id = 2,
				Name = "american"
			};
			_data.Categories.Add(secondCategory);

			EngineType firstEngineType = new EngineType()
			{
				Id = 1,
				Name = "petrol"
			};
			_data.EngineTypes.Add(firstEngineType);

			EngineType secondEngineType = new EngineType()
			{
				Id = 2,
				Name = "diesel"
			};
			_data.EngineTypes.Add(secondEngineType);

			GearboxType manualGearboxType = new GearboxType()
			{
				Id = 1,
				Name = "manual"
			};
			_data.GearboxTypes.Add(manualGearboxType);

			GearboxType automaticGearboxType = new GearboxType()
			{
				Id = 2,
				Name = "automatic"
			};
			_data.GearboxTypes.Add(automaticGearboxType);
			_data.SaveChanges();
		}
    }
}
