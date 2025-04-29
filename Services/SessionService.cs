using Auth.Dtos.Session;
using Auth.Interfaces;
using Auth.Models;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Auth.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public SessionService(
            ISessionRepository sessionRepository,
            IUserRepository userRepository,
            IPasswordService passwordService,
            IJwtService jwtService,
            IOptions<JwtSettings> jwtSettings,
            IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }

        public async Task<SessionWithTokenDto> CreateSessionAsync(CreateSessionDto createSessionDto, string ipAddress, string userAgent)
        {
            var user = await _userRepository.GetByEmailAsync(createSessionDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Email or Password are invalid.");
            }

            if (!_passwordService.VerifyPassword(createSessionDto.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Email or Password are invalid.");
            }

            var session = new Session
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Address = ipAddress,
                Device = userAgent ?? "unknown",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes)
            };

            await _sessionRepository.CreateAsync(session);

            // Generate JWT token
            string token = _jwtService.GenerateToken(session);
            session.Token = token;

            await _sessionRepository.UpdateAsync(session);

            var sessionDto = _mapper.Map<SessionWithTokenDto>(session);
            return sessionDto;
        }

        public async Task<IEnumerable<SessionDto>> GetSessionsByUserIdAsync(Guid userId)
        {
            var sessions = await _sessionRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<SessionDto>>(sessions);
        }

        public async Task<bool> InvalidateSessionAsync(string token)
        {
            return await _sessionRepository.DeleteByTokenAsync(token);
        }

        public async Task<bool> InvalidateAllSessionsAsync(Guid userId)
        {
            return await _sessionRepository.DeleteByUserIdAsync(userId);
        }
    }
}
