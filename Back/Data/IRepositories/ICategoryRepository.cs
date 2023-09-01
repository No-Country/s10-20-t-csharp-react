using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using System.Linq.Dynamic.Core;

namespace s10.Back.Data.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
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
            if(category is not null)
            {
                return PagedList<CategoryDTO>.Create((new List<CategoryDTO>() { category }).AsQueryable(), 1, 1);
            }else
            {
                return null;
            }
        }
    }
}
