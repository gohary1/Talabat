using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProductData.Entites.Idintity;
namespace ProductService.Interface
{
    public interface IAuthService
    {
        public Task<string> createToken(AppUser user, UserManager<AppUser> userManager);
    }
}
