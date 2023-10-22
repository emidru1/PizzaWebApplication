using System.Collections.Generic;

namespace backend.Models
{
    public class PizzaTopping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<OrderTopping> OrderToppings { get; } = new();
        public PizzaTopping()
        {

        }
        public PizzaTopping(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
