using IdentityService.Entities;

namespace IdentityService.Services;

public interface ITokenService
{
    string CreateToken(User user);
}