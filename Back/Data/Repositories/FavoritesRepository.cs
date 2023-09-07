using s10.Back.Data.IRepositories;
using s10.Back.Models;
using System.Linq.Expressions;

namespace s10.Back.Data.Repositories
{
    public class FavoritesRepository : GenericRepository<Favorite>, IFavoritesRepository
    {

        public FavoritesRepository(RedCoContext context):base(context) { }

        public void Update(Favorite favorite)
        {
            this.Context.Update(favorite);
        }
    }
}
