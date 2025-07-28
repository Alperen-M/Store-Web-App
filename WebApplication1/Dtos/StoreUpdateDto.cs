namespace WebApplication1.Dtos
{
    public class StoreUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public string? Description { get; set; }
    }
}
