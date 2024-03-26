using FuryRent.Core.Models.Vip;

namespace FuryRent.Core.Contracts
{
	public interface IVipService
    {
        public Task Become(VipUserServiceModel model);
    }
}
