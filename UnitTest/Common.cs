using Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using appleEarStore.WebApi;


namespace userMicroService.Tests.Common
{
    public class TestBase
    {
        protected ServiceProvider _serviceProvider;

        protected DatabaseContext _context;

        private void InitTestDatabase()
        {
            _context = _serviceProvider.GetService<DatabaseContext>();
            _context?.Database.EnsureDeleted();
            _context?.Database.EnsureCreated();
        }
        public void SetUpTest()
        {
            var configuration = new ConfigurationBuilder().Build();
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .ConfigureDBContextTest()
                .ConfigureInjectionDependencyRepositoryTest()
                .ConfigureInjectionDependencyServiceTest(configuration)
                .BuildServiceProvider();

            InitTestDatabase();
        }

        public void CleanTest()
        {
            _context?.Database.EnsureDeleted();
            _serviceProvider.Dispose();
            _context?.Dispose();
        }

    }
}