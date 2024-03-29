using FuryRent.Core.Models.Pay;
using FuryRent.Core.Models.Rent;

namespace FuryRent.Core.Contracts
{
    public interface IPayService
	{
		public Task Pay(PayServiceViewModel model);

        public Task<IEnumerable<PaymentTypeViewModel>> GetPaymentTypes();

        public Task<PayServiceViewModel> GetRentById(int id);
    }
}
