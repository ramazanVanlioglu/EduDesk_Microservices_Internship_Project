namespace LearningService.Entities;

public class Lesson : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public string VideoUrl { get; set; } // canlı ders veya video bağlantısı
}