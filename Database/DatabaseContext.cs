using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class DatabaseContext : DbContext
    {

        public DatabaseContext()
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseMySQL("server=localhost;database=appleearstore;port=3306;User=root;");
    }
}
