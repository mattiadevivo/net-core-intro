using System;
using Microsoft.EntityFrameworkCore;
namespace introduction_api.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public TodoItem() { } // Neede for automatic Serialization
        public TodoItem(string name, bool isComplete)
        {
            this.Name = name;
            this.IsComplete = isComplete;
        }

        public TodoItem(long id, string name, bool isComplete): this(name, isComplete)
        {
            this.Id = id;
        }
    }
}

