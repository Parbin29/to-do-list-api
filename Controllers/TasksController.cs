using Microsoft.AspNetCore.Mvc;
using to_do_list_api.Data;
using to_do_list_api.Models;

using Microsoft.EntityFrameworkCore;

namespace to_do_list_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TasksController(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // 2. GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTask(int id)
        {
            var Tasks = await _context.Tasks.FindAsync(id);

            if (Tasks == null)
            {
                return NotFound();
            }

            return Tasks;
        }

        // 3. POST: api/Task
        [HttpPost]
        public async Task<ActionResult<Tasks>> CreateTask(Tasks Task)
        {
            _context.Tasks.Add(Task);
            await _context.SaveChangesAsync();

            // Return the created Task with a 201 status and location header
            return CreatedAtAction(nameof(GetTask), new { id = Task.Id }, Task);
        }

        // 4. PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tasks Task)
        {
            if (id != Task.Id)
            {
                return BadRequest();
            }

            _context.Entry(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // 5. DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var Task = await _context.Tasks.FindAsync(id);
            if (Task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(Task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}