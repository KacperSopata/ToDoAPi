using ToDoApi.Models;

namespace ToDoApi.Repositories
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task<List<ToDo>> GetByDueDateRangeAsync(DateTime start, DateTime end);
        Task AddAsync(ToDo ToDo);
        Task UpdateAsync(ToDo ToDo);
        Task DeleteAsync(ToDo ToDo);
    }
}