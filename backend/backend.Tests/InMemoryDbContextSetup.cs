using backend.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class InMemoryDbContextSetup
    {
            public PizzaDbContext CreateNewContext()
            {
                var options = new DbContextOptionsBuilder<PizzaDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                var context = new PizzaDbContext(options);
                context.Database.EnsureCreated();
                DataSeeding.SeedData(context);
                return context;
            }
    }
}
