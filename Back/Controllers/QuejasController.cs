using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using s10.Back.Data.IRepositories;
using s10.Back.DTO;
using s10.Back.Services;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace quejapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuejasController : ControllerBase
{
    private readonly ILogger<QuejasController> _logger;
    private readonly RedCoContext _context;
    // unit of work + repository
    private readonly ICloudinaryService _cloudinaryService;
    private List<Comment> _comments;
    private List<Queja> _quejas;
    private List<Category> _categories;
    private List<AppUser> _users;
    private List<District> _districts;

    public QuejasController(ILogger<QuejasController> logger, RedCoContext context, ICloudinaryService cloudinaryService)
    {
        _logger = logger;
        _context = context;
        _cloudinaryService = cloudinaryService;
    }
    [HttpGet] 
    [Route("{id}/comments")]
    [ResponseCache(CacheProfileName = "Any-60")]
    public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetCommentsByQueja(
        [FromQuery] CommentByQuejaDTO input, int id)
    {
        #region SQLServer
        var query = _context.Comment.AsQueryable().Where(c => c.Complaint_ID == id);

        if (!string.IsNullOrEmpty(input.FilterQuery))
            query = query.Where(d => d.Text.Contains(input.FilterQuery));

        query = query.OrderBy($"{input.SortColumn} {input.SortOrder}");
        int recordCount = await query.CountAsync();
        #endregion

        #region WithFiles
        //if (_comments is null)
        //{
        //    _comments = JsonConvert.DeserializeObject<List<Comment>>(System.IO.File.ReadAllText(@"./Back/Data/comments.json"))!;
        //}
        //if (_categories is null)
        //{
        //    _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        //}

        //var thecomments = _comments.Where(c => c.Complaint_ID == id);
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    thecomments = thecomments.Where(d => d.Text.Contains(input.FilterQuery));

        //int recordCount = thecomments.Count();
        #endregion
        if (recordCount > 0)
        {
            var data = query.Include(q => q.User)
                .Select(c => new CommentResponseDTO
                {

                    Comment_ID = c.Comment_ID,
                    Text = c.Text,
                    User_ID = c.User_ID, //alguien podría queren en vez del id su nombre de usuario
                    Complaint_ID = c.Complaint_ID,
                    UserName = c.User!.Name,
                    UserPhoto = c.User.ProfilePicAddress,
                    AddedAt = c.AddedAt,
                });

            var paged = PagedList<CommentResponseDTO>.Create(data, input.PageIndex, input.PageSize);
            return new PagedListResponse<CommentResponseDTO>(
                paged,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!/*funciona... pero a que costo?*/);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    [Route("{id}/comments")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> CreateComment([FromQuery] CommentRequestDTO model, int id)
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
            if (_context.Comment.Find(id) is null)
                return NotFound();

            var comment = new Comment()
            {
                Complaint_ID = id,
                Text = model.Text,
                User_ID = int.Parse(HttpContext.User.Identities.First().Claims.First(x => x.Type.Contains("nameidenfifier")).Value),
                AddedAt = DateTime.UtcNow
            };
            _context.Comment.Add(comment);
            var changes = await _context.SaveChangesAsync();
            #endregion

            if (changes > 0)
            {
                var user = _context.AppUser.Find(model.User_ID);
                if (user is null)
                    return BadRequest();
                var commentResponse = new CommentResponseDTO()
                {
                    Complaint_ID = id,
                    Text = model.Text,
                    User_ID = model.User_ID,
                    UserName = User.Identity!.Name,
                    AddedAt = DateTime.UtcNow
                };
                //var commentResponse = JsonConvert.DeserializeObject<CommentResponseDTO>(JsonConvert.SerializeObject(comment));
                var paged = PagedList<CommentResponseDTO>.Create((new List<CommentResponseDTO> { commentResponse! }).AsQueryable(), 1, 1);
                return new PagedListResponse<CommentResponseDTO>(
                    paged,
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

    [HttpGet]
    [ResponseCache(CacheProfileName = "Any-60")]
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
        var query = _context.Queja.AsQueryable();

        if (input.Category_ID is not null)
            query = query.Where(q => q.Category_ID == input.Category_ID);
        if (input.District_ID is not null)
            query = query.Where(q => q.District_ID == input.District_ID);
        if (!string.IsNullOrEmpty(input.FilterQuery))
            query = query.Where(q => q.Title.Contains(input.FilterQuery));

        var recordCount = await query.CountAsync();
        query = query.OrderBy($"{input.SortColumn} {input.SortOrder}");
        #endregion


        var data = query
            .Include(q => q.Category)
            .Include(q => q.District)
            .Include(q => q.User)
            .Select(q => new QuejaResponseDTO
            {
                Complaint_ID = q.Complaint_ID,
                Text = q.Text,
                Title = q.Title,
                PhotoAdress = q.PhotoAdress,
                VideoAddress = q.VideoAddress,
                District_Name = q.District.Name,
                UserName = q.User.Name,
                Category_Name = q.Category.Name,
                UserPhoto = q.User.ProfilePicAddress,
                Address = q.Address,
                CreatedAt = q.CreatedAt
            });

        if (recordCount > 0)
        {
            try
            {
                var paged = PagedList<QuejaResponseDTO>.Create(data!.AsQueryable(), input.PageIndex, input.PageSize);
                return new PagedListResponse<QuejaResponseDTO>(
                    paged,
                    (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
        else { return NoContent(); }
    }

    [HttpGet]
    [Route("{id}")]
    [ResponseCache(CacheProfileName = "Any-60")]
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
        var query = _context.Queja.Where(q => q.Complaint_ID == id);
        var data = query
        .Include(q => q.Category)
        .Include(q => q.District)
        .Include(q => q.User)
        .Select(q => new QuejaResponseDTO
        {
            Complaint_ID = q.Complaint_ID,
            Text = q.Text,
            Title = q.Title,
            PhotoAdress = q.PhotoAdress,
            VideoAddress = q.VideoAddress,
            District_Name = q.District.Name,
            UserName = q.User.Name,
            Category_Name = q.Category.Name
        });
        #endregion


        if (await data.CountAsync() > 0)
        {
            try
            {
                var paged = PagedList<QuejaResponseDTO>.Create(data, 1, 1);
                return new PagedListResponse<QuejaResponseDTO>(
                    paged,
                    (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
        else { return NoContent(); }
    }


    [HttpPost]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<QuejaResponseDTO>>> Post([FromQuery] QuejaPostDTO model)
    {
        if (ModelState.IsValid)
        {

            //IFormFile? imageFile = Request.Form.Files[0] ?? null;
            //IFormFile? videoFile = Request.Form.Files[1] ?? null;

            // TODO : procesar los archivos y para poder guardar su dirección
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
            var uploadResult = new ImageUploadResult();
            IFormFile? file = model.media;
            // si lo guardado es nulo y se subió archivo o simplemente si se subió archivo
            if (file is not null)
            {

                uploadResult = await _cloudinaryService.AddPhotoAsync(file!);
                if (uploadResult.Error is not null)
                {
                    return BadRequest(uploadResult.Error.Message);
                }
            }
            var laQueja = new Queja()
            {
                Category_ID = model.Category_ID,
                District_ID = model.District_ID,
                Text = model.Text,
                Title = model.Title,
                PhotoAdress = "http://res.cloudinary" + uploadResult.SecureUrl.AbsolutePath,
                User_ID = model.User_ID//preguntar a DnTo,
            };
            #endregion
            await _context.Queja.AddAsync(laQueja);
            int changes = _context.SaveChanges();
            if (changes > 0)
            {
                var data = JsonConvert.DeserializeObject<QuejaResponseDTO>(JsonConvert.SerializeObject(laQueja));
                var paged = PagedList<QuejaResponseDTO>.Create((new List<QuejaResponseDTO> { data! }).AsQueryable(), 1, 1);
                return new PagedListResponse<QuejaResponseDTO>(paged, (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
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
            var laQueja = _context.Queja.Find(id);
            if (laQueja == null)
                return NotFound();
            var uploadResult = new ImageUploadResult();
            IFormFile? file = model.media;
            // si lo guardado es nulo y se subió archivo o simplemente si se subió archivo
            if ((laQueja.PhotoAdress is null && file is not null) || file is not null)
            {
               
                uploadResult = await _cloudinaryService.AddPhotoAsync(file!);
                if (uploadResult.Error is not null)
                {
                    return BadRequest(uploadResult.Error.Message);
                }
            }
           
            laQueja.Text = model.Text ?? laQueja.Text;
            laQueja.District_ID = model.District_ID ?? laQueja.District_ID;
            laQueja.Category_ID = model.Category_ID ?? laQueja.Category_ID;
            laQueja.User_ID = model.User_ID ?? laQueja.User_ID;
            laQueja.Title = model.Title ?? laQueja.Title;
            laQueja.PhotoAdress = uploadResult.SecureUrl is not null
                ? "http://res.cloudinary" + uploadResult.SecureUrl.AbsolutePath
                : laQueja.PhotoAdress;
            int changes = _context.SaveChanges();
            #endregion
            if (changes > 0)
            {
                var commentResponse = JsonConvert.DeserializeObject<QuejaDTO>(JsonConvert.SerializeObject(laQueja));
                var paged = PagedList<QuejaDTO>.Create((new List<QuejaDTO> { commentResponse! }).AsQueryable(), 1, 1);
                return new PagedListResponse<QuejaDTO>(
                    paged,
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
}
