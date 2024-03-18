using FuryRent.Infrastructure.Enumerators.Car;
using System.ComponentModel.DataAnnotations;

namespace FuryRent.Core.Models.Car
{
    public class AllCarsQueryModel
    {
        public const int CarsPerPage = 3;

        [Display(Name = "Filter by Make")]
        public string Make { get; init; } = string.Empty;

        public CarSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCarsCount { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; } = new List<CarServiceModel>();
    }
}
