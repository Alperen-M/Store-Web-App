using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }

        public List<Product>? Products { get; set; }
    }
}