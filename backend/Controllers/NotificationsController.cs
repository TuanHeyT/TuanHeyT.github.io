using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase {
    private readonly Example07Context _context;

    public NotificationsController(Example07Context context) {
        _context = context;
    }

    // GET: api/Notifications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications() {
        if(_context.Notifications == null) {
            return NotFound();
        }
        return await _context.Notifications.ToListAsync();
    }

    // GET: api/Notifications/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> GetNotification(string id) {
        if(_context.Notifications == null) {
            return NotFound();
        }
        var notification = await _context.Notifications.FindAsync(id);
        if(notification == null) {
            return NotFound();
        }
        return notification;
    }

    // PUT: api/Notifications/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNotification(string id, Notification notification) {
        if(!id.Equals(notification.Id)) {
            return BadRequest();
        }
        _context.Entry(notification).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!NotificationExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/Notifications
    [HttpPost]
    public async Task<ActionResult<Notification>> PostNotification(Notification notification) {
        if(_context.Notifications == null) {
            return Problem("Entity set 'Example07Context.Notifications' is null.");
        }
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
    }

    // DELETE: api/Notifications/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(string id) {
        if(_context.Notifications == null) {
            return NotFound();
        }
        var notification = await _context.Notifications.FindAsync(id);
        if(notification == null) {
            return NotFound();
        }
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool NotificationExists(string id) {
        return (_context.Notifications?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
    }
}
