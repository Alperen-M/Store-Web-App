using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
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
        // GET: api/store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetAllStores()
        {
            var stores = await _context.Stores.Include(s => s.Products).ToListAsync();

            var storeDtos = stores.Select(s => new StoreDto
            {
               
                Name = s.Name,
                Location = s.Location,
                Description = s.Description,
                Products = s.Products.Select(p => new ProductDto
                {
                  
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            });

            return Ok(storeDtos);
        }

        // GET: api/store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStoreById(int id)
        {
            var store = await _context.Stores.Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (store == null)
                return NotFound();

            var storeDto = new StoreDto
            {
              
                Name = store.Name,
                Location = store.Location,
                Description = store.Description,
                Products = store.Products.Select(p => new ProductDto
                {
                   
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            };

            return Ok(storeDto);
        }

        // POST: api/store
        [HttpPost]
        public async Task<ActionResult<StoreDto>> CreateStore([FromBody] StoreDto storeDto)
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

            // Response DTO
            
            return CreatedAtAction(nameof(GetStoreById), new { id = store.Id }, storeDto);
        }

        // PUT: api/store/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] StoreDto updatedDto)
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

        // DELETE: api/store/5
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
