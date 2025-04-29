using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool EmailVerified { get; set; } = false;

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
