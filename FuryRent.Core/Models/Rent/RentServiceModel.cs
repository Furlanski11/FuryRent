namespace FuryRent.Core.Models.Rent
{
	public class RentServiceModel
	{
        public int RentId { get; set; }

        public int CarId { get; set; }

        public DateTime RentalStartDate { get; set; }

		public DateTime RentalEndDate { get; set; }
	}
}
