namespace SchoolAgendCRUD.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public string Subject { get; set; }
        public double Value { get; set; }

    }
}
