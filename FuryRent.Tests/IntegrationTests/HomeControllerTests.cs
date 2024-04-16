using FuryRent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FuryRent.Tests.IntegrationTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController homeController;

        [OneTimeSetUp]
        public void Setup()
                        => homeController = new HomeController(null);

        [Test]

        public void Error_ShouldReturnCorrectView()
        {
            var statusCode = 500;

            var result = homeController.Error(statusCode);

            Assert.IsNotNull(result);

            var viewResult = result as ViewResult; 
            Assert.IsNotNull(viewResult);

            statusCode = 404;

            result = homeController.Error(statusCode);

            Assert.IsNotNull(result);

            viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
