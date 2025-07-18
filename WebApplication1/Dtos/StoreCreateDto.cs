using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApplication1.Models
{
    public class StoreCreateDto
    {
        [Required(ErrorMessage = "Mağaza adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Mağaza adı en fazla 100 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konum zorunludur.")]
        [StringLength(200, ErrorMessage = "Konum en fazla 200 karakter olabilir.")]
        public string Location { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string? Description { get; set; }
    }
}
