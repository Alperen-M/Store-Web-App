using System.Collections.Generic;
using WebApplication1.Dtos;

namespace WebApplication1.Dtos
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int StoreId { get; set; }  // isteğe bağlı
    }
}



