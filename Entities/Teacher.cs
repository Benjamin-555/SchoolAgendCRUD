using System.ComponentModel.DataAnnotations;

namespace SchoolAgendCRUD.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Departament { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
