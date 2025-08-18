using Microsoft.AspNetCore.Mvc;
using SchoolAgend.Domain.Entities;
using SchoolAgendCRUD.DTOs;
using SchoolAgend.Infrastructure.Data.Repositories;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemindersController : Controller
    {
        private readonly ReminderRepository _reminderRepository;

        public RemindersController(ReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetReminders()
        {
            var reminders = await _reminderRepository.GetAllReminders();
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReminderById(int id)
        {
            var reminder = await _reminderRepository.GetReminderById(id);
            if (reminder == null) return NotFound();
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder([FromBody] Reminder reminder)
        {
            if (reminder == null || string.IsNullOrWhiteSpace(reminder.Message))
                return BadRequest("Invalid reminder data.");

            var createdReminder = await _reminderRepository.CreateReminder(reminder);
            return Created($"/api/reminders/{createdReminder.Id}", createdReminder);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReminder([FromBody] UpdateReminderDto request)
        {
            if (request == null || request.Id <= 0) return BadRequest("Invalid reminder data.");

            var existingReminder = await _reminderRepository.GetReminderById(request.Id);
            if (existingReminder == null) return NotFound($"Reminder with ID {request.Id} not found.");

            existingReminder.Message = request.Message;
            existingReminder.DueDate = request.DueDate;
            existingReminder.TaskId = request.TaskId;

            await _reminderRepository.UpdateReminder(existingReminder);
            return Ok(existingReminder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var reminder = await _reminderRepository.GetReminderById(id);
            if (reminder == null) return NotFound($"Reminder with ID {id} not found.");

            await _reminderRepository.DeleteReminder(id);
            return NoContent();
        }
    }
}
