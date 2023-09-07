using Microsoft.EntityFrameworkCore;
using quejapp.Models;
using s10.Back.Data.IRepositories;
using s10.Back.DTO;

namespace s10.Back.Data.Repositories
{
    public class AppUserRespository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRespository(RedCoContext context) : base(context)
        {
            
        }

        public RedCoContext? RedCoContext
        {
            get { return Context as RedCoContext; }
        }

        public async Task<PagedList<AppUserDTO>?> GetByEmail(string email)
        {
            var u = await RedCoContext!.AppUser.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (u is null)
                return null;
            var user = new AppUserDTO()
            {
                Email = email,
                User_ID = u.Id,
                Name = u.Name
            };
            if (user is not null)
            {
                return PagedList<AppUserDTO>.Create((new List<AppUserDTO>() { user }).AsQueryable(), 1, 1);
            }
            else
            {
                return null;
            }
        }
    }
}
