namespace backend.Models
{
    public class PizzaSize
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public PizzaSize(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
