using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchoolAgendCRUD.Entities;

namespace SchoolAgendCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private List<SchoolAgendCRUD.Entities.Student> _students;

        public StudentsController()
        {
            _students = new List<Entities.Student>();
            _students.Add(new Entities.Student { Id = 1, Name = "Benjamin", Email = "example@gmail.com", Address = "Street Anacaona", Telephone = "839493903", IsActive = true });
            _students.Add(new Entities.Student { Id = 2, Name = "Pedro", Email = "example@gmail.com", Address = "Street Anacaona", Telephone = "839493903", IsActive = true });
            _students.Add(new Entities.Student { Id = 3, Name = "Maria", Email = "example@gmail.com", Address = "Street Anacaona", Telephone = "839493903", IsActive = true });
            _students.Add(new Entities.Student { Id = 4, Name = "Jose", Email = "example@gmail.com", Address = "Street Anacaona", Telephone = "839493903", IsActive = true });
        }

        [HttpGet]

        public IActionResult GetStudents()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id) {

            Student student = new Student();

            //foreach (var item in _students)
            //{
            //    if (item.Id == id)
            //    {
            //        student = item;
            //        break;
            //    }
            //}
            //student = _students.FirstOrDefault(stu => stu.Id == id);
            student = _students.Where(st => st.Id == id).FirstOrDefault(); //We are using LINQ is more practice for the development
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (student == null) {
                return BadRequest("Student can't be null");
            }

            student.Id = _students.Count + 1;
            student.StudentCreated = DateTime.Now;
            _students.Add(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromBody] Student student) {

            if (student == null || student.Id != student.Id)
            {
                return BadRequest("Student is null or ID mismatch.");
            }
            var existingStudent = _students.FirstOrDefault(stu => stu.Id == student.Id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {student.Id} not found.");
            }

            existingStudent.Name = student.Name;
            existingStudent.Address = student.Address;
            existingStudent.Telephone = student.Telephone;
            existingStudent.Email = student.Email;
            existingStudent.StudentCreated = DateTime.Now;
            return Ok(_students);
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
            var student = _students.FirstOrDefault(stu => stu.Id == id);
            if(student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            _students.Remove(student);

            return Ok(_students);
        }
    }
} 
