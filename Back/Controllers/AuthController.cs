using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using quejapp.Models;
using s10.Back.Data.IRepositories;
using s10.Back.DTO;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace s10.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public AuthController(IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        [HttpGet("IsAuthenticated")]
        public ActionResult Authenticated()
        {
            return Ok(new { IsAuthenticated = (User.Identity?.IsAuthenticated) ?? false });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { loggedOut = true });
        }

        public class LoginModel
        {
            [EmailAddress]
            public string? User { get; set; }
            public string? Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = _unitOfWork.AppUsers.Find(x => x.Email == loginModel.User).FirstOrDefault();
            if (user == null)
            {
                //bad credentials
                return Unauthorized();
            }
            var claims = new List<Claim>() { new Claim(ClaimTypes.Email, user.Email) };
            //await _signInManager.SignInAsync(user , isPersistent:true);
            await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, claims);

            return Ok(new MeGetDto()
            {
                Email = user.Email,
                Name = user.Name,
                GivenName = user.GivenName,
                LastName = user.LastName,
                Picture_Url = user.ProfilePicAddress,
                Address = user.Address
            });
        }


        /// <summary>retornar todos los emails </summary>

        [HttpGet("Emails")]
        public List<string> Emails()
        {
            return _unitOfWork.AppUsers.GetAll().Select(x => x.Email).ToList();
        }

        [HttpGet("Register")]
        public async Task<ActionResult<MeGetDto>> Register(string email)
        {
            var newUser = new AppUser()
            {
                Email = email,
                UserName = email,
                Name = email,
            };

            var result = await _userManager.CreateAsync(newUser, "S10nc123!");

            return Ok(new MeGetDto()
            {
                Email = newUser.Email,
                Name = newUser.Name,
                GivenName = newUser.GivenName,
                LastName = newUser.LastName,
                Picture_Url = newUser.ProfilePicAddress,
                Address = newUser.Address
            });


        }
    }
}
