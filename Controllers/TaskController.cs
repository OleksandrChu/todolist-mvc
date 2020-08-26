using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        public static List<Task> tasks = new List<Task>();

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult PostTask([FromBody] Task task)
        {
            tasks.Add(task);
            return Ok(task);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(int id, [FromBody] JsonPatchDocument patch)
        {
            var task = tasks.Find(task => task.Id.Equals(id));
            if (task == null)
            {
                return BadRequest();
            }
            patch.ApplyTo(task);
            return Ok(task);
        }

    }
}