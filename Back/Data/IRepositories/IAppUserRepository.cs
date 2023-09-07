using quejapp.Models;
using s10.Back.DTO;

namespace s10.Back.Data.IRepositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        //TODO Esteban. acá no tengo que devolver un pagedlist
        Task<PagedList<AppUserDTO>?> GetByEmail(string email);
    }
}
