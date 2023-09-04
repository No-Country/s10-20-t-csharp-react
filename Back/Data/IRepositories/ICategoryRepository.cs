using quejapp.Models;
using s10.Back.DTO;
using s10.BackDTO;

namespace s10.Back.Data.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public PagedList<CategoryDTO> GetPagedCategories(RequestDTO<CategoryDTO> parameters);

        public PagedList<CategoryDTO>? GetPaged(int idCategory);
    }
}
