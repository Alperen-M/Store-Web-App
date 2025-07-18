using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplication1.Dtos
{
    public class StoreResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
    }
}
