using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;
using ToDoListApi.Services.Interfaces;

namespace ToDoListApi.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ToDoListDbContext _context;

        public TaskItemService(ToDoListDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }
        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task AddTaskItemAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateTaskItemAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskItemAsync(int id)
        {
            var taskItemDelete = await _context.TaskItems.FindAsync(id);
              
            if(taskItemDelete != null)
            {
             _context.TaskItems.Remove(taskItemDelete);
              await _context.SaveChangesAsync();
            }
        }

    }
}
