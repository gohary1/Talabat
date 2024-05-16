using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductApi.DTOs;
using ProductApi.Error;
using ProductApi.Extensions;
using ProductData.Entites.Idintity;
using ProductService.Interface;
using ProductService.Repo;
using System.Diagnostics;
using System.Security.Claims;

namespace ProductApi.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user= await _userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return Ok(new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token =await _authService.createToken(user, _userManager)
                }) ;
            }
            else
            {
                return Unauthorized(new ApiResponse(401));
            }

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                if (!checkEmail(model.Email).Result.Value)
                {
                    var user = new AppUser()
                    {
                        DisplayName = model.DisplayName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.Email.Split("@")[0]
                    };
                    var result = await _userManager.CreateAsync(user, model.Password) ;
                    if (result.Succeeded)
                    {
                        return Ok(new UserDto()
                        {
                            DisplayName = user.DisplayName,
                            Email = user.Email,
                            Token = await _authService.createToken(user, _userManager) 
                        });
                    }
                }
                
            }
          return BadRequest(new ApiResponse(400));
            
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getCurrent")]
        public async Task<ActionResult<UserDto>> getCurrent()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user=await _userManager.FindByIdAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.createToken(user, _userManager)
            });
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<UserDto>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user=await _userManager.FindByEmailWithAddress(User);
            return Ok(user);
        }

        [HttpGet("checkEmail")]
        public async Task<ActionResult<bool>> checkEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
