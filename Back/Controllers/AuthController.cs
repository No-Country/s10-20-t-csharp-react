using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using quejapp.Models;
using s10.Back.Data.IRepositories;
using s10.Back.DTO;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _configuration;


        public AuthController(IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet("IsAuthenticated")]
        public ActionResult Authenticated()
        {
            return (User.Identity.IsAuthenticated) ?
                Ok(new { IsAuthenticated = true }) :
                Unauthorized(new { IsAuthenticated = false });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
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


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                      new Claim(ClaimTypes.Name,user.Name),
                      new Claim(ClaimTypes.Email,user.Email),
                      new Claim(ClaimTypes.GivenName,user.GivenName),
                      new Claim(ClaimTypes.Surname,user.LastName),
                      new Claim(ClaimTypes.NameIdentifier, user.Id)

                }),
                Expires = DateTime.UtcNow.AddHours(100), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            return Ok(new MeGetDto()
            {
                Email = user.Email,
                Name = user.Name,
                GivenName = user.GivenName,
                LastName = user.LastName,
                Picture_Url = user.ProfilePicAddress,
                Address = user.Address,
                Token = tokenString,
            });
        }


        /// <summary>retornar todos los emails </summary>

        [HttpGet("Emails")]
        public List<string> Emails()
        {
            return _unitOfWork.AppUsers.GetAll().Select(x => x.Email).ToList();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<MeGetDto>> Register(RegisterModel model)
        {
            var newUser = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.GivenName + " " + model.SurName,
                GivenName = model.GivenName ?? null,
                LastName = model.SurName ?? null,
            };
            try
            {

                var result = await _userManager.CreateAsync(newUser, "S10nc123!");

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.Select(x => x));
                }
            }
            catch (Exception e)
            {
                //log
                throw;
            }

            return Ok(new MeGetDto()
            {
                Email = newUser.Email,
                Name = newUser.Name,
                GivenName = newUser.GivenName,
                LastName = newUser.LastName,
                Address = newUser.Address

            });
        }

        public class RegisterModel
        {
            public string? GivenName { get; set; }
            public string? SurName { get; set; }
            public string? ResidenceLocation { get; set; }
            public DateTime? Birthday { get; set; }
            [EmailAddress]
            [Required]
            public string Email { get; set; }
            public string? Password { get; set; }
        }
    }
}
