using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace quejapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuejasController : ControllerBase
{
    private readonly ILogger<QuejasController> _logger;
    private readonly ApplicationDbContext _context;
    private List<Comment> _comments;
    private List<Queja> _quejas;
    private List<Category> _categories;
    private List<AppUser> _users;
    private List<District> _districts;

    public QuejasController(ILogger<QuejasController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet] //mover a quejacontrollers
    [Route("/api/Quejas/{id}/comments")]
    [ResponseCache(CacheProfileName = "Any-60")]
    public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> GetAllByQueja([FromQuery] CommentByQuejaDTO input, int id)
    {
        if (_comments is null)
        {
            _comments = JsonConvert.DeserializeObject<List<Comment>>(System.IO.File.ReadAllText(@"./Back/Data/comments.json"))!;
        }
        if (_categories is null)
        {
            _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        }

        var thecomments = _comments.Where(c => c.Complaint_ID == id);
        if (!string.IsNullOrEmpty(input.FilterQuery))
            thecomments = thecomments.Where(d => d.Text.Contains(input.FilterQuery));
        #region SQLServer
        //var query = _context.Comment.Where(c => c.Complaint_ID == id);       
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    query = query.Where(d => d.Text.Contains(input.FilterQuery));

        //query = query.OrderBy($"{input.SortColumn} {input.SortOrder}");
        #endregion
        int recordCount = thecomments.Count();

        if (recordCount > 0)
        {
            var data = thecomments.Select(c => new CommentResponseDTO
            {

                Comment_ID = c.Comment_ID,
                Text = c.Text,
                User_ID = c.User_ID, //alguien podría queren en vez del id su nombre de usuario
                Complaint_ID = c.Complaint_ID,
                UserName = GetUserName(c.User_ID),
                AddedAt = c.AddedAt
            });
            //var commentResponse = JsonConvert.DeserializeObject<List<CommentResponseDTO>>(JsonConvert.SerializeObject(thecomments));
            //var commentResponse = JsonConvert.DeserializeObject<List<CommentResponseDTO>>(JsonConvert.SerializeObject(data));
            var paged = PagedList<CommentResponseDTO>.Create(data.AsQueryable(), input.PageIndex, input.PageSize);
            return new PagedListResponse<CommentResponseDTO>(
                paged,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!/*funciona... pero a que costo?*/);
        }
        else
        {
            return NotFound();
        }
    }

    private string? GetUserName(int? user_ID)
    {
        if (_users is null)
        {
            _users = JsonConvert.DeserializeObject<List<AppUser>>(System.IO.File.ReadAllText(@"./Back/Data/users.json"))!;
        }     
        return _users.Where(u => u.User_ID == user_ID).FirstOrDefault() is not null 
            ? _users.Where(u => u.User_ID == user_ID).FirstOrDefault()!.Name 
            : null;
        
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "Any-60")]
    public async Task<ActionResult<PagedListResponse<QuejaResponseDTO>>> GetAll([FromQuery] QuejaRequestDTO input)
    {
        if (_quejas is null)
        {
            _quejas = JsonConvert.DeserializeObject<List<Queja>>(System.IO.File.ReadAllText(@"./Back/Data/complaints.json"))!;
        }

        //IEnumerable<int> category_ID_list = null!;
        //try
        //{
        //    if (input.Category_IDs is not null)
        //    {
        //        category_ID_list = input.Category_IDs.Split(",").ToList().Select(int.Parse);
        //        var validCIds = await _context.Category.Select(c => c.Category_ID).ToListAsync();
        //        category_ID_list.All(c => validCIds.Contains(c));
        //        query = query.Where(q => q.Category_ID == input.Category_IDs);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    return UnprocessableEntity(ex);
        //}

        #region SQLServer
        //var query = _context.Queja.AsQueryable();
        //var count = await query.CountAsync();
        //if (input.Category_ID is not null)
        //    query = query.Where(q => q.Category_ID == input.Category_ID);
        //if (input.District_ID is not null)
        //    query = query.Where(q => q.District_ID == input.District_ID);
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    query = query.Where(q => q.Title.Contains(input.FilterQuery));

        //var recordCount = await query.CountAsync();
        //query = query.OrderBy($"{input.SortColumn} {input.SortOrder}");
        #endregion

        var theseQuejas = _quejas.AsQueryable();
        if (input.Category_ID is not null)
            theseQuejas = theseQuejas.Where(q => q.Category_ID == input.Category_ID);
        if (input.District_ID is not null)
            theseQuejas = theseQuejas.Where(q => q.District_ID == input.District_ID);
        if (!string.IsNullOrEmpty(input.FilterQuery))
            theseQuejas = theseQuejas.Where(q => q.Title.Contains(input.FilterQuery));

        var data = theseQuejas.Select(q => new QuejaResponseDTO
        {
            Text = q.Text,
            Title = q.Title,
            PhotoAdress = q.PhotoAdress,
            VideoAddress = q.VideoAddress,
            District_Name = GetDistrictName(q.District_ID),
            UserName = GetUserName(q.User_ID),
            Category_Name = GetCategoryName(q.Category_ID)
        });
        var recordCount = _quejas.Count();
        if (recordCount > 0)
        {
            try
            {
                //var quejaResponse = JsonConvert.DeserializeObject<List<QuejaResponseDTO>>(JsonConvert.SerializeObject(_quejas));
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

    private string? GetCategoryName(int category_ID)
    {
        if (_categories is null)
        {
            _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        }
        return _categories.Where(c => c.Category_ID == category_ID).FirstOrDefault() is not null
            ? _categories.Where(c => c.Category_ID == category_ID).FirstOrDefault()!.Name
            : null;
    }

    private string? GetDistrictName(int district_ID)
    {
        if (_districts is null)
        {
            _districts = JsonConvert.DeserializeObject<List<District>>(System.IO.File.ReadAllText(@"./Back/Data/districts.json"))!;
        }
        return _districts.Where(d => d.District_ID == district_ID).FirstOrDefault() is not null
            ? _districts.Where(d => d.District_ID == district_ID).FirstOrDefault()!.Name
            : null;
    }

    [HttpPost]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<QuejaDTO>>> Post([FromQuery] QuejaDTO model)
    {
        if (ModelState.IsValid)
        {
            if (_quejas is null)
            {
                _quejas = JsonConvert.DeserializeObject<List<Queja>>(System.IO.File.ReadAllText(@"./Back/Data/complaints.json"))!;
            }
            //IFormFile? imageFile = Request.Form.Files[0] ?? null;
            //IFormFile? videoFile = Request.Form.Files[1] ?? null;

            // TODO : procesar los archivos y para poder guardar su dirección
            Queja queja = new()
            {
                Category_ID = model.Category_ID,
                Text = model.Text,
                Title = model.Title,
                User_ID = model.User_ID,
                // TODO
                //VideoAddress = model.VideoAddress,
                //PhotoAdress = model.PhotoAdress,
                District_ID = model.District_ID,
                CreatedAt = DateTime.Now
            };
            #region SQLServer
            //_context.Queja.Add(queja);
            //int changes = await _context.SaveChangesAsync();
            #endregion

            _quejas.Add(queja);
            int changes = 1;
            if (changes > 0)
            {
                var laqueja = JsonConvert.DeserializeObject<QuejaDTO>(JsonConvert.SerializeObject(queja));
                var paged = PagedList<QuejaDTO>.Create((new List<QuejaDTO> { laqueja! }).AsQueryable(), 1, 1);
                return new PagedListResponse<QuejaDTO>(paged, (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
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
