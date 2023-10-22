using backend.Models;

namespace backend.Data
{
    public class DataSeeding
    {
        /// <summary>
        /// Seed Entity Framework In-Memory Database with default data upon running the application
        /// </summary>
        /// <param name="context">In-Memory database context</param>
        public static void SeedData(PizzaDbContext context)
        {
            context.PizzaSizes.AddRange(
                new PizzaSize(1, "Small", 8.00),
                new PizzaSize(2, "Medium", 10.00),
                new PizzaSize(3, "Large", 12.00)
            );
            context.PizzaToppings.AddRange(
                new PizzaTopping(1, "Tomato sauce", 1.00),
                new PizzaTopping(2, "Pepperoni", 1.00),
                new PizzaTopping(3, "Cheese", 1.00),
                new PizzaTopping(4, "Spinach", 1.00),
                new PizzaTopping(5, "Chicken", 1.00),
                new PizzaTopping(6, "Olives", 1.00),
                new PizzaTopping(7, "Mushrooms", 1.00)

            );
            context.SaveChanges();
        }
    }
}
