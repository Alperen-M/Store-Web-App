/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var result = await _context.Products.ToListAsync();
            Console.WriteLine(result);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.Include(p => p.Store).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            // StoreId kontrolü
            if (product.StoreId != null && !_context.Stores.Any(s => s.Id == product.StoreId))
            {
                return BadRequest("Geçerli bir StoreId giriniz.");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product updated)
        {
            if (id != updated.Id) return BadRequest();

            if (updated.StoreId != null && !_context.Stores.Any(s => s.Id == updated.StoreId))
            {
                return BadRequest("Geçerli bir StoreId giriniz.");
            }

            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = updated.Name;
            existing.Price = updated.Price;
            existing.Description = updated.Description;
            existing.StoreId = updated.StoreId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
*/