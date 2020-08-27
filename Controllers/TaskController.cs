using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Repositories;
using mvc.Services;

namespace mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        public static TaskRepository databseService;

        static TaskController()
        {
            databseService = new TaskRepository(new DatabaseService());
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(databseService.SelectAll());
        }

        [HttpPost]
        public IActionResult PostTask([FromBody] Task task)
        {
            return Ok(databseService.Create(task));
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(int id, [FromBody] JsonPatchDocument patch)
        {
            var updatedTask = databseService.Update(id, patch);
            if(updatedTask == null)
            {
                return BadRequest();
            }
            return Ok(updatedTask);
        }

    }
}