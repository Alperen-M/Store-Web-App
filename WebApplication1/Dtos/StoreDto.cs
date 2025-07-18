using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos;
namespace WebApplication1.Models
{
    public class StoreDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        public List<WebApplication1.Dtos.ProductResponseDto>? Products { get; set; }

    }
}
