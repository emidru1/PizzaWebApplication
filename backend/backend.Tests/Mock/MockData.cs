using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Tests.Mock
{
    public static class MockData
    {
        public static List<PizzaSize> GetPizzaSizes() => new List<PizzaSize>
        {
            new PizzaSize(1, "Small", 8.00),
            new PizzaSize(2, "Medium", 10.00),
            new PizzaSize(3, "Large", 12.00),
        };
        public static List<PizzaTopping> GetPizzaToppings()
        {
            return new List<PizzaTopping>
            {
                new PizzaTopping(1, "Tomato sauce", 1.00),
                new PizzaTopping(2, "Pepperoni", 1.00),
                new PizzaTopping(3, "Cheese", 1.00),
                new PizzaTopping(4, "Spinach", 1.00),
                new PizzaTopping(5, "Chicken", 1.00),
                new PizzaTopping(6, "Olives", 1.00),
                new PizzaTopping(7, "Mushrooms", 1.00)
            };
        }
        public static List<Order> GetOrders()
        {
            var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                Size = new PizzaSize(3, "Large", 12.00),
                OrderToppings = new List<OrderTopping>
                {
                    new OrderTopping
                    {
                        OrderId = 1,
                        PizzaToppingId = 3,
                        Order = null, // You can set the Order reference here
                        PizzaTopping = new PizzaTopping { Id = 3, Name = "Cheese", Price = 1.00 }
                    }
                },
                OrderTotal = 13.00
                },
                new Order
                {
                    Id = 2,
                    Size = new PizzaSize(3, "Large", 12.00),
                    OrderToppings = new List<OrderTopping>
                    {
                        new OrderTopping
                        {
                            OrderId = 2,
                            PizzaToppingId = 2,
                            Order = null, // You can set the Order reference here
                            PizzaTopping = new PizzaTopping { Id = 2, Name = "Pepperoni", Price = 1.00 }
                        },
                        new OrderTopping
                        {
                            OrderId = 2,
                            PizzaToppingId = 3,
                            Order = null, // You can set the Order reference here
                            PizzaTopping = new PizzaTopping { Id = 3, Name = "Cheese", Price = 1.00 }
                        }
                    },
                    OrderTotal = 14.00
                }
            };

            // Set the Order reference for each OrderTopping
            foreach (var order in orders)
            {
                foreach (var orderTopping in order.OrderToppings)
                {
                    orderTopping.Order = order;
                }
            }

            return orders;
        }
    }

}
