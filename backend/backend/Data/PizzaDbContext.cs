using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<PizzaSize> PizzaSizes { get; set; }
        public DbSet<PizzaTopping> PizzaToppings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTopping> OrderToppings { get; set; }
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("PizzaDatabase");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderTopping>()
                .HasKey(op => new { op.OrderId, op.PizzaToppingId });

            modelBuilder.Entity<OrderTopping>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderToppings)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderTopping>()
                .HasOne(op => op.PizzaTopping)
                .WithMany(p => p.OrderToppings)
                .HasForeignKey(op => op.PizzaToppingId);
        }



    }
}

