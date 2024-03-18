namespace FuryRent.Core.Models.Car
{
	public class CarServiceModel
	{
		public int Id { get; set; }

		public string ImageUrl { get; set; } = null!;

		public string Make { get; set; } = string.Empty;

		public string Model { get; set; } = string.Empty;

		public string PricePerDay { get; set; } = string.Empty;
	}
}
