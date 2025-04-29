using Auth.Models;

namespace Auth.Interfaces
{
    public interface ISessionRepository
    {
        Task<Session> GetByIdAsync(Guid id);
        Task<Session> GetByTokenAsync(string token);
        Task<IEnumerable<Session>> GetByUserIdAsync(Guid userId);
        Task<Session> CreateAsync(Session session);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<Session> UpdateAsync(Session session);
        Task<bool> DeleteByTokenAsync(string token);
        Task<bool> DeleteByUserIdAsync(Guid userId);
    }
}
