using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Address { get; set; }

        [Required]
        [MaxLength(256)]
        public string Device { get; set; }

        [Required]
        [MaxLength(512)]
        public string Token { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
