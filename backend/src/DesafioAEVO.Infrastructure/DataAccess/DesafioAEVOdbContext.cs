using DesafioAEVO.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioAEVO.Infrastructure.DataAccess
{
    public class DesafioAEVOdbContext : DbContext
    {
        public DesafioAEVOdbContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioAEVOdbContext).Assembly);

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
