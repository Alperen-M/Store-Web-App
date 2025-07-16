namespace WebApplication1.Models
{
    public class StoreDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
