using System;
using mvc.Models;

namespace mvc.Controllers
{
    public class JsonPatchDocument
    {
        public int Id { get; set; }
        public bool Done { get; set; }
        public string Name {get; set;}
    }
}