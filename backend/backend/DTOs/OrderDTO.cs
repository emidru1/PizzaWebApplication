using System.Collections.Generic;

namespace backend.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public PizzaSizeDTO Size { get; set; }
        public List<PizzaToppingDTO> Toppings { get; set; }
        public double OrderTotal { get; set; }
        public OrderDTO()
        {
            Toppings = new List<PizzaToppingDTO>();
        }
        public OrderDTO(int id, PizzaSizeDTO size, List<PizzaToppingDTO> toppings, double orderTotal)
        {
            Id = id;
            Size = size;
            Toppings = toppings;
            OrderTotal = orderTotal;
        }
    }
}