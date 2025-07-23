using WebApplication1.Dtos;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFrameworkCore;
using WebApplication1.Models;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly AppDbContext _context;

    public ProductService(IProductRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description,
            StoreId = p.StoreId
        }).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return null;

        return new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description,
            StoreId = p.StoreId
        };
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var store = await _context.Stores.FirstOrDefaultAsync();
        if (store == null)
            throw new Exception("Mağaza bulunamadı.");

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description,
            StoreId = store.Id
        };

        await _repo.CreateAsync(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            StoreId = product.StoreId
        };
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return false;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Description = dto.Description;

        await _repo.UpdateAsync(product);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return false;

        await _repo.DeleteAsync(product);
        return true;
    }
}
