using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        public static DatabaseService  databseService;

        static TaskController() {
            databseService = new DatabaseService();
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(databseService.Select());
        }

        [HttpPost]
        public IActionResult PostTask([FromBody] Task task)
        {
            return Ok(databseService.Create(task));
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(int id, [FromBody] JsonPatchDocument patch)
        {
            databseService.Update(id, patch);
            return Ok();
        }

    }
}