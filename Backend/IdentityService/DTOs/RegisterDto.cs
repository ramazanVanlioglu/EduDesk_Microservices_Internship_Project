namespace IdentityService.DTOs;


// Data Transfer Object sınıfı, direkt User sınıfını kullanmak
// güvenli değildir.
public class RegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}