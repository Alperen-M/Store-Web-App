using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFrameworkCore;
using WebApplication1.Models;

public class StoreRepository : IStoreRepository
{
    private readonly AppDbContext _context;

    public StoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Store>> GetAllAsync()
    {
        return await _context.Stores.ToListAsync();
    }

    public async Task<Store?> GetByIdAsync(int id)
    {
        return await _context.Stores.FindAsync(id);
    }

    public async Task CreateAsync(Store store)
    {
        _context.Stores.Add(store);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Store store)
    {
        _context.Stores.Update(store);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Store store)
    {
        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();
    }
}
