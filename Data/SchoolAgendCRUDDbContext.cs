using Microsoft.EntityFrameworkCore;
using SchoolAgendCRUD.Entities;

namespace SchoolAgendCRUD.Data
{
    public class SchoolAgendCRUDDbContext : DbContext
    {
        private string _connectionString;

        public SchoolAgendCRUDDbContext(DbContextOptions<SchoolAgendCRUDDbContext> options): base(options) 
        { 
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }    
    }
}
