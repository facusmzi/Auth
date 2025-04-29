using Auth.Models;

namespace Auth.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Session session);
        bool ValidateToken(string token, out Guid sessionId);
    }
}
