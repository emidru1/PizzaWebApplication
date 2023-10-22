namespace backend.Models
{
    public class OrderTopping
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PizzaToppingId { get; set; }
        public PizzaTopping PizzaTopping { get; set; }

        public OrderTopping()
        {

        }
        public OrderTopping(int orderId, Order order, int pizzaToppingId, PizzaTopping pizzaTopping)
        {
            OrderId = orderId;
            Order = order;
            PizzaToppingId = pizzaToppingId;
            PizzaTopping = pizzaTopping;
        }
    }
}
