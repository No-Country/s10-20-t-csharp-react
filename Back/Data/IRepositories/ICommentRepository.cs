using quejapp.Models;
using s10.Back.DTO;

namespace s10.Back.Data.IRepositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        PagedList<CommentResponseDTO>? GetCommentsOfQueja(CommentByQuejaDTO parameters, int id);
    }
}
