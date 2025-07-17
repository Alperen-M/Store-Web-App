using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat pozitif olmalıdır.")]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public int StoreId { get; set; }
    }
}
