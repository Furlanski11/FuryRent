namespace FuryRent.Core.Models.Rent
{
	public class RentViewModel
	{
        public int Id { get; set; }

        public int CarId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string PricePerDay { get; set; } = string.Empty;

        public DateTime RentalStartDate { get; set; }

		public DateTime RentalEndDate { get; set; }

        public string TotalCost { get; set; } = string.Empty;

        public bool IsPaid { get; set; } = false;
    }
}
