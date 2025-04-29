using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos.User
{
    public class UpdateUserDto
    {
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(64)]
        public string DisplayName { get; set; }
    }
}
