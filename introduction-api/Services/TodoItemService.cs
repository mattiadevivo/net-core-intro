using System;
using introduction_api.Models;

namespace introduction_api.Services;

    public class TodoItemService
    {
        public TodoItemService()
        {
        }
    }

    public interface ITodoItemService
    {
    Task<IEnumerable<TodoItem>> GetAll();
    Task<TodoItem> Get(long id);
    Task<TodoItem> Get(TodoItem item);
    Task Modify(long id, TodoItem item);
    Task Delete(long id);
    }

