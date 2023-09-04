using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;

namespace s10.Back.Data.IRepositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        PagedList<CommentResponseDTO>? GetCommentsOfQueja(CommentByQuejaDTO parameters, int id);
    }
}
