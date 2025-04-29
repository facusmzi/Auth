using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos.Session
{
    public class CreateSessionDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
