using WebApplication1.Models;
using System.Collections.Generic;

public class StoreCreateDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Location { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public List<ProductCreateDto> Products { get; set; } = new();
}
