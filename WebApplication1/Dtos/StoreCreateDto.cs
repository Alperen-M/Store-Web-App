using WebApplication1.Models;
using System.Collections.Generic;

public class StoreCreateDto
{
<<<<<<< Updated upstream
    
    [Required(ErrorMessage = "Mağaza adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Mağaza adı en fazla 100 karakter olabilir.")]
    [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$", ErrorMessage = "Mağaza adı sadece harf ve boşluk içerebilir.")]
=======
>>>>>>> Stashed changes
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<ProductCreateDto> Products { get; set; } = new();
    
}
