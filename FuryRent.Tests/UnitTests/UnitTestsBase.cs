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

        public ApplicationUser bonusRenter { get; private set; } = null!;

        public VipUser vipUser { get; private set; } = null!;

		public Car vipCar { get; private set; } = null!;

		public Car nonVipCar { get; private set; } = null!;

		public Car bonusCar { get; private set; } = null!;

		public Rent renterUserRent { get; private set; } = null!;

		public Payment payment { get; private set; } = null!;

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
			_data.Users.Add(vipRenter);

            bonusRenter = new ApplicationUser()
            {
                Id = "BonusRenterId",
                Email = "bonusRenter@mail.com",
                FirstName = "Bonus",
                LastName = "Renter"
            };
            _data.Users.Add(bonusRenter);

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

			renterUserRent = new Rent()
			{
				RentId = 1,
				CarId = 1,
				RenterId = "RenterUserId",
				RentalStartDate = new DateTime(2024, 5, 5),
				RentalEndDate = new DateTime(2024, 5, 7),
				TotalCost = 500
			};
			_data.Rents.Add(renterUserRent);

			renterUserRent = new Rent()
			{
				RentId = 2,
				CarId = 3,
				RenterId = "RenterUserId",
				RentalStartDate = new DateTime(2024, 6, 12),
				RentalEndDate = new DateTime(2024, 6, 15),
				TotalCost = 600
			};
			_data.Rents.Add(renterUserRent);

            renterUserRent = new Rent()
            {
                RentId = 3,
                CarId = 2,
                RenterId = "RenterUserId",
                RentalStartDate = new DateTime(2024, 7, 25),
                RentalEndDate = new DateTime(2024, 7, 27),
                TotalCost = 500
            };
            _data.Rents.Add(renterUserRent);

            payment = new Payment()
			{
				Id = 1,
				RentId = 1,
				PaymentTypeId = 1,
				PaymentAmount = 500,
				PaymentDate = new DateTime(2024, 5, 10)
			};
			_data.Payments.Add(payment);

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

			PaymentTypes paymentTypeCash = new()
			{
				Id = 1,
				TypeName = "Cash"
			};
			_data.PaymentTypes.Add(paymentTypeCash);

            PaymentTypes paymentTypeCard = new()
            {
                Id = 2,
                TypeName = "Card"
            };
            _data.PaymentTypes.Add(paymentTypeCard);
            _data.SaveChanges();
		}
    }
}
