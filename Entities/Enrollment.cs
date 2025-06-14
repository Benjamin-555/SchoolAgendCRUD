namespace SchoolAgendCRUD.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime EnrolledAt { get; set; }
    }
}
