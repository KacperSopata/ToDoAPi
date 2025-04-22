using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ToDo = await _service.GetByIdAsync(id);
            return ToDo == null ? NotFound() : Ok(ToDo);
        }

        [HttpGet("incoming/today")]
        public async Task<IActionResult> GetToday() => Ok(await _service.GetIncomingTodayAsync());

        [HttpGet("incoming/tomorrow")]
        public async Task<IActionResult> GetTomorrow() => Ok(await _service.GetIncomingTomorrowAsync());

        [HttpGet("incoming/week")]
        public async Task<IActionResult> GetWeek() => Ok(await _service.GetIncomingThisWeekAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDo ToDo)
        {
            var created = await _service.CreateAsync(ToDo);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDo ToDo)
        {
            var updated = await _service.UpdateAsync(id, ToDo);
            return updated ? NoContent() : NotFound();
        }

        [HttpPatch("{id}/percent")]
        public async Task<IActionResult> SetPercent(int id, [FromBody] int percent)
        {
            var result = await _service.SetPercentCompleteAsync(id, percent);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{id}/done")]
        public async Task<IActionResult> MarkDone(int id)
        {
            var result = await _service.MarkAsDoneAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}