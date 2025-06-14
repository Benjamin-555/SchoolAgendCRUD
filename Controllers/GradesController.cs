using Microsoft.AspNetCore.Mvc;
using SchoolAgendCRUD.Data;
using SchoolAgendCRUD.Entities;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController: Controller
    {
        private readonly SchoolAgendCRUDDbContext _context;

        public GradesController(SchoolAgendCRUDDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetGrades()
        {
            var grades = _context.Grades.ToList();
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public IActionResult GetGrades(int id)
        {

            var grade = _context.Students.Where(st => st.Id == id).FirstOrDefault();
            if (grade == null)
            {
                return NotFound($"Grade with ID {id} not found.");
            }

            return Ok(grade);
        }

        [HttpPost]
        public IActionResult CreateGrade([FromBody] Grade grade)
        {
            if (grade == null)
            {
                return BadRequest("Grade can't be null");
            }

            var studentExists = _context.Students.Any(s => s.Id == grade.StudentId);
            if (!studentExists)
            {
                return BadRequest($"No existe el estudiante con ID = {grade.StudentId}");
            }
            grade.Value = grade.Value;
            _context.Grades.Add(grade);
            _context.SaveChanges();
            return Ok(grade);
        }

        [HttpPut]
        public IActionResult UpdateGrade([FromBody] Grade grade)
        {

            if (grade == null || grade.Id != grade.Id)
            {
                return BadRequest("Grade is null or ID mismatch.");
            }
            var existingGrade = _context.Grades.FirstOrDefault(stu => stu.Id == grade.Id);
            if (existingGrade == null)
            {
                return NotFound($"Grade with ID {grade.Id} not found.");
            }

            existingGrade.Id = grade.Id;
            existingGrade.StudentId = grade.StudentId;
            existingGrade.Subject = grade.Subject;
            existingGrade.Value = grade.Value;
            _context.Grades.Update(existingGrade);
            _context.SaveChanges();
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
        public IActionResult DeleteGrade(int id)
        {
            var grade = _context.Grades.FirstOrDefault(stu => stu.Id == id);
            if (grade == null)
            {
                return NotFound($"Grade with ID {id} not found.");
            }
            _context.Grades.Remove(grade);

            return NoContent();
        }
    }
}
