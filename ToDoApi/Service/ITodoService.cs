using ToDoApi.Models;

namespace ToDoApi.Services
{
    public interface IToDoService
    {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task<List<ToDo>> GetIncomingTodayAsync();
        Task<List<ToDo>> GetIncomingTomorrowAsync();
        Task<List<ToDo>> GetIncomingThisWeekAsync();
        Task<ToDo> CreateAsync(ToDo ToDo);
        Task<bool> UpdateAsync(int id, ToDo updated);
        Task<bool> SetPercentCompleteAsync(int id, int percent);
        Task<bool> MarkAsDoneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}