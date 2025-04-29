using Auth.Dtos.Session;

namespace Auth.Interfaces
{
    public interface ISessionService
    {
        Task<SessionWithTokenDto> CreateSessionAsync(CreateSessionDto createSessionDto, string ipAddress, string userAgent);
        Task<IEnumerable<SessionDto>> GetSessionsByUserIdAsync(Guid userId);
        Task<bool> InvalidateSessionAsync(string token);
        Task<bool> InvalidateAllSessionsAsync(Guid userId);
    }
}
