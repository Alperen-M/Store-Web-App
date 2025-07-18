using WebApplication1.Models;
using System.Collections.Generic;
namespace WebApplication1.Models
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public StoreDto? Store { get; set; }
    }
}
