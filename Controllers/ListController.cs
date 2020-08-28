using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Repositories;
using mvc.Services;

namespace mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        public static ListRepository databseService;

        static ListController()
        {
            databseService = new ListRepository(new DatabaseService());
        }

        [HttpPost]
        public IActionResult PostList([FromBody] TaskList task)
        {
            return Ok(databseService.CreateList(task));
        }
    }
}