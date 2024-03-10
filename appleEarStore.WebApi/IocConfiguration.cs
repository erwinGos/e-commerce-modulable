using Database;
using Data.Repository;
using Data.Repository.Contract;
using Data.Services;
using Data.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Data.Managers;
using AutoMapper;

namespace appleEarStore.WebApi
{
    public static class IocConfiguration
    {

        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPromoRepository, PromoRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

            return services;
        }


        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IProductService, Data.Services.ProductService>();
            services.AddScoped<IOrderService, OrderService>();  
            services.AddScoped<IPromoService, PromoService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IVoucherService, VoucherService>();

            //Order Manager
            services.AddScoped<OrderManager>();


            //Stripe
            services.AddScoped<Stripe.ProductService>();
            services.AddScoped<Stripe.PriceService>();
            services.AddScoped<Stripe.Checkout.SessionService>();
            services.AddScoped<Stripe.CustomerService>();
            services.AddScoped<IStripeService, StripeService>();



            //Configuration 
            services.AddSingleton(configuration);


            //Mapper
            services.AddScoped(cfg => new MapperConfiguration(cfg => cfg.AddProfile<Data.MapperConfiguration>()));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<MapperConfiguration>(), sp.GetService));
            return services;
        }

        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BddConnection");
            services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
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

        public static IServiceCollection ConfigureDBContextTest(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            return services;
        }

    }
}
