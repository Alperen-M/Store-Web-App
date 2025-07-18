using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApplication1.Models
{
    public class StoreCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<ProductCreateDto> Products { get; set; } = new();
    }
}

