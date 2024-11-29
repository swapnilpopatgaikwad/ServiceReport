using ServiceReport.Models;

namespace ServiceReport.Interface
{
    public interface IJwtAuthenticationService
    {
        string GenerateToken(User user);
    }
}
