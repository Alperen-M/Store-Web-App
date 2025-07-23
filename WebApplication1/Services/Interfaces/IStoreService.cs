using WebApplication1.Dtos;
using WebApplication1.Models;

public interface IStoreService
{
    Task<List<StoreResponseDto>> GetAllAsync();
    Task<StoreResponseDto?> GetByIdAsync(int id);
    Task<StoreResponseDto> CreateAsync(StoreCreateDto dto);
    Task<bool> UpdateAsync(int id, StoreUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
