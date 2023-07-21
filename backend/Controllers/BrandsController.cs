using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase {
    private readonly Example07Context _context;

    public BrandsController(Example07Context context) {
        _context = context;
    }

    // GET: api/Brands
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrands() {
        if(_context.Brands == null) {
            return NotFound();
        }
        return await _context.Brands.ToListAsync();
    }

    // GET: api/Brands/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(long id) {
        if(_context.Brands == null) {
            return NotFound();
        }
        var brand = await _context.Brands.FindAsync(id);
        if(brand == null) {
            return NotFound();
        }
        return brand;
    }

    // PUT: api/Brands/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBrand(long id, Brand brand) {
        if(id != brand.Id) {
            return BadRequest();
        }
        _context.Entry(brand).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!BrandExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Brands
    [HttpPost]
    public async Task<ActionResult<Brand>> PostBrand(Brand brand) {
        if(_context.Brands == null) {
            return Problem("Entity set 'Example07Context.Brands' is null.");
        }
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
    }

    // DELETE: api/Brands/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(long id) {
        if(_context.Brands == null) {
            return NotFound();
        }
        var brand = await _context.Brands.FindAsync(id);
        if(brand == null) {
            return NotFound();
        }
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool BrandExists(long id) {
        return (_context.Brands?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
