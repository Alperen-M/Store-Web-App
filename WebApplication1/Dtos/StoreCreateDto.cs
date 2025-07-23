using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

public class StoreCreateDto
{
    [Required(ErrorMessage = "Mağaza adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Mağaza adı en fazla 100 karakter olabilir.")]
    [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$", ErrorMessage = "Mağaza adı sadece harf ve boşluk içerebilir.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Konum bilgisi zorunludur.")]
    [StringLength(100, ErrorMessage = "Konum en fazla 100 karakter olabilir.")]
    public string Location { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
    public string? Description { get; set; }

    public List<ProductCreateDto> Products { get; set; } = new();
}
