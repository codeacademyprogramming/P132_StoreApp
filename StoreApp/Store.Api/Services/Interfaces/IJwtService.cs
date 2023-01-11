using Store.Core.Entities;

namespace Store.Api.Services
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user, IList<string> roles, IConfiguration confg);
    }
}
