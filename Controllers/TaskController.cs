using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Repositories;
using mvc.Services;

namespace mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public static TaskRepository taskRepository;

        static TaskController()
        {
            taskRepository = new TaskRepository(new DatabaseService());
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(taskRepository.SelectAll());
        }

        [HttpPost("{listId}")]
        public IActionResult PostTask(int listId, [FromBody] Task task)
        {
            task.ListId = listId;
            return Created("", taskRepository.Create(task));
        }

        [HttpPut("{id}")]
        public IActionResult PatchTask(int id, [FromBody] Task task)
        {
            task.Id = id;
            return Ok(taskRepository.Update(task));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            taskRepository.Delete(id);
            return Ok();
        }

    }
}