using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using quejapp.Models;
using s10.Back.Data;
using s10.Back.Data.IRepositories;
using s10.Back.Data.Repositories;
using s10.Back.DTO;
using s10.Back.Models;
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
    private readonly IUnitOfWork _unitOfWork;
    //private List<Comment> _comments;
    //private List<Queja> _quejas;
    //private List<Category> _categories;
    //private List<AppUser> _users;
    //private List<District> _districts;

    public QuejasController(IUnitOfWork unitOfWork, ILogger<QuejasController> logger, RedCoContext context,
        ICloudinaryService cloudinaryService, GeometryFactory geometryFactory)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _context = context;
        _cloudinaryService = cloudinaryService;
        _geometryFactory = geometryFactory;
    }
    [HttpGet]
    [Authorize]
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
                return NotFound();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("{id}/comments")]
    [Authorize]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<CommentResponseDTO>> CreateComment(CommentRequestDTO model, int id)
    {
        var unitOfWork = new UnitOfWork(_context);
        //com[plaint should exist
        if (_unitOfWork.Quejas.Get(id) == null)
        {
            return BadRequest();
        }

        Comment comment = new Comment()
        {
            AddedAt = DateTime.UtcNow,
            Complaint_ID = id,
            Text = model.Text,
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
            User_ID = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
        unitOfWork.Comments.Add(comment);
        try
        {
            int changes = await unitOfWork.Complete();
            return Created($"api/quejas/{id}/comments", null);
        }
        catch (Exception e)
        {

            throw;
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
            await unitOfWork.Complete(); //TODO Aca no necesitas un complete porque no has hecho ningun cambio
            #endregion
            //Siempre prefiere una propiedad a un metodo
            //por ejemplo aca para retorna los valores de la  ruta  HttpContext.Request.Path
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
    public async Task<ActionResult<QuejaResponseDTO>> GetById(int id)
    {
        var unitOfWork = new UnitOfWork(_context);
        var theQueja = unitOfWork.Quejas.GetPaged(id).FirstOrDefault();

        if (theQueja != null )
        {
            
            theQueja.Location.Latitude = theQueja.Latitude.Value;
            theQueja.Location.Longitude = theQueja.Longitude.Value;

            return theQueja;
        }
        else
        {
            return NotFound();
        }

    }


    [HttpPost]
    [Authorize]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<QuejaResponseDTO>> Post([FromForm] QuejaPostDTO model)
    {
        IFormFile? file = model.media;
        var uploadResult = new ImageUploadResult();

        uploadResult = await _cloudinaryService.AddPhotoAsync(file);
        if (uploadResult.Error is not null)
        {
            return Problem($"Error al crear la queja : No se pudo guardar la imagen: {uploadResult.Error}");
        }
        //model no usa distrito pero bd si
        var district = _context.District.First();

        var unitOfWork = new UnitOfWork(_context);
        Queja theQueja = new Queja()
        {
            Category_ID = model.Category_ID,
            District_ID = district.District_ID,
            Text = model.Text,
            Title = model.Title,
            PhotoAdress = "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath,
            User_ID = unitOfWork
                .AppUsers
                .GetByEmail(User.FindFirstValue(ClaimTypes.Email))
                .Result!.First().User_ID,
            CreatedAt = DateTime.Now,
            IsAnonymous = model.IsAnonymous,
            Location = model.Location == null ? null : _geometryFactory
                .CreatePoint(new Coordinate(model.Location.Longitude, model.Location.Latitude))
        };

        try
        {
            //if category exists
            var category = _unitOfWork.Categories.Get(model.Category_ID);
            if (category != null)
            {
                unitOfWork.Quejas.Add(theQueja);
                int changes = await unitOfWork.Complete();
                return Created($"api/quejas/{theQueja.Complaint_ID}", null);
            }
            else
            {
                ModelState.AddModelError( nameof(model.Category_ID),"Category not found");
                return BadRequest(ModelState);
                    
            }
        }
        catch (DbUpdateException e)
        {
            //for debug return Database error 
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            var message = new string[] { e.Message }.AsEnumerable();
            while (e.InnerException != null)
            {
                e = e.InnerException;
                message = message.Append(e.Message + Environment.NewLine);
            }
            return Problem(String.Join(",", message));
        }
    }


    [HttpPost("queja2")]
    [Authorize]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<QuejaResponseDTO>> Post([FromForm] QuejaPostDTO2 model)
    {
        QuejaPostDTO queja = new QuejaPostDTO()
        {
            Category_ID = model.Category_ID,
            IsAnonymous = model.IsAnonymous,
            Location = model.Location,
            media   = model.media[0],
            Text = model.Text,
            Title   = model.Title
        };
        return await Post(queja);
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



    /// <summary>
    /// favorites a complaint, but if already favorited will change status to disabled
    /// </summary>
    /// <param name="id"></param>

    [HttpPut("{id}/MeGusta")]
    //[Authorize]
    public async Task<ActionResult<bool>> MeGusta(int id)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

        //for debug, delete
        if (userEmail == null)
        {
            userEmail = "test@domain.com";
        }

        var complaint = _unitOfWork.Quejas.Get(id);
        if (complaint == null)
        {
            return BadRequest();
        }
        //complaint.Favorites //navegation property missing

        var alreadyFavorited = _unitOfWork
            .Favorites.GetAll()
            .FirstOrDefault(x => x.FavoritedBy == userEmail && x.Complaint_ID == id);

        if (alreadyFavorited == null)
        {
            alreadyFavorited = new Favorite()
            {
                Complaint_ID = id,
                Favorited = DateTime.UtcNow,
                FavoritedBy = userEmail,
                Enabled = true
            };
        }
        else
        {
            alreadyFavorited.Enabled = !alreadyFavorited.Enabled;
        }

        _unitOfWork.Favorites.Update(alreadyFavorited);
        var rows = await _unitOfWork.Complete();
        return alreadyFavorited.Enabled;
    }
}
