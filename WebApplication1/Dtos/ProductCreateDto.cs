namespace WebApplication1.Models
{
    public class ProductCreateDto
    {
<<<<<<< Updated upstream
        
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$", ErrorMessage = "Ürün adı sadece harf ve boşluk içerebilir.")]
=======
>>>>>>> Stashed changes
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        
    }
}
