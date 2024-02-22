using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Reflection.Emit;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            builder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<Color>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.Hex).IsUnique();
            });
            
            builder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderNumber).IsUnique();
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Ean).IsUnique();
                entity.HasIndex(e => e.ProductName).IsUnique();
            });

            builder.Entity<PromoCode>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });

            builder.Entity<Vouchers>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetColumnType("decimal(8, 5)");
                    }
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=appleearstore;port=3306;User=root;", new MySqlServerVersion(new Version(8, 0, 21)));
    }
}
