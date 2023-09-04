using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using s10.Back.Data.IRepositories;
using s10.Back.Data.Repositories;
using s10.Back.DTO;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using System.Security.Claims;

namespace quejapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuejasController : ControllerBase
{
    private readonly ILogger<QuejasController> _logger;
    private readonly RedCoContext _context;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly GeometryFactory _geometryFactory;
    //private List<Comment> _comments;
    //private List<Queja> _quejas;
    //private List<Category> _categories;
    //private List<AppUser> _users;
    //private List<District> _districts;

    public QuejasController(ILogger<QuejasController> logger, RedCoContext context, 
        ICloudinaryService cloudinaryService, GeometryFactory geometryFactory)
    {
        _logger = logger;
        _context = context;
        _cloudinaryService = cloudinaryService;
        _geometryFactory = geometryFactory;
    }
    [HttpGet] 
    [Route("{id}/comments")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsByQueja(
        [FromQuery] CommentByQuejaDTO input, int id)
    {
        if (ModelState.IsValid)
        {
            //verificar que el id usuario sea del que crea el comentario cuando haya login
            // y verificar que exista la queja
            #region WithFiles
            //if (_comments is null)
            //{
            //    _comments = JsonConvert.DeserializeObject<List<Comment>>(System.IO.File.ReadAllText(@"./Back/Data/comments.json"))!;
            //}

            //var comment = new Comment()
            //{
            //    Text = model.Text,
            //    User_ID = model.User_ID,
            //    Complaint_ID = model.Complaint_ID,
            //    AddedAt = DateTime.Now
            //};
            //_comments.Add(comment);
            //int changes = 1;
            #endregion

            #region SQLServer
            //if (_context.Comment.Find(id) is null)
            //    return NotFound();

            //var comment = new Comment()
            //{
            //    Complaint_ID = id,
            //    Text = model.Text,
            //    User_ID = int.Parse(HttpContext.User.Identities.First().Claims.First(x => x.Type.Contains("nameidenfifier")).Value),
            //    AddedAt = DateTime.UtcNow
            //};
            //_context.Comment.Add(comment);
            //var changes = await _context.SaveChangesAsync();
            #endregion

            #region WithUnitOfWorkPattern
            var unitOfWork = new UnitOfWork(_context);
            PagedList<CommentResponseDTO>? pagedComments =
                unitOfWork.Comments.GetCommentsOfQueja(input, id);
            await unitOfWork.Complete();
            #endregion

            if (pagedComments is not null)
            {
                return new PagedListResponse<CommentResponseDTO>(
                    pagedComments,
                    (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost]
    [Authorize]
    [Route("{id}/comments")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<CommentResponseDTO>> CreateComment([FromQuery] CommentRequestDTO model, int id)
    {
        if (ModelState.IsValid)
        {
            var unitOfWork = new UnitOfWork(_context);
            Comment comment = new Comment()
            {
                AddedAt = DateTime.UtcNow,
                Complaint_ID = id,
                Text = model.Text,
                User_ID = unitOfWork
                    .AppUsers
                    .GetByEmail(User.FindFirstValue(ClaimTypes.Email))
                    .Result!.First().User_ID, // tendría que existir sí o sí, porque al loguear crea el usuario, acá accedemos a un usuario logueado 
            };
            unitOfWork.Comments.Add(comment);
            int changes = await unitOfWork.Complete();

            if (changes > 0)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<QuejaResponseDTO>>> GetAll(
        [FromQuery] QuejaRequestDTO input)
    {
        #region WithFiles
        //if (_quejas is null)
        //{
        //    _quejas = JsonConvert.DeserializeObject<List<Queja>>(System.IO.File.ReadAllText(@"./Back/Data/complaints.json"))!;
        //}

        //var theseQuejas = _quejas.AsQueryable();
        //if (input.Category_ID is not null)
        //    theseQuejas = theseQuejas.Where(q => q.Category_ID == input.Category_ID);
        //if (input.District_ID is not null)
        //    theseQuejas = theseQuejas.Where(q => q.District_ID == input.District_ID);
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    theseQuejas = theseQuejas.Where(q => q.Title.Contains(input.FilterQuery));
        #endregion

        #region SQLServer
        //var query = _context.Queja.AsQueryable();

        //if (input.Category_ID is not null)
        //    query = query.Where(q => q.Category_ID == input.Category_ID);
        //if (input.District_ID is not null)
        //    query = query.Where(q => q.District_ID == input.District_ID);
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    query = query.Where(q => q.Title.Contains(input.FilterQuery));

        //var recordCount = await query.CountAsync();
        //query = query.OrderBy($"{input.SortColumn} {input.SortOrder}");
        #endregion
        if (ModelState.IsValid)
        {
            #region WithUnitOfWorkPattern
            var unitOfWork = new UnitOfWork(_context);
            PagedList<QuejaResponseDTO> pagedView = unitOfWork.Quejas.GetFeed(input);
            await unitOfWork.Complete();
            #endregion

            return new PagedListResponse<QuejaResponseDTO>(
                pagedView,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpGet]
    [Route("{id}")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<QuejaResponseDTO>>> GetById(int id)
    {
        #region WithFiles
        //if (_quejas is null)
        //{
        //    _quejas = JsonConvert.DeserializeObject<List<Queja>>(System.IO.File.ReadAllText(@"./Back/Data/complaints.json"))!;
        //}

        //var laQueja = await _quejas.AsQueryable().Where(q => q.Complaint_ID == id).FirstOrDefaultAsync();
        #endregion

        #region SQLServer
        //var query = _context.Queja.Where(q => q.Complaint_ID == id);
        //var data = query
        //.Include(q => q.Category)
        //.Include(q => q.District)
        //.Include(q => q.User)
        //.Select(q => new QuejaResponseDTO
        //{
        //    Complaint_ID = q.Complaint_ID,
        //    Text = q.Text,
        //    Title = q.Title,
        //    PhotoAdress = q.PhotoAdress,
        //    VideoAddress = q.VideoAddress,
        //    District_Name = q.District.Name,
        //    UserName = q.User.Name,
        //    Category_Name = q.Category.Name
        //});
        #endregion

        #region WithUnitOfWorkPattern
        var unitOfWork = new UnitOfWork(_context);
        var theQueja = unitOfWork.Quejas.GetPaged(id);
        await unitOfWork.Complete();
        #endregion

        if (theQueja != null)
        {
            return new PagedListResponse<QuejaResponseDTO>(
                theQueja,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
        }
        else {
            return NotFound();
        }
        
    }


    [HttpPost]
    [Authorize]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<QuejaResponseDTO>> Post([FromQuery] QuejaPostDTO model)
    {
        if (ModelState.IsValid)
        {
            #region WithFile
            //if (_quejas is null)
            //{
            //    _quejas = JsonConvert.DeserializeObject<List<Queja>>(System.IO.File.ReadAllText(@"./Back/Data/complaints.json"))!;
            //}

            //Queja queja = new()
            //{
            //    Category_ID = model.Category_ID,
            //    Text = model.Text,
            //    Title = model.Title,
            //    User_ID = model.User_ID,
            //    // TODO
            //    //VideoAddress = model.VideoAddress,
            //    //PhotoAdress = model.PhotoAdress,
            //    District_ID = model.District_ID,
            //    CreatedAt = DateTime.Now
            //};
            //_quejas.Add(queja);
            #endregion

            #region SQLServer
            //var uploadResult = new ImageUploadResult();
            //IFormFile? file = model.media;
            //// si lo guardado es nulo y se subió archivo o simplemente si se subió archivo
            //if (file is not null)
            //{

            //    uploadResult = await _cloudinaryService.AddPhotoAsync(file!);
            //    if (uploadResult.Error is not null)
            //    {
            //        return BadRequest(uploadResult.Error.Message);
            //    }
            //}
            //var laQueja = new Queja()
            //{
            //    Category_ID = model.Category_ID,
            //    District_ID = model.District_ID,
            //    Text = model.Text,
            //    Title = model.Title,
            //    PhotoAdress = "http://res.cloudinary" + uploadResult.SecureUrl.AbsolutePath,
            //    User_ID = model.User_ID//preguntar a DnTo,
            //};
            //await _context.Queja.AddAsync(laQueja);
            #endregion

            #region WithUnitOfWorkPattern
            IFormFile? file = model.media;
            var uploadResult = new ImageUploadResult();
            if (file is not null)
            {
                uploadResult = await _cloudinaryService.AddPhotoAsync(file);
                if (uploadResult.Error is not null)
                {
                    return BadRequest("No se pudo guardar la imagen");
                }
                var unitOfWork = new UnitOfWork(_context);
                Queja theQueja = new Queja()
                {
                    Category_ID = model.Category_ID,
                    District_ID = model.District_ID,
                    Text = model.Text,
                    Title = model.Title,
                    PhotoAdress = "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath,
                    User_ID = unitOfWork
                        .AppUsers
                        .GetByEmail(User.FindFirstValue(ClaimTypes.Email))
                        .Result!.First().User_ID,
                    CreatedAt = DateTime.Now,
                    IsAnonymous = model.IsAnonymous,
                    Location = _geometryFactory.CreatePoint(
                        new NetTopologySuite.Geometries.Coordinate(model.Longitude, model.Latitude))
                };
                unitOfWork.Quejas.Add(theQueja);
                int changes = await unitOfWork.Complete();
                #endregion

                if (changes > 0)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    [Authorize]
    [Route("{id}")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<QuejaDTO>>> UpdateQueja(
        [FromQuery] QuejaDTO model,
        int id)
    {
        if (ModelState.IsValid)
        {
            //verificar que el id usuario sea del que crea el comentario cuando haya login
            #region WithFiles
            //if (_comments is null)
            //{
            //    _comments = JsonConvert.DeserializeObject<List<Comment>>(System.IO.File.ReadAllText(@"./Back/Data/comments.json"))!;
            //}

            //var comment = new Comment()
            //{
            //    Text = model.Text,
            //    User_ID = model.User_ID,
            //    Complaint_ID = model.Complaint_ID,
            //    AddedAt = DateTime.Now
            //};
            //_comments.Add(comment);
            //int changes = 1;
            #endregion

            #region SQLServer
            //var laQueja = _context.Queja.Find(id);
            //if (laQueja == null)
            //    return NotFound();
            //var uploadResult = new ImageUploadResult();
            //IFormFile? file = model.media;
            //// si lo guardado es nulo y se subió archivo o simplemente si se subió archivo
            //if ((laQueja.PhotoAdress is null && file is not null) || file is not null)
            //{

            //    uploadResult = await _cloudinaryService.AddPhotoAsync(file!);
            //    if (uploadResult.Error is not null)
            //    {
            //        return BadRequest(uploadResult.Error.Message);
            //    }
            //}

            //laQueja.Text = model.Text ?? laQueja.Text;
            //laQueja.District_ID = model.District_ID ?? laQueja.District_ID;
            //laQueja.Category_ID = model.Category_ID ?? laQueja.Category_ID;
            //laQueja.User_ID = model.User_ID ?? laQueja.User_ID;
            //laQueja.Title = model.Title ?? laQueja.Title;
            //laQueja.PhotoAdress = uploadResult.SecureUrl is not null
            //    ? "http://res.cloudinary" + uploadResult.SecureUrl.AbsolutePath
            //    : laQueja.PhotoAdress;
            //int changes = _context.SaveChanges();
            #endregion       

            #region WithUnitOfWorkPattern
            var unitOfWork = new UnitOfWork(_context);
            var laQueja = await unitOfWork.Quejas.Update(model, id);
            int changes = await unitOfWork.Complete();
            #endregion
            if (changes > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }
}
