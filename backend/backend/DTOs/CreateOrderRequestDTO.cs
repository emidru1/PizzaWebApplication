namespace backend.DTOs
{
    public class CreateOrderRequestDTO
    {
        public string SizeName { get; set; }
        public List<string> ToppingNames { get; set; }
        public CreateOrderRequestDTO() { } 
        public CreateOrderRequestDTO(string sizeName, List<string> toppingNames)
        {
            this.SizeName = sizeName;
            this.ToppingNames = toppingNames;
        }
    }

}
