using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponsController : ControllerBase {
    private readonly Example07Context _context;

    public CouponsController(Example07Context context) {
        _context = context;
    }

    // GET: api/Coupons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons() {
        if(_context.Coupons == null) {
            return NotFound();
        }
        return await _context.Coupons.ToListAsync();
    }

    // GET: api/Coupons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Coupon>> GetCoupon(long id) {
        if(_context.Coupons == null) {
            return NotFound();
        }
        var coupon = await _context.Coupons.FindAsync(id);
        if(coupon == null) {
            return NotFound();
        }
        return coupon;
    }

    // PUT: api/Coupons/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCoupon(long id, Coupon coupon) {
        if(id != coupon.Id) {
            return BadRequest();
        }
        _context.Entry(coupon).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!CouponExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Coupons
    [HttpPost]
    public async Task<ActionResult<Coupon>> PostCoupon(Coupon coupon) {
        if(_context.Coupons == null) {
            return Problem("Entity set 'Example07Context.Coupons' is null.");
        }
        _context.Coupons.Add(coupon);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetCoupon", new { id = coupon.Id }, coupon);
    }

    // DELETE: api/Coupons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupon(long id) {
        if(_context.Coupons == null) {
            return NotFound();
        }
        var coupon = await _context.Coupons.FindAsync(id);
        if(coupon == null) {
            return NotFound();
        }
        _context.Coupons.Remove(coupon);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool CouponExists(long id) {
        return (_context.Coupons?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
