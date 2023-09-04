using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using s10.Back.DTO;

namespace s10.Back.Data.IRepositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<PagedList<AppUserDTO>?> GetByEmail(string email);
    }
}
