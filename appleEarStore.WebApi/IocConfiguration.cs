using Database;
using Data.Repository;
using Data.Repository.Contract;
using Data.Services;
using Data.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Data.Managers;

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

            return services;
        }


        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services)
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


            //Order Manager
            services.AddScoped<OrderManager>();


            //Stripe
            services.AddScoped<Stripe.ProductService>();
            services.AddScoped<Stripe.PriceService>();
            services.AddScoped<Stripe.Checkout.SessionService>();
            services.AddScoped<Stripe.CustomerService>();
            services.AddScoped<IStripeService, StripeService>();
            return services;
        }

        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BddConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

            services.AddDbContext<DatabaseContext>(options => options.UseMySql(serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

            return services;
        }

    }
}
