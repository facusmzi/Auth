using Auth.Dtos.Session;
using Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SessionWithTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] CreateSessionDto createSessionDto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

                var session = await _sessionService.CreateSessionAsync(createSessionDto, ipAddress, userAgent);
                return Ok(session);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMySessions()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return Unauthorized();
            }

            var sessions = await _sessionService.GetSessionsByUserIdAsync(parsedUserId);
            return Ok(sessions);
        }

        [HttpDelete("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var result = await _sessionService.InvalidateSessionAsync(token);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("logout-all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogoutAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return Unauthorized();
            }

            var result = await _sessionService.InvalidateAllSessionsAsync(parsedUserId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

