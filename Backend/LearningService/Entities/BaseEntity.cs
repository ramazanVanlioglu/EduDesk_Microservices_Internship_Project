namespace LearningService.Entities;


public abstract class BaseEntity
{
    public int Id { get; set; }
    public Guid TenantId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
