namespace FuryRent.Core.Models.Car
{
	public class CarQueryServiceModel
	{
        public int TotalCarsCount { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; } = new List<CarServiceModel>();
    }
}
