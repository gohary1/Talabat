using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductData.Entites.Idintity;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace ProductApi.Extensions
{
    public static class UserManagerAddress
    {
        public static async Task<AppUser> FindByEmailWithAddress(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user =  userManager.Users.Include(x => x.Address).FirstOrDefault(e => e.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}
