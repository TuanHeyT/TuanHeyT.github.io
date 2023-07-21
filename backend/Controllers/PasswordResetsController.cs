using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PasswordResetsController : ControllerBase {
    private readonly Example07Context _context;

    public PasswordResetsController(Example07Context context) {
        _context = context;
    }

    // GET: api/PasswordResets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PasswordReset>>> GetPasswordResets() {
        if(_context.PasswordResets == null) {
            return NotFound();
        }
        return await _context.PasswordResets.ToListAsync();
    }

    // GET: api/PasswordResets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PasswordReset>> GetPasswordReset(long id) {
        if(_context.PasswordResets == null) {
            return NotFound();
        }
        var passwordReset = await _context.PasswordResets.FindAsync(id);
        if(passwordReset == null) {
            return NotFound();
        }
        return passwordReset;
    }

    // PUT: api/PasswordResets/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPasswordReset(long id, PasswordReset passwordReset) {
        if(id != passwordReset.Id) {
            return BadRequest();
        }
        _context.Entry(passwordReset).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!PasswordResetExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/PasswordResets
    [HttpPost]
    public async Task<ActionResult<PasswordReset>> PostPasswordReset(PasswordReset passwordReset) {
        if(_context.PasswordResets == null) {
            return Problem("Entity set 'Example07Context.PasswordResets' is null.");
        }
        _context.PasswordResets.Add(passwordReset);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetPasswordReset", new { id = passwordReset.Id }, passwordReset);
    }

    // DELETE: api/PasswordResets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePasswordReset(long id) {
        if(_context.PasswordResets == null) {
            return NotFound();
        }
        var passwordReset = await _context.PasswordResets.FindAsync(id);
        if(passwordReset == null) {
            return NotFound();
        }
        _context.PasswordResets.Remove(passwordReset);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool PasswordResetExists(long id) {
        return (_context.PasswordResets?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
