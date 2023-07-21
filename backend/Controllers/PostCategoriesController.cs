using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostCategoriesController : ControllerBase {
    private readonly Example07Context _context;

    public PostCategoriesController(Example07Context context) {
        _context = context;
    }

    // GET: api/PostCategories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostCategory>>> GetPostCategories() {
        if(_context.PostCategories == null) {
            return NotFound();
        }
        return await _context.PostCategories.ToListAsync();
    }

    // GET: api/PostCategories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PostCategory>> GetPostCategory(long id) {
        if(_context.PostCategories == null) {
            return NotFound();
        }
        var postCategory = await _context.PostCategories.FindAsync(id);
        if(postCategory == null) {
            return NotFound();
        }
        return postCategory;
    }

    // PUT: api/PostCategories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPostCategory(long id, PostCategory postCategory) {
        if(id != postCategory.Id) {
            return BadRequest();
        }
        _context.Entry(postCategory).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!PostCategoryExists(id)) {
                return NotFound();
            } else throw;
        }
        return NoContent();
    }
    
    // POST: api/PostCategories
    [HttpPost]
    public async Task<ActionResult<PostCategory>> PostPostCategory(PostCategory postCategory) {
        if(_context.PostCategories == null) {
            return Problem("Entity set 'Example07Context.PostCategories' is null.");
        }
        _context.PostCategories.Add(postCategory);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetPostCategory", new { id = postCategory.Id }, postCategory);
    }

    // DELETE: api/PostCategories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostCategory(long id) {
        if(_context.PostCategories == null) {
            return NotFound();
        }
        var postCategory = await _context.PostCategories.FindAsync(id);
        if(postCategory == null) {
            return NotFound();
        }
        _context.PostCategories.Remove(postCategory);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool PostCategoryExists(long id) {
        return (_context.PostCategories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
