using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using to_do_list_api.Data;
using to_do_list_api.Models;

namespace to_do_list_api.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProjectController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Project> GetProjects()
        {
            var projects = _context.Projects.ToList();
            return projects;
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById([FromRoute] int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null) {
                return NotFound(); // return 404 if no project
            }
            
            return Ok(project);
        }        
    }
}