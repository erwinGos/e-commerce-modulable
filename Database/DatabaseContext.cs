using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class DatabaseContext : DbContext
    {

        public DatabaseContext()
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Color> Colors { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductOrder> ProductOrder { get; set; }

        public virtual DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseMySQL("server=localhost;database=appleearstore;port=3306;User=root;");
    }
}
