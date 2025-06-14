namespace SchoolAgendCRUD.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public DateTime StudentCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
