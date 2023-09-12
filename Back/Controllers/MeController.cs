using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
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
using Location = s10.Back.DTO.Location;

namespace s10.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly RedCoContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<AppUser> _userManager;

        public MeController(RedCoContext context, IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<MeGetDto> Me()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var user = _unitOfWork.AppUsers.Find(x => x.Email == userEmail).FirstOrDefault();

            return new MeGetDto()
            {
                Email = userEmail,
                Name = user.Name,
                GivenName = user.GivenName ?? "",
                LastName = user.LastName ?? "",
                Picture_Url = user == null ? null : user.ProfilePicAddress,
                Address = user == null ? null : user.Address
            };
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<MeGetDto>> MePatch(MePatchDto me)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)!.Value;
            var _me = await _userManager.FindByEmailAsync(userEmail);

            _me.GivenName = me.GivenName ?? _me.GivenName;
            _me.LastName = me.LastName ?? _me.LastName;
            _me.Address = me.Address ?? _me.Address;
            _me.Name = _me.GivenName + ' ' + _me.LastName; //identity shpould calculate this

            try
            {
                //the user needs to be updated using userManager for name update? no because is from model
                var updateResult = await _userManager.UpdateAsync(_me);
                //_unitOfWork.AppUsers.Update(_me);
                //await _unitOfWork.Complete();

                return new MeGetDto()
                {
                    Email = userEmail,
                    Name = _me.Name,
                    GivenName = _me.GivenName,
                    LastName = _me.LastName,
                    Picture_Url = _me.ProfilePicAddress,
                    Address = me.Address
                };
            }
            catch (Exception e)
            {
                //log(e)
                throw;
            }
        }


        public class UpdatePictureResult
        {
            public bool IsSuccess { get; set; }
            public string? PictureUrl { get; set; }
            public string? Message { get; set; }
            public string? Error { get; set; }
        }

        [Authorize]
        [HttpPatch("picture")]
        public async Task<ActionResult<UpdatePictureResult>> MePicture(IFormFile picture)
        {
            //TODO UserService
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _unitOfWork.AppUsers
                .GetAll()
                .Where(x => x.Email == userEmail).FirstOrDefaultAsync();

            if (user == null) throw new Exception("Session error");

            try
            {
                var uploadResult = await _cloudinaryService.AddPhotoAsync(picture);
                if (uploadResult.Error is not null)
                {
                    return new UpdatePictureResult
                    {
                        IsSuccess = false,
                        Error = uploadResult.Error.Message,
                    };
                }

                var PhotoAdress = "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath;
                user.ProfilePicAddress = PhotoAdress;
                _unitOfWork.AppUsers.Update(user);
                await _unitOfWork.Complete();

                return new UpdatePictureResult
                {
                    IsSuccess = true,
                    PictureUrl = user.ProfilePicAddress,
                    Message = "Success"
                };

            }
            catch (Exception e)
            {
                return new UpdatePictureResult
                {
                    IsSuccess = false,
                    Error = e.Message
                };
            }
        }


        /// <summary>get all my complaints </summary>
        [Authorize]
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
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (String.IsNullOrEmpty(email)) return Unauthorized();

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

        [Authorize]
        [HttpGet("Comments/Left")]
        public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsLeft([FromQuery] RequestDTO<object> pagedQuery)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = (await _unitOfWork.AppUsers.GetByEmail(email)).First();

            //TODO cambiar a ID
            var myComments = _unitOfWork.Comments.GetAll().Where(x => x.Id == user.User_ID);

            var pagedList = PagedList<Comment>.Create(myComments, pagedQuery.PageIndex, pagedQuery.PageSize);

            var url = HttpContext.Request.Path;
            var dtos = pagedList.ToPagedListResponse(CommentsToDto, url);

            return dtos;
        }

        [Authorize]
        [HttpGet()]
        [Route("Comments/Received")]
        public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsReceived([FromQuery] RequestDTO<object> pagedQuery)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await CommentsService_GetCommentsReceived(email);
        }

        [Authorize]
        [HttpGet()]
        [Route("Comments/Received/{Complaint_Id?}")]
        public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsReceived([FromQuery] RequestDTO<object> pagedQuery, int Complaint_Id)
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            return await CommentsService_GetCommentsReceived(email!, Complaint_Id);
        }

        [Authorize]
        [HttpGet()]
        [Route("Favorites")]
        public async Task<ActionResult<PagedListResponse<QuejaResponseDTO>>> GetMyFavorites([FromQuery] RequestDTO<object> pagedQuery)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = (await _unitOfWork.AppUsers.GetByEmail(email)).First();

            var favorites = _unitOfWork.Favorites.GetAll()
                .Where(x => x.FavoritedBy == email && x.Enabled)
                .Select(x => x.Complaint_ID);

            var myFavoriteComplaints = _unitOfWork.Quejas
                .GetAll()
                .Include(x => x.Category)
                .Where(x => favorites.Contains(x.Complaint_ID));

            var pagedList = PagedList<Queja>.Create(myFavoriteComplaints);
            var url = HttpContext.Request.Path;
            var response = pagedList.ToPagedListResponse(QuejasToDto, url);

            return response;
        }


        /// <summary> Get all comments received by my Complaints </summary>
        [NonAction]
        private async Task<PagedListResponse<CommentResponseDTO>> CommentsService_GetCommentsReceived(string userEmail, int? Complaint_Id = null)
        {
            var user = (await _unitOfWork.AppUsers.GetByEmail(userEmail)).First();
            var ids = _unitOfWork.Quejas
                .GetAll()
                .Where(x => x.User_ID == user.User_ID)
                ;

            if (!ids.Any(x => x.Complaint_ID == Complaint_Id) && Complaint_Id != null)
            {
                //throw since clamplaint doesnt belong to user? or a bussi nens rule from caller
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
                   Latitude = (x.Location?.Y) ?? null,
                   Longitude = (x.Location?.X) ?? null,
                   Location = x.Location!=null? new Location() { Latitude= x.Location.X, Longitude= x.Location.Y } :null,
                   LikesCount = x.Favorites_Count
               }
           );
            return quejasDTO.ToList();
        }
    }
}





