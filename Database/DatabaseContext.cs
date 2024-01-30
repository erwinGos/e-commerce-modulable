using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Database
{
    public partial class DatabaseContext : DbContext
    {
        public readonly IConfiguration configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> options,IConfiguration _configuration) : base(options)
        {
            configuration = _configuration;
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Brand> Brand { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Color> Colors { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductImage> ProductImage { get; set; }

        public virtual DbSet<ProductOrder> ProductOrder { get; set; }

        public virtual DbSet<PromoCode> PromoCode { get; set; }

        public virtual DbSet<Return> Return { get; set; }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<UserCart> UserCart { get; set; }

        public virtual DbSet<Order> Vouchers { get; set; }

        public virtual DbSet<Order> WebsiteSettings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseMySQL("server=localhost;database=appleearstore;port=3306;User=root;");
    }
}
