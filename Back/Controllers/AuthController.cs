using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using s10.Back.Services.Auth.Models;
using System.Security.Claims;

namespace s10.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;


        public AuthController()
        {
        }

        [HttpGet("Authenticated"),Authorize]
        public ActionResult Authenticated()
        {
            return Ok(User.Identity.Name);
        }

    }
}
