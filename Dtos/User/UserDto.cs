namespace Auth.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool EmailVerified { get; set; }
    }
}
