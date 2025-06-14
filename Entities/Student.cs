namespace SchoolAgendCRUD.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public DateTime StudentCreated { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
