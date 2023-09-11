using Microsoft.EntityFrameworkCore;
using quejapp.Models;
using System.Linq.Dynamic.Core;
using s10.Back.Data.IRepositories;
using s10.Back.DTO;

namespace s10.Back.Data.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(RedCoContext context) : base(context) { }

        public RedCoContext? RedCoContext
        {
            get { return Context as RedCoContext; }
        }

        public PagedList<CommentResponseDTO>? GetCommentsOfQueja(CommentByQuejaDTO parameters, int idQueja)
        {
            var query = RedCoContext!.Comment
                .Where(c => c.Complaint_ID == idQueja);

            //    if(query !=null)
            //   {
            if (!string.IsNullOrEmpty(parameters.FilterQuery))
                query = query.Where(c => c.Text.Contains(parameters.FilterQuery));

            var qComments = query
                .Include(c => c.User)
                .Select(c => new CommentResponseDTO()
                {
                    UserName = c.User.Name,
                    UserPhoto = c.User.ProfilePicAddress,
                    Comment_ID = c.Comment_ID,
                    Text = c.Text,
                    AddedAt = c.AddedAt,
                    Complaint_ID = c.Complaint_ID
                }).
                OrderBy($"{parameters.SortColumn} {parameters.SortOrder}");
            //TODO Esteban lo mismo aca, como ya enumeraste, te traes todos los comentarios y los paginas en el servidor
            //en vez de hacerlo en Sql Server

            return PagedList<CommentResponseDTO>.Create(qComments, parameters.PageIndex, parameters.PageSize);
            //    }

            //No retornas null, si no tiene comentarios retornas un arraglo vacio
            //else
            //{
            //    return null;
            //}
        }

    }
}
