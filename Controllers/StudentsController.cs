using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAgendCRUD.Data;
using SchoolAgendCRUD.Entities;
using SchoolAgendCRUD.DTOs;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolAgendCRUDDbContext _context;

        public StudentsController(SchoolAgendCRUDDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]

        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id) {

            //Student student = new Student();

            //foreach (var item in _students)
            //{
            //    if (item.Id == id)
            //    {
            //        student = item;
            //        break;
            //    }
            //}
            //student = _students.FirstOrDefault(stu => stu.Id == id);
            var student = _context.Students.Where(st => st.Id == id).FirstOrDefault(); 
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentDto request)
        {
            if (request == null) {
                return BadRequest("Student can't be null");
            }

            var student = new Student 
            { 
                Address = request.Address,
                Name = request.Name,
                StudentCreated = DateTime.Now,
                Email = request.Email,
                Telephone = request.Telephone,
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(new {id = student.Id});
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto request) {

            if (request == null || request.Id != request.Id)
            {
                return BadRequest("Student is null or ID mismatch.");
            }
            var existingStudent = _context.Students.FirstOrDefault(stu => stu.Id == request.Id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {request.Id} not found.");
            }

            existingStudent.Name = request.Name;
            existingStudent.Address = request.Address;
            existingStudent.Telephone = request.Telephone;
            existingStudent.Email = request.Email;
            existingStudent.StudentCreated = DateTime.Now;
            _context.Students.Update(existingStudent);
            _context.SaveChanges();
            //return Ok(existingStudent);
            return NoContent();
        }

        //[HttpPut ("{id}")]
        ////public IActionResult UpdateStudent(int id, [FromBody] Student student)
        //{
        //    if (student == null || student.Id != id)
        //    {
        //        return BadRequest("Student is null or ID mismatch");

        //    }
        //}

        [HttpDelete ("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(stu => stu.Id == id);
            if(student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            _context.Students.Remove(student);

            return NoContent();
        }
    }
} 
