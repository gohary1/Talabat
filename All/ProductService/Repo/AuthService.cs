using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductData.Entites.Idintity;
using ProductService.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Repo
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> createToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role,role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:AuthKey"]));

            var Token = new JwtSecurityToken(
                audience: _config["JWT:ValidAud"],
                issuer: _config["JWT:ValidIssuer"],
                expires:DateTime.Now.AddDays(double.Parse( _config["JWT:DurationDays"])) ,
                claims: authClaims,
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.Aes128CbcHmacSha256)
                
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
