using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingsController : ControllerBase {
    private readonly Example07Context _context;

    public ShippingsController(Example07Context context) {
        _context = context;
    }

    // GET: api/Shippings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shipping>>> GetShippings() {
        if(_context.Shippings == null) {
            return NotFound();
        }
        return await _context.Shippings.ToListAsync();
    }

    // GET: api/Shippings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Shipping>> GetShipping(long id) {
        if(_context.Shippings == null) {
            return NotFound();
        }
        var shipping = await _context.Shippings.FindAsync(id);
        if(shipping == null) {
            return NotFound();
        }
        return shipping;
    }

    // PUT: api/Shippings/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShipping(long id, Shipping shipping) {
        if(id != shipping.Id) {
            return BadRequest();
        }
        _context.Entry(shipping).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!ShippingExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Shippings
    [HttpPost]
    public async Task<ActionResult<Shipping>> PostShipping(Shipping shipping) {
        if(_context.Shippings == null) {
            return Problem("Entity set 'Example07Context.Shippings' is null.");
        }
        _context.Shippings.Add(shipping);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetShipping", new { id = shipping.Id }, shipping);
    }

    // DELETE: api/Shippings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipping(long id) {
        if(_context.Shippings == null) {
            return NotFound();
        }
        var shipping = await _context.Shippings.FindAsync(id);
        if(shipping == null) {
            return NotFound();
        }
        _context.Shippings.Remove(shipping);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ShippingExists(long id) {
        return (_context.Shippings?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
