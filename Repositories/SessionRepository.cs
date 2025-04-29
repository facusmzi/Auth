using Auth.Data;
using Auth.Interfaces;
using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Session> GetByIdAsync(Guid id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public async Task<Session> GetByTokenAsync(string token)
        {
            return await _context.Sessions
                .FirstOrDefaultAsync(s => s.Token == token);
        }

        public async Task<IEnumerable<Session>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Sessions
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<Session> CreateAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return false;

            _context.Sessions.Remove(session);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteByTokenAsync(string token)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Token == token);

            if (session == null)
                return false;

            _context.Sessions.Remove(session);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Session> UpdateAsync(Session session)
        {
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<bool> DeleteByUserIdAsync(Guid userId)
        {
            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .ToListAsync();

            if (!sessions.Any())
                return false;

            _context.Sessions.RemoveRange(sessions);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
