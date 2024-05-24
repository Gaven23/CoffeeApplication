using CoffeeApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoffeeApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        internal DbSet<Order> Order { get; set; }
        internal DbSet<Product> Product { get; set; }
        internal DbSet<Discount> Discount { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {

                entity.HasKey(e => e.OrderId);

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);
            });
        }
    }
}
