using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp_17_06_2022.Data.Entities;
using TestApp_17_06_2022.Data.Interfaces;
using TestApp_17_06_2022.Enums;

namespace TestApp_17_06_2022.Data.Classes
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Product>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Order>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).ValueGeneratedOnAdd();
                e.HasOne(s => s.Product).WithMany(s => s.Orders).HasForeignKey(s => s.ProductId);
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}