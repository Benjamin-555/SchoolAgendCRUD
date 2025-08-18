using Microsoft.AspNetCore.Mvc;
using SchoolAgendCRUD.DTOs;
using SchoolAgend.Domain.Entities;
using System.Threading.Tasks;
using SchoolAgend.Infrastructure.Data.Repositories;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksIController : Controller
    {
        private readonly TaskRepository _taskRepository;

        public TasksIController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Task task)
        {
            var createdTask = await _taskRepository.CreateTask(task);
            return Created($"/api/tasks/{createdTask.Id}", createdTask);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto request)
        {
            if (request == null || request.Id <= 0) return BadRequest("Invalid task data.");

            var existingTask = await _taskRepository.GetTaskById(request.Id);
            if (existingTask == null) return NotFound($"Task with ID {request.Id} not found.");

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.DueDate = request.DueDate;
            existingTask.SessionId = request.SessionId;

            await _taskRepository.UpdateTask(existingTask);
            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null) return NotFound($"Task with ID {id} not found.");

            await _taskRepository.DeleteTask(id);
            return NoContent();
        }
    }
}
