using Microsoft.EntityFrameworkCore;

namespace SchoolAgendCRUD.Data
{
    public class SchoolAgendCRUDDbContext : DbContext
    {
        private string _connectionString;

        public SchoolAgendCRUDDbContext(DbContextOptions<SchoolAgendCRUDDbContext> options): base(options) 
        { 
            
        
        }
        
    }
}
