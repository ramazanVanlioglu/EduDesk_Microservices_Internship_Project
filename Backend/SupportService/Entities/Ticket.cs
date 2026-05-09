using System.ComponentModel.DataAnnotations;

namespace SupportService.Entities;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Status { get; set; } = "Open"; // Open, InProgress, Closed

    public string UserId { get; set; } // hangi kullanıcı bileti açtı?

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


}