namespace WebApplication1.Models.DTOs
{
    public class UserDetailsDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
    }
}