using ToDoApi.Models;
using ToDoApi.Repositories;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repo;

        public ToDoService(IToDoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ToDo>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<ToDo?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<List<ToDo>> GetIncomingTodayAsync() =>
            await _repo.GetByDueDateRangeAsync(DateTime.Today, DateTime.Today);

        public async Task<List<ToDo>> GetIncomingTomorrowAsync()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            return await _repo.GetByDueDateRangeAsync(tomorrow, tomorrow);
        }

        public async Task<List<ToDo>> GetIncomingThisWeekAsync()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            return await _repo.GetByDueDateRangeAsync(start, end);
        }

        public async Task<ToDo> CreateAsync(ToDo ToDo)
        {
            await _repo.AddAsync(ToDo);
            return ToDo;
        }

        public async Task<bool> UpdateAsync(int id, ToDo updated)
        {
            var ToDo = await _repo.GetByIdAsync(id);
            if (ToDo == null) return false;
            ToDo.Title = updated.Title;
            ToDo.Description = updated.Description;
            ToDo.ExpiryDate = updated.ExpiryDate;
            ToDo.PercentComplete = updated.PercentComplete;
            ToDo.IsDone = updated.IsDone;
            await _repo.UpdateAsync(ToDo);
            return true;
        }

        public async Task<bool> SetPercentCompleteAsync(int id, int percent)
        {
            var ToDo = await _repo.GetByIdAsync(id);
            if (ToDo == null) return false;
            ToDo.PercentComplete = percent;
            await _repo.UpdateAsync(ToDo);
            return true;
        }

        public async Task<bool> MarkAsDoneAsync(int id)
        {
            var ToDo = await _repo.GetByIdAsync(id);
            if (ToDo == null) return false;
            ToDo.IsDone = true;
            await _repo.UpdateAsync(ToDo);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ToDo = await _repo.GetByIdAsync(id);
            if (ToDo == null) return false;
            await _repo.DeleteAsync(ToDo);
            return true;
        }
    }
}