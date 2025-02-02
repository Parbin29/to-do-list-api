using Microsoft.AspNetCore.Mvc;
using to_do_list_api.Data;
using to_do_list_api.Models;

using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Threading.Tasks;

namespace to_do_list_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TagController(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // 2. GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var Tag = await _context.Tags.FindAsync(id);

            if (Tag == null)
            {
                return NotFound();
            }

            return Tag;
        }

        // 3. POST: api/Tag
        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag(Tag Tag)
        {
            _context.Tags.Add(Tag);
            await _context.SaveChangesAsync();

            // Return the created Tag with a 201 status and location header
            return CreatedAtAction(nameof(GetTag), new { id = Tag.Id }, Tag);
        }

        // 4. PUT: api/Tag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, Tag Tag) // public async Task<IActionResult> UpdateTag(int id, Tag Tag)
        {
            if (id != Tag.Id)
            {
                return BadRequest();
            }

            _context.Entry(Tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // 5. DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var Tag = await _context.Tags.FindAsync(id);
            if (Tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(Tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
