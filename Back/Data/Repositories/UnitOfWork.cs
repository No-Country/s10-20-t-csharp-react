using NetTopologySuite.Geometries;
using quejapp.Data;
using s10.Back.Data.IRepositories;
using s10.Back.Services;

namespace s10.Back.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RedCoContext _context;
        public UnitOfWork(RedCoContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            AppUsers = new AppUserRespository(_context);
            Comments = new CommentRepository(_context);
            Quejas = new QuejaRepository(_context);
        }
        public ICategoryRepository Categories { get; private set; }
        public IAppUserRepository AppUsers { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IQuejaRepository Quejas { get; private set; }

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
