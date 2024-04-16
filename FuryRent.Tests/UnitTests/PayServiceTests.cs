using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Pay;
using FuryRent.Core.Services;

namespace FuryRent.Tests.UnitTests
{
    [TestFixture]
    public class PayServiceTests : UnitTestsBase
    {
        private IPayService _payService;

        [OneTimeSetUp]
        public void Setup()
            => _payService = new PayService(_data);

        [Test]
        public async Task GetRentById_ReturnsCorrectRentDetails()
        {
            int rentId = 1;
            decimal paymentAmount = 500;

            var result = await _payService.GetRentById(rentId);

            Assert.IsNotNull(result);
            Assert.That(rentId, Is.EqualTo(result.RentId));
            Assert.That(result.PaymentAmount, Is.EqualTo(paymentAmount));
        }

        [Test]
        public async Task GetPaymentTypes_ReturnsCorrectPaymentTypes()
        {
            int paymentTypesCount = 2;

            var result = await _payService.GetPaymentTypes();

            Assert.IsNotNull(result);
            Assert.That(paymentTypesCount, Is.EqualTo(result.ToList().Count));
            Assert.That(result.First().Name, Is.EqualTo("Cash"));
            Assert.That(result.Last().Name, Is.EqualTo("Card"));
        }

        [Test]
        public void Pay_ThrowsException_WhenRentNotFound()
        {
            int invalidRentId = 100;

            var model = new PayServiceViewModel
            {
                RentId = invalidRentId,
                PaymentAmount = 200.0,
                PaymentTypeId = 1
            };

            Assert.ThrowsAsync<Exception>(async () => await _payService.Pay(model)); //Throws Exception if the Rent is not found
        }

        [Test]
        public async Task Pay_AddsPayment()
        {
            var paymentsCount = _data.Payments.Count();

            Assert.That(paymentsCount, Is.EqualTo(1));

            var model = new PayServiceViewModel
            {
                RentId = 1,
                PaymentAmount = 200.0,
                PaymentTypeId = 1
            };

            await _payService.Pay(model);

            paymentsCount = _data.Payments.Count();

            Assert.That(paymentsCount, Is.EqualTo(2));
        }

        [OneTimeTearDown]
        public void TearDown()
            => _data.Dispose();
    }
}
