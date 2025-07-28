using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos;
// Dtos/StoreDto.cs
namespace WebApplication1.Dtos
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }

        // EKLEDİK: Store içindeki ürünlerin listesi
        public List<ProductResponseDto>? Products { get; set; }
    }
}
