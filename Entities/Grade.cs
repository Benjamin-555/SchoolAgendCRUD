using System.ComponentModel.DataAnnotations;

namespace SchoolAgendCRUD.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        [StringLength(130)]
        public string Subject { get; set; }
        public double Value { get; set; }

    }
}
