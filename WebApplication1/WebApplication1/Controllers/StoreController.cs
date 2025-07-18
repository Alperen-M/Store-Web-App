using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StoreController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreResponseDto>>> GetAllStores()
        {
            var stores = await _context.Stores.Include(s => s.Products).ToListAsync();

            var storeDtos = stores.Select(s => new StoreResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                Description = s.Description,
                Products = s.Products.Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            });

            return Ok(storeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreResponseDto>> GetStoreById(int id)
        {
            var store = await _context.Stores.Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (store == null)
                return NotFound();

            var storeDto = new StoreResponseDto
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location,
                Description = store.Description,
                Products = store.Products.Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            };

            return Ok(storeDto);
        }

        [HttpPost]
        public async Task<ActionResult<StoreResponseDto>> CreateStore([FromBody] StoreCreateDto storeDto)
        {
            var store = new Stores
            {
                Name = storeDto.Name,
                Location = storeDto.Location,
                Description = storeDto.Description,
                Products = storeDto.Products.Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            var response = new StoreResponseDto
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location,
                Description = store.Description,
                Products = store.Products.Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            };

            return CreatedAtAction(nameof(GetStoreById), new { id = store.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] StoreCreateDto updatedDto)
        {
            var store = await _context.Stores.Include(s => s.Products).FirstOrDefaultAsync(s => s.Id == id);
            if (store == null)
                return NotFound();

            store.Name = updatedDto.Name;
            store.Location = updatedDto.Location;
            store.Description = updatedDto.Description;

            store.Products.Clear();
            foreach (var p in updatedDto.Products)
            {
                store.Products.Add(new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Stores.Include(s => s.Products).FirstOrDefaultAsync(s => s.Id == id);
            if (store == null)
                return NotFound();

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
