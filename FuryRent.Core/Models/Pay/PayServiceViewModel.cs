using System.ComponentModel.DataAnnotations;
using static FuryRent.Core.CarConstants;

namespace FuryRent.Core.Models.Pay
{
    public class PayServiceViewModel
    {
        public int Id { get; set; }

        public int RentId { get; set; }

        public double PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int PaymentTypeId { get; set; }

        public IEnumerable<PaymentTypeViewModel> PaymentTypes { get; set; } = new List<PaymentTypeViewModel>();
    }
}
