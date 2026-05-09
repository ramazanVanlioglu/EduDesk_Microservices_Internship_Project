namespace IdentityService.Services;

public static class PasswordHasher
{

    //şifreyi hash'leme
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    //giriş yaparken şifreyi doğrulama
    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

}