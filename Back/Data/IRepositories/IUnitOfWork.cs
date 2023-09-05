namespace s10.Back.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IAppUserRepository AppUsers { get; }
        ICommentRepository Comments { get; }
        IQuejaRepository Quejas { get; }
        Task<int> Complete();
    }
}
