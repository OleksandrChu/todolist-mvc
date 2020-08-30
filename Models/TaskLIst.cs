using System.Collections.Generic;

namespace mvc.Models
{
    public class TaskList
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public List<Task> Tasks {get; set;}
    }
}