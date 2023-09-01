using AutoMapper;
using quejapp.Data;
using quejapp.Models;
using s10.Back.Data.IRepositories;

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
    }
}
