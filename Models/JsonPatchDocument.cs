using System;
using mvc.Models;

namespace mvc.Controllers
{
    public class JsonPatchDocument
    {
        public bool Done { get; set; }

        internal void ApplyTo(Task task)
        {
            task.Done = Done;
        }
    }
}