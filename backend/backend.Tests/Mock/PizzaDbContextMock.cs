using backend.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using backend.Models;

namespace backend.Tests.Mock
{
    public static class PizzaDbContextMock
    {
        public static Mock<PizzaDbContext> GetDatabaseMock()
        {
            var dbMock = new Mock<PizzaDbContext>();

            dbMock.Setup(x => x.PizzaSizes).ReturnsDbSet(MockData.GetPizzaSizes());
            dbMock.Setup(x => x.PizzaToppings).ReturnsDbSet(MockData.GetPizzaToppings());


            dbMock.Setup(x => x.Orders).ReturnsDbSet(MockData.GetOrders());
            dbMock.Setup(x => x.OrderToppings).ReturnsDbSet(GetOrderToppings());

            return dbMock;
        }

        private static List<OrderTopping> GetOrderToppings()
        {
            // This method aggregates all OrderToppings from the orders for DbSet mocking.
            return MockData.GetOrders().SelectMany(o => o.OrderToppings).ToList();
        }
    }

}
