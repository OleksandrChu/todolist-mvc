namespace mvc.Models
{
    public class Task
    {
        public Task() { }
        public Task(int id, string name, bool done)
        {
            Id = id;
            Name = name;
            Done = done;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }


    }
}