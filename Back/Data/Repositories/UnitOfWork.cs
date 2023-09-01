using quejapp.Data;
using s10.Back.Data.IRepositories;

namespace s10.Back.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RedCoContext _context;
        public UnitOfWork(RedCoContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
        }
        public ICategoryRepository Categories { get; private set; }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
