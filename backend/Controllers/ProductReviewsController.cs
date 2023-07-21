using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductReviewsController : ControllerBase {
    private readonly Example07Context _context;

    public ProductReviewsController(Example07Context context) {
        _context = context;
    }

    // GET: api/ProductReviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReview>>> GetProductReviews() {
        if(_context.ProductReviews == null) {
            return NotFound();
        }
        return await _context.ProductReviews.ToListAsync();
    }

    // GET: api/ProductReviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReview>> GetProductReview(long id) {
        if(_context.ProductReviews == null) {
            return NotFound();
        }
        var productReview = await _context.ProductReviews.FindAsync(id);
        if(productReview == null) {
            return NotFound();
        }
        return productReview;
    }

    // PUT: api/ProductReviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductReview(long id, ProductReview productReview) {
        if(id != productReview.Id) {
            return BadRequest();
        }
        _context.Entry(productReview).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!ProductReviewExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/ProductReviews
    [HttpPost]
    public async Task<ActionResult<ProductReview>> PostProductReview(ProductReview productReview) {
        if(_context.ProductReviews == null) {
            return Problem("Entity set 'Example07Context.ProductReviews' is null.");
        }
        _context.ProductReviews.Add(productReview);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetProductReview", new { id = productReview.Id }, productReview);
    }

    // DELETE: api/ProductReviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductReview(long id) {
        if(_context.ProductReviews == null) {
            return NotFound();
        }
        var productReview = await _context.ProductReviews.FindAsync(id);
        if(productReview == null) {
            return NotFound();
        }
        _context.ProductReviews.Remove(productReview);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductReviewExists(long id) {
        return (_context.ProductReviews?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
