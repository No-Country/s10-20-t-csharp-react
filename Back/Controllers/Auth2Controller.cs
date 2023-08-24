using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace s10.Back.Controllers
{
    // [Route("/")]
    public class Auth2Controller : Controller
    {
        public ActionResult Home()
        {
            return Ok("loggedOut");
        }

        /// <summary>
        /// Login with a provides
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Login()
        {
            //TODO Customize for other providers
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(GoogleResponse));
            }
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse"),
            });
            return new EmptyResult();
        }

        // [Route("/signin-google")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
        }

        [Authorize]
        public IActionResult Data()
        {
            return Ok(HttpContext.User.Identities.First().Claims.Select(x => x.Value));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Home));
        }
    }
}
