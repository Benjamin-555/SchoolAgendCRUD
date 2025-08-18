using Microsoft.AspNetCore.Mvc;
using SchoolAgendCRUD.Infrastructure.Data;
using SchoolAgendCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolAgend.Infrastructure.Data.Repositories;
using SchoolAgendCRUD.DTOs;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController: Controller
    {
        private readonly GradeRepository _gradeRepository;

        public GradesController(GradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetGrades()
        {
            var grades = await _gradeRepository.GetAllGrades();
            return Ok(grades);
        }

        [HttpGet]
        [Route("by-grade")]

        public async Task<IActionResult> GetGradesByStudent([FromQuery]int studentId)
        {
            if (studentId <= 0)
            {
                return BadRequest("Student ID must be greater than zero.");
            }
            var grades = await _gradeRepository.GetGradesByStudentId(studentId);
            return Ok(grades);
        }

        [HttpGet]
        [Route("with-grades")]
        public async Task<IActionResult> GetStudentsWithGrades()
        {
            var grades = await _gradeRepository.GetGradesWithStudents(0);
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _gradeRepository.GetGradeById(id);
            if (grade == null)
                return NotFound();

            return Ok(grade);
        }


        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] Grade grade)
        {
            if (grade == null)
                return BadRequest("Grade data is null.");

            if (string.IsNullOrWhiteSpace(grade.Subject))
                return BadRequest("Subject is required.");

            if (grade.Value < 0)
                return BadRequest("Grade value must be non-negative.");

            if (grade.StudentId <= 0)
                return BadRequest("StudentId must be greater than 0.");

            var createdGrade = await _gradeRepository.CreateGrade(grade);

            // Puedes usar Created() si no quieres redirigir a otra acción
            return Created($"/api/grades/{createdGrade.Id}", createdGrade);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateGradeAsync([FromBody] UpdateGradeDto request)
        {
            if (request == null || request.Id <= 0)
            {
                return BadRequest("Invalid grade data.");
            }
            var existingGrade = await _gradeRepository.GetGradeById(request.Id);
            if (existingGrade == null)
            {
                return NotFound($"Grade with ID {request.Id} not found.");
            }
            existingGrade.Value = request.Value;
            existingGrade.StudentId = request.StudentId;
            await _gradeRepository.UpdateGrade(existingGrade);
            return Ok(existingGrade);
        }

        //[HttpPut ("{id}")]
        ////public IActionResult UpdateStudent(int id, [FromBody] Student student)
        //{
        //    if (student == null || student.Id != id)
        //    {
        //        return BadRequest("Student is null or ID mismatch");

        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _gradeRepository.GetGradeById(id);

            if (grade == null)
            {
                return NotFound($"Grade with ID {id} not found.");
            }

            await _gradeRepository.DeleteGrade(id);

            return NoContent();
        }
    }
}
