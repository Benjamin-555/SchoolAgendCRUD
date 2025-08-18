using Microsoft.AspNetCore.Mvc;
using SchoolAgend.Domain.Entities;
using SchoolAgendCRUD.DTOs;
using SchoolAgend.Infrastructure.Data.Repositories;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : Controller
    {
        private readonly SessionRepository _sessionRepository;

        public SessionsController(SessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessions()
        {
            var sessions = await _sessionRepository.GetAllSessions();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            var session = await _sessionRepository.GetSessionById(id);
            if (session == null) return NotFound();
            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            if (session == null || string.IsNullOrWhiteSpace(session.Title))
                return BadRequest("Invalid session data.");

            var createdSession = await _sessionRepository.CreateSession(session);
            return Created($"/api/sessions/{createdSession.Id}", createdSession);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSession([FromBody] UpdateSessionDto request)
        {
            if (request == null || request.Id <= 0) return BadRequest("Invalid session data.");

            var existingSession = await _sessionRepository.GetSessionById(request.Id);
            if (existingSession == null) return NotFound($"Session with ID {request.Id} not found.");

            existingSession.Title = request.Title;
            existingSession.CourseId = request.CourseId;

            await _sessionRepository.UpdateSession(existingSession);
            return Ok(existingSession);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _sessionRepository.GetSessionById(id);
            if (session == null) return NotFound($"Session with ID {id} not found.");

            await _sessionRepository.DeleteSession(id);
            return NoContent();
        }
    }
}
