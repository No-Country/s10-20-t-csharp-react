using AutoMapper;
using quejapp.Models;
using System.Linq.Dynamic.Core;
using s10.Back.Data.IRepositories;
using s10.Back.DTO;
using s10.BackDTO;

namespace s10.Back.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        //private readonly IMapper _mapper;
        public CategoryRepository(RedCoContext context/*, IMapper mapper*/) : base(context)
        {
            //_mapper = mapper;
        }

        public RedCoContext? RedCoContext
        {
            get { return Context as RedCoContext; }
        }
        public PagedList<CategoryDTO> GetPagedCategories(RequestDTO<CategoryDTO> parameters)
        {
            var qCategory = GetAll()
                .Select(c => new CategoryDTO
                {
                    Category_ID = c.Category_ID,
                    Name = c.Name
                })
                .Where(c => parameters.FilterQuery != null
                    ? c.Name.Contains(parameters.FilterQuery)
                    : c.Name.Contains(""))
                .OrderBy($"{parameters.SortColumn} {parameters.SortOrder}");

            return PagedList<CategoryDTO>.Create(qCategory, parameters.PageIndex,
                parameters.PageSize);
        }

        public PagedList<CategoryDTO>? GetPaged(int idCategory)
        {
            var c = Get(idCategory);
            if (c is null)
                return null;
            var category = new CategoryDTO()
            {
                Category_ID = c.Category_ID,
                Name = c.Name
            };
            if (category is not null)
            {
                return PagedList<CategoryDTO>.Create((new List<CategoryDTO>() { category }).AsQueryable(), 1, 1);
            }
            else
            {
                return null;
            }
        }
    }
}
