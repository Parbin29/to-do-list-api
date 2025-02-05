using Microsoft.AspNetCore.Mvc;
using to_do_list_api.Data;
using to_do_list_api.Models;

using Microsoft.EntityFrameworkCore;

namespace to_do_list_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TaskController(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. GET: api/Task
        [HttpGet]
        public async Task<IActionResult> GetTask()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Tags)
                .ToListAsync();
            return Ok(tasks);
        }

        // 2. GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        // 3. POST: api/Task
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Models.Task Task)
        {
            _context.Tasks.Add(Task);
            await _context.SaveChangesAsync();

            // Return the created Task with a 201 status and location header
            return CreatedAtAction(nameof(GetTask), new { id = Task.Id }, Task);
        }

        // 4. PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Models.Task Task)
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

        [HttpPost("Tags/{taskId}")]
        public async Task<IActionResult> AddTagToTask(int taskId, [FromBody] string tagName)
        {
            var task =await _context.Tasks
                .Include(t=> t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound($"task with id {taskId} not found");
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(t=> t.Name == tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                _context.Tags.Add(tag);
            }

            task.Tags.Add(tag);
            
            await _context.SaveChangesAsync();
            return Ok(tag);
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}