using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class Task
    {
        public Task() { }
        public Task(int id, string name, bool done, int listId)
        {
            Id = id;
            Name = name;
            Done = done;
            ListId = listId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public int ListId {get; set;}


    }
}