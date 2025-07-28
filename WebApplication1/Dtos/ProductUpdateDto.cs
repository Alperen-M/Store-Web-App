namespace WebApplication1.Models
{
    public class ProductUpdateDto
    {
<<<<<<< Updated upstream
        
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
=======
>>>>>>> Stashed changes
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        
    }
}
