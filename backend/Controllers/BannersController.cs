using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BannersController : ControllerBase {
    private readonly Example07Context _context;

    public BannersController(Example07Context context) {
        _context = context;
    }

    // GET: api/Banners
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Banner>>> GetBanners() {
        if(_context.Banners == null) {
            return NotFound();
        }
        return await _context.Banners.ToListAsync();
    }

    // GET: api/Banners/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Banner>> GetBanner(long id) {
        if(_context.Banners == null) {
            return NotFound();
        }
        var banner = await _context.Banners.FindAsync(id);
        if(banner == null) {
            return NotFound();
        }
        return banner;
    }

    // PUT: api/Banners/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBanner(long id, Banner banner) {
        if(id != banner.Id) {
            return BadRequest();
        }
        _context.Entry(banner).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!BannerExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Banners
    [HttpPost]
    public async Task<ActionResult<Banner>> PostBanner(Banner banner) {
        if(_context.Banners == null) {
            return Problem("Entity set 'Example07Context.Banners' is null.");
        }
        _context.Banners.Add(banner);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetBanner", new { id = banner.Id }, banner);
    }

    // DELETE: api/Banners/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBanner(long id) {
        if(_context.Banners == null) {
            return NotFound();
        }
        var banner = await _context.Banners.FindAsync(id);
        if(banner == null) {
            return NotFound();
        }
        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool BannerExists(long id) {
        return (_context.Banners?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
