namespace Auth.Dtos.Session
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Device { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
