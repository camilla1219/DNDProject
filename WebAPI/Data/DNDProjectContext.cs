using Microsoft.EntityFrameworkCore;

namespace DNDProject.Models
{
    public class DNDProjectContext : DbContext
    {
        public DNDProjectContext(DbContextOptions<DNDProjectContext> options)
            : base(options)
        {
        }

        // Define your DbSets (tables) here
        public DbSet<Question> Question { get; set; }
    }
}
