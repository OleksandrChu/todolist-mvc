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
        public static ListRepository listRepository;

        static ListController()
        {
            listRepository = new ListRepository(new DatabaseService());
        }

        [HttpGet]
        public IActionResult GetLists()
        {
            return Ok(listRepository.SelectAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetList(int id)
        {
            System.Console.WriteLine(id);
            return Ok(listRepository.Select(id));
        }

        [HttpPost]
        public IActionResult PostList([FromBody] TaskList task)
        {
            return Ok(listRepository.Create(task));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteList(int id)
        {
            listRepository.Delete(id);
            return Ok();
        }
    }
}