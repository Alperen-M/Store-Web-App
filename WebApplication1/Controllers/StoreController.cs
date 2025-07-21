using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Dtos;

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

        // Tüm mağazaları getir
        [HttpGet]
        [SwaggerOperation(Summary = "Tüm mağazaları getir", Description = "Veritabanındaki tüm mağazaların listesini döner.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetStores()
        {
            var stores = await _context.Store
                .Include(s => s.Products)
                .Select(s => new StoreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Location = s.Location,
                    Products = s.Products.Select(p => new ProductResponseDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }).ToList()
                }).ToListAsync();

            return Ok(stores);
        }

        // ID'ye göre mağaza getir
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "ID'ye göre mağaza getir", Description = "Verilen ID'ye sahip mağazayı döner.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StoreDto>> GetStore(int id)
        {
            var store = await _context.Store
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (store == null)
                return NotFound();

            var storeDto = new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                Location = store.Location,
                Products = store.Products.Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            };

            return Ok(storeDto);
        }

        // Yeni mağaza oluştur
        [HttpPost]
        [SwaggerOperation(Summary = "Yeni mağaza oluştur", Description = "Yeni bir mağaza kaydı oluşturur.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StoreDto>> CreateStore([FromBody] StoreDto storeDto)
        {
            var store = new Store
            {
                Name = storeDto.Name,
                Description = storeDto.Description,
                Location = storeDto.Location
            };

            _context.Store.Add(store);
            await _context.SaveChangesAsync();

            storeDto.Id = store.Id;

            return CreatedAtAction(nameof(GetStore), new { id = store.Id }, storeDto);
        }

        // Mağaza güncelle
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Mağaza güncelle", Description = "Belirli bir ID'ye sahip mağazayı günceller.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] StoreDto storeDto)
        {
            var store = await _context.Store.FindAsync(id);
            if (store == null)
                return NotFound();

            store.Name = storeDto.Name;
            store.Description = storeDto.Description;
            store.Location = storeDto.Location;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Mağaza sil
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Mağaza sil", Description = "Belirli bir ID'ye sahip mağazayı siler.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Store.FindAsync(id);
            if (store == null)
                return NotFound();

            _context.Store.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
