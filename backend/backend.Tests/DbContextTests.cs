using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;

namespace backend.Tests
{
    public class DbContextTests
    {
        private readonly PizzaDbContext _context;

        public DbContextTests()
        {
            _context = CreateDbContext();
            DataSeeding.SeedData(_context);
        }

        private PizzaDbContext CreateDbContext()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<PizzaDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new PizzaDbContext(options);
        }
        [Fact]
        public void SeedData_CorrectlySeedsPizzaSizes()
        {
            var pizzaSizesCount = _context.PizzaSizes.Count();
            Assert.Equal(3, pizzaSizesCount);
        }

        [Fact]
        public void SeedData_CorrectlySeedsPizzaToppings()
        {
            var pizzaToppingsCount = _context.PizzaToppings.Count();
            Assert.Equal(7, pizzaToppingsCount);
        }

        [Fact]
        public void SeedData_PizzaSizeMediumExists()
        {
            var pizzaSizeMedium = _context.PizzaSizes.SingleOrDefault(ps => ps.Name == "Medium" && ps.Price == 10.00);
            Assert.NotNull(pizzaSizeMedium);
        }

        [Fact]
        public void SeedData_PizzaToppingSpinachExists()
        {
            var pizzaToppingSpinach = _context.PizzaToppings.SingleOrDefault(pt => pt.Name == "Spinach" && pt.Price == 1.00);
            Assert.NotNull(pizzaToppingSpinach);
        }

        [Fact]
        public void SeedData_PizzaToppingMozzarellaDoesNotExist()
        {
            var pizzaTopppingNotExisting = _context.PizzaToppings.SingleOrDefault(pt => pt.Name == "Mozzarella" && pt.Price == 1.00);
            Assert.Null(pizzaTopppingNotExisting);
        }
    }
}
