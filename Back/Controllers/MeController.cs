using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using NetTopologySuite.Geometries;
using quejapp.Models;
using s10.Back.Data;
using s10.Back.Data.IRepositories;
using s10.Back.Data.Repositories;
using s10.Back.DTO;
using s10.Back.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Xml.Linq;

namespace s10.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly RedCoContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public MeController(RedCoContext context, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<MeGetDto> Me()
        {
            var claims = User.Identities
                .FirstOrDefault().Claims;
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var user = _unitOfWork.AppUsers.Find(x => x.Email == userEmail).FirstOrDefault();

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
                Name = (claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value) ?? "",
                GivenName = (claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value) ?? "",
                LastName = (claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value) ?? "",
                Picture_Url = user == null ? null : user.ProfilePicAddress,
                Address = user == null ? null : user.Address
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
        public ActionResult<MeGetDto> MePicture(IFormFile picture)
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
                Picture_Url = "Not yet implemented but you will receive an url to " + picture.FileName,
            };
        }


        /// <summary>get all my complaints </summary>
       // [Authorize]
        [HttpGet("Quejas")]
        public ActionResult<PagedListResponse<QuejaResponseDTO>> GetMyComplaints([FromQuery] RequestDTO<object> pagedQuery, [FromServices] GeometryFactory _geometryFactory)
        {
            //TODO Favorites service -> Update values on entity onFavoritedEvent
            var favoritesCount = _unitOfWork
                .Favorites.GetAll().GroupBy(x => x.Complaint_ID)
                .Select(x => new
                {
                    Complaint_Id = x.Key,
                    Likes = x.Count()
                }
                ).ToList();

            //email for debug
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? _unitOfWork.AppUsers.Get(1).Email;

            var misQuejas = _unitOfWork.Quejas.GetAll()
                .Include(x => x.Category)
                .Where(x => x.User.Email == email);

            #region This will not be necessary when  Favorites Service gets implemented
            misQuejas.ToList().ForEach(x =>
               {
                   x.Favorites_Count = (favoritesCount.FirstOrDefault(y => y.Complaint_Id == x.Complaint_ID)?.Likes) ?? 0;
               });
            #endregion

            var url = HttpContext.Request.Path;
            var pagedList = PagedList<Queja>.Create(misQuejas, pagedQuery.PageIndex, pagedQuery.PageSize);
            var response = pagedList.ToPagedListResponse(QuejasToDto, url);

            return response;
        }

        //[Authorize]
        [HttpGet("Comments/Left")]
        public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsLeft([FromQuery] RequestDTO<object> pagedQuery)
        {
            //for debug, will choose first user with id 1 if not logged 
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? _unitOfWork.AppUsers.Get(1).Email;
            var user = (await _unitOfWork.AppUsers.GetByEmail(email)).First();
            var myComments = _unitOfWork.Comments.GetAll().Where(x => x.User_ID == user.User_ID);

            var pagedList = PagedList<Comment>.Create(myComments, pagedQuery.PageIndex, pagedQuery.PageSize);

            var url = HttpContext.Request.Path;
            var dtos = pagedList.ToPagedListResponse(CommentsToDto, url);

            return dtos;
        }

        //[Authorize]
        [HttpGet()]
        [Route("Comments/Received")]
        public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsReceived([FromQuery] RequestDTO<object> pagedQuery)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? _unitOfWork.AppUsers.Get(1).Email;
            return await CommentsService_GetCommentsReceived(email);
        }

        //[Authorize]
        [HttpGet()]
        [Route("Comments/Received/{Complaint_Id?}")]
        public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsReceived([FromQuery] RequestDTO<object> pagedQuery, int Complaint_Id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? _unitOfWork.AppUsers.Get(1).Email;
            return await CommentsService_GetCommentsReceived(email, Complaint_Id);
        }


        [HttpGet()]
        [Route("Favorites")]
        public async Task<ActionResult<PagedListResponse<Queja>>> GetMyFavorites([FromQuery] RequestDTO<object> pagedQuery)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? _unitOfWork.AppUsers.Get(1).Email;
            var user = (await _unitOfWork.AppUsers.GetByEmail(email)).First();

            var favorites = _unitOfWork.Favorites.GetAll()
                .Where(x => x.FavoritedBy == email)
                .Select(x => x.Complaint_ID);

            var myFavoriteComplaints = _unitOfWork.Quejas.GetAll().Where(x => favorites.Contains(x.Complaint_ID));

            var pagedList = PagedList<Queja>.Create(myFavoriteComplaints);
            var url = HttpContext.Request.Path;
            var pagedResponse = new PagedListResponse<Queja>(pagedList, url);

            //TODO PagedResponse with DTO
            // var pagedResponse = pagedList.ToPagedListResponse("", url);
            //   PagedList<Favorite>.Create(myFavoriteComplaints);

            return pagedResponse;
        }


        /// <summary> Get all comments received by my Complaints </summary>
        [NonAction]
        private async Task<PagedListResponse<CommentResponseDTO>> CommentsService_GetCommentsReceived(string userEmail, int? Complaint_Id = null)
        {
            var user = (await _unitOfWork.AppUsers.GetByEmail(userEmail)).First();
            var ids = _unitOfWork.Quejas.GetAll()
                .Where(x => x.User_ID == user.User_ID)
                ;

            if (!ids.Any(x => x.Complaint_ID == Complaint_Id))
            {
                return new PagedListResponse<CommentResponseDTO>();
            }

            if (Complaint_Id != null)
            {
                ids = ids.Where(x => x.Complaint_ID == Complaint_Id);
            }

            var miComplaintsIds = ids
                .Select(x => x.Complaint_ID);

            var myComplaintsComments = _unitOfWork.Comments
                .GetAll().Include(x => x.User)
                .Where(x => miComplaintsIds.Contains(x.Complaint_ID));

            var pagedList = PagedList<Comment>.Create(myComplaintsComments);

            var url = HttpContext.Request.Path;
            var dtos = pagedList.ToPagedListResponse(CommentsToDto, url);
            return dtos;
        }


        /// <summary> Mapper comments to DtoReponse </summary>
        [NonAction]
        public List<CommentResponseDTO> CommentsToDto(List<Comment> comments)
        {
            var commentsDTO = comments
                .Select(x => new CommentResponseDTO()
                {
                    AddedAt = x.AddedAt,
                    Comment_ID = x.Comment_ID,
                    Complaint_ID = x.Complaint_ID,
                    Text = x.Text,
                    UserName = x.User.Name,
                    UserPhoto = x.User.ProfilePicAddress
                }).ToList();
            return commentsDTO;
        }


        //MAPPER
        //TODO Esteban como calcular Lat y Long con la libreria que instalaste?
        [NonAction]
        public List<QuejaResponseDTO> QuejasToDto(List<Queja> quejas)
        {
            var quejasDTO = quejas.Select(x =>
               new QuejaResponseDTO
               {
                   Complaint_ID = x.Complaint_ID,
                   Title = x.Title,
                   Text = x.Text,
                   Address = x.Address,
                   PhotoAdress = x.PhotoAdress,
                   VideoAddress = x.VideoAddress,
                   Category_Name = x.Category.Name,
                   Category_ID = x.Category.Category_ID,
                   // District_ID = x.District_ID,
                   //Latitude = x.Location.X??0.0,
                   //Longitude = x.Location.Y??0.0,
                   LikesCount = x.Favorites_Count
               }
           );
            return quejasDTO.ToList();
        }
    }
}





