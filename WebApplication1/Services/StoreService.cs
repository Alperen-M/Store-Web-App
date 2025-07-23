using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication1.Dtos;
using WebApplication1.EntityFrameworkCore;
using WebApplication1.Models;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _repo;
    private readonly AppDbContext _context;

    public StoreService(IStoreRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<List<StoreResponseDto>> GetAllAsync()
    {
        var stores = await _repo.GetAllAsync();
        return stores.Select(s => new StoreResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        }).ToList();
    }

    public async Task<StoreResponseDto?> GetByIdAsync(int id)
    {
        var s = await _repo.GetByIdAsync(id);
        if (s == null) return null;

        return new StoreResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        };
    }

    public async Task<StoreResponseDto> CreateAsync(StoreCreateDto dto)
    {
        var store = new Store
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _repo.CreateAsync(store);

        return new StoreResponseDto
        {
            Id = store.Id,
            Name = store.Name,
            Description = store.Description
        };
    }

    public async Task<bool> UpdateAsync(int id, StoreUpdateDto dto)
    {
        var store = await _repo.GetByIdAsync(id);
        if (store == null) return false;

        store.Name = dto.Name;
        store.Description = dto.Description;

        await _repo.UpdateAsync(store);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var store = await _repo.GetByIdAsync(id);
        if (store == null) return false;

        await _repo.DeleteAsync(store);
        return true;
    }
}
