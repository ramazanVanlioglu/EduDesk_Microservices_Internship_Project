//veri tabanı kuralları ve satır seviyesi güvenlik dosyası

using IdentityService.Entities;
using IdentityService.Services;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class AppDbContext : DbContext
{
    private readonly ITenantService _tenantService;
    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
    }

    public DbSet <User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //otomatik satır seviye güvenlik filtresi,
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantService.GetTenantId());
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //yeni veri eklenince TenantId'yi otomatik basalım
        foreach(var entry in ChangeTracker.Entries <BaseEntity>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = _tenantService.GetTenantId();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}

    




