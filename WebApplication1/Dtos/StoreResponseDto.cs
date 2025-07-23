using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos;

namespace WebApplication1.Models
{
    public class StoreResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<ProductResponseDto>? Products { get; set; }

    }
}

