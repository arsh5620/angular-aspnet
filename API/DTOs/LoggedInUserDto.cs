namespace API
{
    public class LoggedInUserDto
    {
        public required string Username { get; set; }
        public required string AuthToken { get; set; }
    }
}