using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using s10.Back.Data;
using s10.Back.DTO;
using System.Security.Claims;

namespace s10.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly RedCoContext _context;

        public MeController(RedCoContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<MeGetDto> Me()
        {
            var claims = User.Identities
                .FirstOrDefault().Claims;

            var userEmail = claims.First(x => x.Type == ClaimTypes.Email).Value;
            //TODO Get the user from Db, if not try to create it and return a DTO
            //var user = _context.Usuarios.FirstOrDefault(x => x.Email.Equals(userEmail));
            //if(User == null )
            //{
            //    //create User
            //}
            //from user, create DTO
            return new MeGetDto()
            {
                Email = userEmail,
                Name = claims.First(x => x.Type == ClaimTypes.Name).Value,
                GivenName = claims.First(x => x.Type == ClaimTypes.GivenName).Value,
                LastName = claims.First(x => x.Type == ClaimTypes.Name).Value,
                Picture_Url = null,
                Address = null
            };
        }

        [Authorize]
        [HttpPatch]
        public ActionResult<MeGetDto> MePatch(MePatchDto me)
        {
            var claims = User.Identities
                .FirstOrDefault().Claims;

            var userEmail = claims.First(x => x.Type == ClaimTypes.Email).Value;
            //TODO Get the user from Db, if not try to create it and return a DTO
            //var user = _context.Usuarios.FirstOrDefault(x => x.Email.Equals(userEmail));
            //if(User == null )
            //{
            //    //create User
            //}
            //update user
            //save user
            //from user, create DTO
            return new MeGetDto()
            {
                Email = userEmail,
                Name = me.Name,
                GivenName = me.GivenName,
                LastName = me.LastName,
                Picture_Url = "Not yet implemented but you will receive an url ",
                Address = me.Address
            };
        }

        [Authorize]
        [HttpPatch("picture")]
        public ActionResult<MeGetDto> MePicture( IFormFile picture)
        {
            var claims = User.Identities
                .FirstOrDefault().Claims;

            var userEmail = claims.First(x => x.Type == ClaimTypes.Email).Value;
            //TODO Get the user from Db, if not try to create it and return a DTO
            //var user = _context.Usuarios.FirstOrDefault(x => x.Email.Equals(userEmail));
            //upload image to ImageHostingService
            //update user
            //save user
            //from user, create DTO
            return new MeGetDto
            {
                Picture_Url = "Not yet implemented but you will receive an url to " +picture.FileName,
            };
        }
    }
}
