using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace appleEarStore.WebApi
{
    public static class IocConfiguration
    {

        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {
            return services;
        }


        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BddConnection");

            services.AddDbContext<DatabaseContext>(options => options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

            return services;
        }

    }

    public static class IocConfigurationTest
    {
        public static IServiceCollection ConfigureInjectionDependencyRepositoryTest(this IServiceCollection services)
        {
            services.ConfigureInjectionDependencyRepository();

            return services;

        }

        public static IServiceCollection ConfigureInjectionDependencyServiceTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureInjectionDependencyService(configuration);

            return services;
        }

    }
}
