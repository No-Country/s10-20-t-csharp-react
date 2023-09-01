namespace s10.Back.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        Task<int> Complete();
    }
}
