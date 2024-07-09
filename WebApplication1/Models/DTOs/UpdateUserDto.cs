namespace WebApplication1.Models.DTOs
{
    public class UpdateUserDto
    {
     
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string UserId { get; set; }
            public int? RoleId { get; set; }
            public string? PhoneNumber { get; set; }
            public Guid? CategoryID { get; set; }
            public string? Description { get; set; }
  

}
}
