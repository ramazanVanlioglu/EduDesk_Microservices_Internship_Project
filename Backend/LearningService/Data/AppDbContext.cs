using Microsoft.EntityFrameworkCore;
using LearningService.Entities;

namespace LearningService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Lesson> Lessons { get; set; }
}