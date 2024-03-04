using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuryRent.Infrastructure.Data.Configuration
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(CreateCars());
        }

        private List<Car> CreateCars()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    ImageUrl = "https://cdn.dealeraccelerate.com/miami/1/221/3697/1920x1440/2017-audi-s8-plus",
                    Make = "Audi",
                    Model = "S8",
                    Color = "Black",
                    Kilometers = 118000,
                    EngineType = 1,
                    Horsepower = 605,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2017, 7, 25),
                    PricePerDay = 450,
                    CategoryId = 1,
                    IsAvailable = true,
                    IsVipOnly = false
                },
                new Car()
                {
                    Id = 2,
                    ImageUrl = "https://avogroup.lv/wp-content/uploads/2022/04/F10-F11-M-Sport-M5-Side-skirts-addons-blades-Performance-ABS-White-Matt-3.jpg",
                    Make = "BMW",
                    Model = "M5 F10",
                    Color = "White",
                    Kilometers = 140000,
                    EngineType = 1,
                    Horsepower = 560,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2012, 2, 1),
                    PricePerDay = 300,
                    CategoryId = 1,
                    IsAvailable = true,
                    IsVipOnly = false
                },
                new Car()
                {
                    Id = 3,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/19/Mercedes-Benz_CLS63_AMG_Stealth_%288676918717%29.jpg",
                    Make = "Mercedes",
                    Model = "CLS63 AMG",
                    Color = "Red",
                    Kilometers = 70000,
                    EngineType = 1,
                    Horsepower = 585,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2014, 7, 10),
                    PricePerDay = 400,
                    CategoryId = 1,
                    IsAvailable = true,
                    IsVipOnly = false
                },
                new Car()
                {
                    Id = 4,
                    ImageUrl = "https://www.clinkardcars.co.uk/blobs/Images/Stock/208/177fe8ee-d38b-42f1-b171-90510b06abb6.JPG?width=2000&height=1333",
                    Make = "Ferrari",
                    Model = "458",
                    Color = "Yellow",
                    Kilometers = 62000,
                    EngineType = 1,
                    Horsepower = 562,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2012, 2, 15),
                    PricePerDay = 600,
                    CategoryId = 5,
                    IsAvailable = true,
                    IsVipOnly = false
                },
                new Car()
                {
                    Id = 5,
                    ImageUrl = "https://www.europeanprestige.co.uk/blobs/stock/338/images/c22c4591-3a08-4389-9ef9-4cb1c2126400/hi4a1239.jpg?width=2000&height=1333",
                    Make = "Nissan",
                    Model = "GTR",
                    Color = "Black",
                    Kilometers = 150000,
                    EngineType = 1,
                    Horsepower = 580,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2016, 11, 5),
                    PricePerDay = 350,
                    CategoryId = 3,
                    IsAvailable = true,
                    IsVipOnly = false
                },
                new Car()
                {
                    Id = 6,
                    ImageUrl = "https://autozine.org/Archive/Chrysler/new/Challenger_Hellcat_1.jpg",
                    Make = "Dodge",
                    Model = "Challenger SRT",
                    Color = "Red",
                    Kilometers = 110000,
                    EngineType = 1,
                    Horsepower = 717,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2019, 6, 12),
                    PricePerDay = 550,
                    CategoryId = 2,
                    IsAvailable = true,
                    IsVipOnly = false
                },
                new Car()
                {
                    Id = 7,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2f/Aston_Martin_DBS_-_Flickr_-_Alexandre_Pr%C3%A9vot_%2811%29_%28cropped%29.jpg",
                    Make = "Aston Martin",
                    Model = "DBS",
                    Color = "Silver",
                    Kilometers = 78000,
                    EngineType = 1,
                    Horsepower = 517,
                    GearboxType = 1,
                    YearOfProduction = new DateTime(2008, 9, 15),
                    PricePerDay = 300,
                    CategoryId = 4,
                    IsAvailable = true,
                    IsVipOnly = false
                }
            };
            return cars;
        }
    }
}
