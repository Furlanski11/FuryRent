using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
using FuryRent.Core.Models.Vip;
using FuryRent.Core.Services;

namespace FuryRent.Tests.UnitTests
{
    [TestFixture]
    public class VipServiceTests : UnitTestsBase
    {
        private IVipService _vipService;

        [OneTimeSetUp]
        public void Setup()
            => _vipService = new VipService(_data);

        [Test]
        public void Become_ThrowsException_WhenUserIdIsNull()
        {

            var model = new VipUserServiceModel
            {
                UserId = null,
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await _vipService.Become(model));
            Assert.That(ex.Message, Is.EqualTo("UserId cannot be null!"));
        }

        [Test]
        public void Become_ThrowsInvalidOperationException_WhenUserDoesNotMeetRequirements()
        {
            // Arrange
            var model = new VipUserServiceModel
            {
                UserId = "BonusRenterId"
            };

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _vipService.Become(model));
            Assert.That(ex.Message, Is.EqualTo("You must have 3 rents to become a VIP member!"));
        }

        [Test]
        public async Task Become_ThrowsAlreadyVipException_WhenUserIsAlreadyVip()
        {
            var model = new VipUserServiceModel
            {
                UserId = "vipRenterUserId"
            };

            var ex = Assert.ThrowsAsync<AlreadyVipException>(async () => await _vipService.Become(model));
            Assert.That(ex.Message, Is.EqualTo("You are a VIP member already!"));
        }

        [Test]
        public async Task Become_AddsUserAsVip_WhenUserMeetsRequirements()
        {
            var model = new VipUserServiceModel
            {
                UserId = "RenterUserId"
            };

            await _vipService.Become(model);

        }


        [OneTimeTearDown]
        public void TearDown()
           => _data.Dispose();
    }
}
