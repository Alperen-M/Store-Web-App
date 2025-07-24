using WebApplication1.Models;

public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync();
    Task<Store?> GetByIdAsync(int id);
    Task CreateAsync(Store store);
    Task UpdateAsync(Store store);
    Task DeleteAsync(Store store);
}
