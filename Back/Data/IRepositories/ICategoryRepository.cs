using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using System.Linq.Dynamic.Core;

namespace s10.Back.Data.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public PagedList<CategoryDTO> GetPagedCategories(RequestDTO<CategoryDTO> parameters);

        public PagedList<CategoryDTO>? GetPaged(int idCategory);
    }
}
