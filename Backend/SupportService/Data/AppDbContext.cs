using Microsoft.EntityFrameworkCore;
using SupportService.Entities;

namespace SupportService.Data;

public class AppDbContext : DbContext
{
    // boş constructor (Migration) için bazen gereklidir
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Eğer servis henüz konfigüre edilmemişse 
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=EduDesk_SupportDB;User Id=sa;Password=Ramazan223205050?;TrustServerCertificate=True;");
        }
    }
}