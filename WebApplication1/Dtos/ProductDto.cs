using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplication1.Dtos
{
    public class ProductDto
    {
        
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        
    }
}

