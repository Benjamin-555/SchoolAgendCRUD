namespace SchoolAgendCRUD.DTOs
{
    public class CreateStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
