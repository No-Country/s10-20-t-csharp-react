using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using Newtonsoft.Json;

namespace quejapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{   
    private readonly ILogger<CategoriesController> _logger;
    private readonly ApplicationDbContext _context;
    private List<Category> _categories;

    public CategoriesController(ILogger<CategoriesController> logger, ApplicationDbContext context)
    {        
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "Any-60")]
    public async Task<PagedListResponse<Category>> GetAll([FromQuery] RequestDTO<CategoryDTO> input) // la especialización en CategoryDTO se usa solo para saber que campos, siendo estos subcampos del modelo, están disponibles para buscar u ordenar
    {
        #region SQLServer
        //var query = _context.Category.AsQueryable();
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    query = query.Where(d => d.Name.Contains(input.FilterQuery));

        //int recordCount = await query.CountAsync();
        #endregion
        if(_categories is null)
        {
            _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        }
        List<Category> theseCategories = _categories;
        if (!string.IsNullOrEmpty(input.FilterQuery))
            theseCategories = theseCategories.Where(d => d.Name.Contains(input.FilterQuery)).ToList();
        theseCategories.Sort(delegate(Category x, Category y)
        {
            if (x.Name == null && y.Name == null) return 0;
            else if (x.Name == null) return -1;
            else if (y.Name == null) return 1;
            else
                if (input.SortOrder == "ASC")
                    return x.Name.CompareTo(y.Name);
                else
                    return y.Name.CompareTo(x.Name);
        });

        var paged = PagedList<Category>.Create(theseCategories.AsQueryable(), input.PageIndex, input.PageSize);
        return new PagedListResponse<Category>(
            paged, 
            (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText! /*funciona... pero a que costo?*/);
       
    }

    [HttpGet("{id}")]
    [ResponseCache(CacheProfileName = "Any-60")]
    public async Task<ActionResult<PagedListResponse<Category>>> GetById(int id)
    {
        #region SQLServer
        //var categoryResult = await _context.Category.Where(c => c.Category_ID == id).FirstOrDefaultAsync();// usar find con sql server
        #endregion
        if (_categories is null)
        {
            _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        }
        var theCategory = _categories.Where(c => c.Category_ID == id).FirstOrDefault();
        if (theCategory is not null)
        {
           // tengo que crear una lista de un elemento para poder tener,luego de transformarse, un queryable
            var paged = PagedList<Category>.Create((new List<Category> { theCategory }).AsQueryable(), 1, 1); // como es pk solo Debería ser como máximo 1 página de 1 elemento y como mínimo not found o 0
            return new PagedListResponse<Category>(
                paged,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText! /*funciona... pero a que costo?*/);
        }
        else
        {
            return NotFound();
        }
        
    }
}
