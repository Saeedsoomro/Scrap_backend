using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Repositries
{
    public interface ITokenRepository
    {
        string CraeteJwtToken(IdentityUser user, IList<string> roles);
      
    }
}
