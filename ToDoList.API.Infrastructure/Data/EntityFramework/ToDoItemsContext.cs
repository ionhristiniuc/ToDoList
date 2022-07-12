using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Infrastructure.Data.Entities;

namespace ToDoList.API.Infrastructure.Data.EntityFramework
{
    public class ToDoItemsContext : IdentityDbContext<User>
    {
        public ToDoItemsContext(DbContextOptions<ToDoItemsContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
