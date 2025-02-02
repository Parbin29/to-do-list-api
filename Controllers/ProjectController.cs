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
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProjectController(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.Tasks)
                .ThenInclude(p => p.Tags)
            .ToListAsync();
            /*
            var projects = await _context.Projects
                .Include(p => p.Tasks)
                .ThenInclude(p => p.Tags)
                .Select(p => new
                {
                    ProjectId = p.Id,
                    Name = p.Name,
                    IsActive = p.Status,
                    Tags = p.Tags.Select(tag => new
                    {
                        TagId = tag.Id,
                        Name = tag.Name
                    }).ToList(),
                    Tasks = p.Tasks.Select(task => new
                    {
                        TaskId = task.Id,
                        Name = task.Name,
                        Deadline = task.Deadline,
                        Tags = task.Tags.Select(tag => new
                        {
                            TagId = tag.Id,
                            Name = tag.Name
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
                */
            return Ok(projects);
        }

        // 2. GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstAsync(p => p.Id == id);

            Console.WriteLine($"Project: {project.Name}");
            foreach (var task in project.Tasks)
            {
                Console.WriteLine($" - Task: {task.Name}");
            }

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // 3. POST: api/Project
        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Return the created project with a 201 status and location header
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // 4. PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // 5. DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Project/Tasks/{projectId}")]
        public async Task<IActionResult> AddTaskToProject(int projectId, [FromBody]CreateTaskDto taskTemp)
        {
            var project = _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null) 

            {
                return NotFound($"project with id {projectId} not found");
            }
            var task = new Models.Task
            {
                Name = taskTemp.Name,
                Deadline = taskTemp.Deadline,
                ProjectId = projectId,
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPost("Project/Tags/{projectId}")]
        public async Task<IActionResult> AddTagToProject(int projectId, [FromBody] string tagName)
        {
            var project =await _context.Projects
                .Include(p=> p.Tags)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound($"project with id {projectId} not found");
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(t=> t.Name == tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                _context.Tags.Add(tag);
            }

            project.Tags.Add(tag);
            
            await _context.SaveChangesAsync();
            return Ok(tag);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
