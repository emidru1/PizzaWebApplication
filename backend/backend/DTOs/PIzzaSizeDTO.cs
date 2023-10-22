namespace backend.DTOs
{
    public class PizzaSizeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public PizzaSizeDTO() 
        {
        
        }
        public PizzaSizeDTO(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}