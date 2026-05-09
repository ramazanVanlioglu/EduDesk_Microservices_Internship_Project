using System;
namespace IdentityService.Entities;

public abstract class  BaseEntity
{
    public int Id { get; set; }
    public Guid TenantId { get; set; } //kiracı ID bilgisi
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}