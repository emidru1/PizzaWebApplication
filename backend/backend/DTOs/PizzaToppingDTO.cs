namespace backend.DTOs
{
    public class PizzaToppingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public PizzaToppingDTO() 
        { 

        }
        public PizzaToppingDTO(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}