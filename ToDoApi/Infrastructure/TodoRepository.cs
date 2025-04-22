using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
namespace ToDoApi.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public ToDoRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDo>> GetAllAsync() => await _context.ToDos.ToListAsync();

        public async Task<ToDo?> GetByIdAsync(int id) => await _context.ToDos.FindAsync(id);

        public async Task<List<ToDo>> GetByDueDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.ToDos.Where(t => t.ExpiryDate >= start && t.ExpiryDate <= end).ToListAsync();
        }

        public async Task AddAsync(ToDo ToDo)
        {
            _context.ToDos.Add(ToDo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDo ToDo)
        {
            _context.ToDos.Update(ToDo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ToDo ToDo)
        {
            _context.ToDos.Remove(ToDo);
            await _context.SaveChangesAsync();
        }
    }
}