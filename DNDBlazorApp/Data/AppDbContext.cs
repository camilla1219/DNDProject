using DNDBlazorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DNDBlazorApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }
}
