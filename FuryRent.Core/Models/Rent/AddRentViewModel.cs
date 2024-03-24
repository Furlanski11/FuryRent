using System.ComponentModel.DataAnnotations;

namespace FuryRent.Core.Models.Rent
{
	public class AddRentViewModel
	{
        public int RentId { get; set; }

		public string RenterId { get; set; } = string.Empty;

		public int CarId { get; set; }

        [Required]
		public DateTime RentalStartDate { get; set; }

		[Required]
		public DateTime RentalEndDate { get; set; }

		public double TotalCost { get; set; }
	}
}
