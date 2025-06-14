namespace SchoolAgendCRUD.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
