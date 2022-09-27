using System;
using Microsoft.EntityFrameworkCore;

namespace introduction_api.Models;
public class TodoContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }
}


