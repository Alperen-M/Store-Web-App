using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;

// Entity'leri alias ile kullan (ambiguous riskini azaltır)
using ProductEntity = WebApplication1.Models.Product;
using StoreEntity = WebApplication1.Models.Store;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // --------------------------------------------------------------------
        // GET: api/product
        // --------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Store)
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
             
                })
                .ToListAsync();

            return Ok(products);
        }

        // --------------------------------------------------------------------
        // GET: api/product/{id}
        // --------------------------------------------------------------------
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Store)
                .Where(p => p.Id == id)
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                  
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // --------------------------------------------------------------------
        // POST: api/product
        // StoreId Swagger'da yok; otomatik atanacak.
        // --------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // En az bir Store var mı?
            var firstStore = await _context.Store.FirstOrDefaultAsync();
            if (firstStore == null)
            {
                return BadRequest("Önce en az bir mağaza (Store) oluşturmalısınız; ürün atanacak mağaza bulunamadı.");
            }

            var product = new ProductEntity
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                StoreId = firstStore.Id
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Response DTO
            var response = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                
            };

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
        }

        // --------------------------------------------------------------------
        // PUT: api/product/{id}
        // Store değişmez; sadece temel alanlar güncellenir.
        // --------------------------------------------------------------------
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errors });
            }


            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // --------------------------------------------------------------------
        // DELETE: api/product/{id}
        // --------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

