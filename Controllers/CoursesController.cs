using Microsoft.AspNetCore.Mvc;
using SchoolAgend.Domain.Entities;
using SchoolAgendCRUD.DTOs;
using SchoolAgend.Infrastructure.Data.Repositories;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CourseRepository _courseRepository;

        public CoursesController(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseRepository.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourseById(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            if (course == null || string.IsNullOrWhiteSpace(course.Name))
                return BadRequest("Invalid course data.");

            var createdCourse = await _courseRepository.CreateCourse(course);
            return Created($"/api/courses/{createdCourse.Id}", createdCourse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto request)
        {
            if (request == null || request.Id <= 0) return BadRequest("Invalid course data.");

            var existingCourse = await _courseRepository.GetCourseById(request.Id);
            if (existingCourse == null) return NotFound($"Course with ID {request.Id} not found.");

            existingCourse.Name = request.Name;
            existingCourse.Teacher = request.Teacher;

            await _courseRepository.UpdateCourse(existingCourse);
            return Ok(existingCourse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _courseRepository.GetCourseById(id);
            if (course == null) return NotFound($"Course with ID {id} not found.");

            await _courseRepository.DeleteCourse(id);
            return NoContent();
        }
    }
}
