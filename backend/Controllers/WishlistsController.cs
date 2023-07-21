using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistsController : ControllerBase {
    private readonly Example07Context _context;

    public WishlistsController(Example07Context context) {
        _context = context;
    }

    // GET: api/Wishlists
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlists() {
        if(_context.Wishlists == null) {
            return NotFound();
        }
        return await _context.Wishlists.ToListAsync();
    }

    // GET: api/Wishlists/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Wishlist>> GetWishlist(long id) {
        if(_context.Wishlists == null) {
            return NotFound();
        }
        var wishlist = await _context.Wishlists.FindAsync(id);
        if(wishlist == null) {
            return NotFound();
        }
        return wishlist;
    }

    // PUT: api/Wishlists/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWishlist(long id, Wishlist wishlist) {
        if(id != wishlist.Id) {
            return BadRequest();
        }
        _context.Entry(wishlist).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!WishlistExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Wishlists
    [HttpPost]
    public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist wishlist) {
        if(_context.Wishlists == null) {
            return Problem("Entity set 'Example07Context.Wishlists' is null.");
        }
        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetWishlist", new { id = wishlist.Id }, wishlist);
    }

    // DELETE: api/Wishlists/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWishlist(long id) {
        if(_context.Wishlists == null) {
            return NotFound();
        }
        var wishlist = await _context.Wishlists.FindAsync(id);
        if(wishlist == null) {
            return NotFound();
        }
        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool WishlistExists(long id) {
        return (_context.Wishlists?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
