using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos.User
{
    public class UpdatePasswordDto
    {
        [Required]
        [MaxLength(256)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        public string NewPassword { get; set; }
    }
}
