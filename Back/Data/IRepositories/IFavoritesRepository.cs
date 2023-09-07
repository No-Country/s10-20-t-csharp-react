using quejapp.Models;
using s10.Back.Data.IRepositories;
using s10.Back.Data;
using s10.Back.DTO;
using s10.Back.Models;

namespace s10.Back.Data.IRepositories
{
    public interface IFavoritesRepository :IGenericRepository<Favorite>
    {
        void Update(Favorite favorite);
    }
}


