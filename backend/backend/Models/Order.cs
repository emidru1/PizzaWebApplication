using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public PizzaSize Size { get; set; }
        public List<OrderTopping> OrderToppings { get; set; } = new();
        public double OrderTotal { get; set; }

        public Order()
        {

        }
        public Order(int id, PizzaSize size, List<OrderTopping> orderToppings)
        {
            this.Id = id;
            this.Size = size;
            this.OrderToppings = orderToppings;
        }
    }
}
