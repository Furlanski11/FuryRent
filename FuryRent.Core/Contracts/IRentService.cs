using FuryRent.Core.Models.Rent;

namespace FuryRent.Core.Contracts
{
	public interface IRentService
	{
		public Task Add(AddRentViewModel rentModel,string userId, int carId);

		public Task<IEnumerable<RentViewModel>> All(string userId);
	}
}
