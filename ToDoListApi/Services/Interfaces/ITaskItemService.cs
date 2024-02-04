using System.Threading.Tasks;
using ToDoListApi.Models;



namespace ToDoListApi.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync();// Modifier pour avoir les bons noms TODO
        Task<TaskItem> GetTaskItemByIdAsync(int id);
        Task AddTaskItemAsync(TaskItem task);
        Task UpdateTaskItemAsync(TaskItem task);
        Task DeleteTaskItemAsync(int id);
    }
}
