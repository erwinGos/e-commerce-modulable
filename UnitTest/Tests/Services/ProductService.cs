using Data.Services.Contract;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Scenarios;
using userMicroService.Tests.Common;

namespace UnitTest.Tests.Services
{
    public class ProductService : TestBase
    {
        private IProductService _productService;

        [SetUp]
        public void Setup()
        {
            SetUpTest();
            _productService = _serviceProvider?.GetService<IProductService>();
            _context.CreateUser();
            _context.CreateUsers();
        }

        [TearDown]
        public void TearDown()
        {
            CleanTest();
        }
    }
}
