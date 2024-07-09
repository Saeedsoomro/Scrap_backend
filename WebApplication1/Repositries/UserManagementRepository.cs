using WebApplication1.Data;
using WebApplication1.Models.Domain;

public interface IUserManagementRepository
{
    Task AddUserManagementAsync(UserManagement userManagement);
    // Add other methods as needed
}

public class UserManagementRepository : IUserManagementRepository
{
    private readonly ScrapMangementDbContext _context;

    public UserManagementRepository(ScrapMangementDbContext context)
    {
        _context = context;
    }

    public async Task AddUserManagementAsync(UserManagement userManagement)
    {
        _context.UserManagements.Add(userManagement);
        await _context.SaveChangesAsync();
    }
}
