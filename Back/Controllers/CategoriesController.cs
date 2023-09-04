using Microsoft.AspNetCore.Mvc;
using s10.Back.Data;
using s10.Back.Data.Repositories;
using s10.Back.DTO;

namespace s10.Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{   
    private readonly ILogger<CategoriesController> _logger;
    private readonly RedCoContext _context;

    public CategoriesController(ILogger<CategoriesController> logger, RedCoContext context)
    {        
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<CategoryDTO>>> GetAll(
        [FromQuery] RequestDTO<CategoryDTO> input) // la especialización en CategoryDTO se usa solo para saber que campos, siendo estos subcampos del modelo, están disponibles para buscar u ordenar
    {
        #region SQLServer
        //var query = _context.Category.AsQueryable();
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    query = query.Where(d => d.Name.Contains(input.FilterQuery));

        //query = query.OrderBy($"{input.SortColumn} {input.SortOrder}");
        //int recordCount = await query.CountAsync();
        #endregion

        #region WithFiles
        //if (_categories is null)
        //{
        //    _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        //}
        //List<Category> theseCategories = _categories;
        //if (!string.IsNullOrEmpty(input.FilterQuery))
        //    theseCategories = theseCategories.Where(d => d.Name.Contains(input.FilterQuery)).ToList();
        //theseCategories.Sort(delegate(Category x, Category y)
        //{
        //    if (x.Name == null && y.Name == null) return 0;
        //    else if (x.Name == null) return -1;
        //    else if (y.Name == null) return 1;
        //    else
        //        if (input.SortOrder == "ASC")
        //            return x.Name.CompareTo(y.Name);
        //        else
        //            return y.Name.CompareTo(x.Name);
        //});
        //var paged = PagedList<Category>.Create(theseCategories.AsQueryable(), input.PageIndex, input.PageSize);
        #endregion

        if (ModelState.IsValid)
        {
            #region WithUnitOfWorkPattern
            var unitOfWork = new UnitOfWork(_context);
            var categoriesData = unitOfWork.Categories.GetPagedCategories(input); //debería se async aquí??
            await unitOfWork.Complete();
            #endregion

            return new PagedListResponse<CategoryDTO>(
                categoriesData,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText! /*funciona... pero a que costo?*/);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<CategoryDTO>>> GetById(int id)
    {
        #region WithFiles
        //if (_categories is null)
        //{
        //    _categories = JsonConvert.DeserializeObject<List<Category>>(System.IO.File.ReadAllText(@"./Back/Data/categories.json"))!;
        //}
        //var theCategory = _categories.Where(c => c.Category_ID == id).FirstOrDefault();

        //if (theCategory is not null)
        #endregion

        #region SQLServer
        //var categoryResult = await _context.Category.FindAsync(id);// usar find con sql server
        #endregion
        #region WithUnitOfWorkPattern
        var unitOfWork = new UnitOfWork(_context);
        var categoryResult = unitOfWork.Categories.GetPaged(id);
        await unitOfWork.Complete();
        #endregion
        if (categoryResult is not null)
        {                    
            return new PagedListResponse<CategoryDTO>(
                categoryResult,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText! /*funciona... pero a que costo?*/);
        }
        else
        {
            return NotFound();
        }

    }
}
