using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        public string DisplayName { get; set; }
    }
}
