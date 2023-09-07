using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using s10.Back.Data.Repositories;
using quejapp.Models;
using s10.Back.Data;
using Microsoft.AspNetCore.Http.Extensions;
using CloudinaryDotNet;
using s10.Migrations;

namespace s10.Back.Controllers
{
    // [Route("/")]
    public class Auth2Controller : Controller
    {

        private readonly RedCoContext _context;

        public Auth2Controller(RedCoContext context)
        {
            _context = context;
        }

        public ActionResult Home()
        {
            return Ok("home");
        }

        /// <summary>
        /// Login with a provides
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Login(string mode = "prod", string? Redirect_Uri = null)
        {
            //TODO Customize for other providers
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(GoogleResponse), new { mode , Redirect_Uri });
            }

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse", new { mode, Redirect_Uri }),
            });
            return new EmptyResult();
        }

        // [Route("/signin-google")]
        public async Task<IActionResult> GoogleResponse(string mode, string? Redirect_Uri = null)
        {
            var urlBase = HttpContext.Request.QueryString;

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            var posibleUser = claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Email);
            if (posibleUser.Any())
            {
                var unitOfWork = new UnitOfWork(_context);
                int changes = 0;
                if (await unitOfWork.AppUsers.GetByEmail(posibleUser.First().Value) is null)
                {
                    var newUser = new AppUser()
                    {
                        Email = posibleUser.First().Value,
                        Name = claims.Where(c => c.Type == ClaimTypes.Name).First().Value,
                        GivenName = (claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value) ?? ""
                    };
                    unitOfWork.AppUsers.Add(newUser);
                    changes = await unitOfWork.Complete();
                }
            }

            //redirect_uri cannot be from  outside domain
            //sanitize from sqli
            //xss
            //etc
            //https://learn.microsoft.com/en-us/aspnet/core/security/cross-site-scripting?view=aspnetcore-7.0
            if (Redirect_Uri != null)
            {
                Redirect_Uri = Redirect_Uri.Contains("http") ? null : Redirect_Uri;

            }

            var redirectTo = Redirect_Uri ?? "report";
            if (mode == "dev")
            {
                var client = "https://localhost:5173";
                return Redirect($"{client}/{redirectTo}");
            }
            return Redirect($"/{redirectTo}");

            //            //redirect
            //            var r = HttpContext.Request;
            //            var url = r.Scheme +"://" + r.Host;
            //#if DEBUG
            //            //only webapi redirect to swagger
            //            if(r.Host.Port.Equals(5001))
            //            {
            //                return Redirect($"{url}/swagger");
            //            }
            //            return Redirect($"{url}/");
            //#else
            //            //Relese with spa redirect to Wall?
            //            return Redirect($"{url}/");
            //#endif
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { loggedOut = true });
            //return RedirectToAction(nameof(Home));
        }
    }
}
