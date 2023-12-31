using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase {
    private readonly Example07Context _context;

    public OrdersController(Example07Context context) {
        _context = context;
    }

    // GET: api/Orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders() {
        if(_context.Orders == null) {
            return NotFound();
        }
        return await _context.Orders.ToListAsync();
    }

    // GET: api/Orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(long id) {
        if(_context.Orders == null) {
            return NotFound();
        }
        var order = await _context.Orders.FindAsync(id);
        if(order == null) {
            return NotFound();
        }
        return order;
    }

    // PUT: api/Orders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(long id, Order order) {
        if(id != order.Id) {
            return BadRequest();
        }
        _context.Entry(order).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!OrderExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Orders
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order) {
        if(_context.Orders == null) {
            return Problem("Entity set 'Example07Context.Orders' is null.");
        }
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    // DELETE: api/Orders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id) {
        if(_context.Orders == null) {
            return NotFound();
        }
        var order = await _context.Orders.FindAsync(id);
        if(order == null) {
            return NotFound();
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool OrderExists(long id) {
        return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
