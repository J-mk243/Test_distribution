using Microsoft.EntityFrameworkCore;
using ToDoListApi.Models;



namespace ToDoListApi.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
