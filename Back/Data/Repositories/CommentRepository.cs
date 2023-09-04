using Microsoft.EntityFrameworkCore;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using System.Linq.Dynamic.Core;
using s10.Back.Data.IRepositories;
using System.Xml.Linq;

namespace s10.Back.Data.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(RedCoContext context) : base(context){}

        public RedCoContext? RedCoContext
        {
            get { return Context as RedCoContext; }
        }

        public PagedList<CommentResponseDTO>? GetCommentsOfQueja(
            CommentByQuejaDTO parameters, 
            int idQueja)
        {
            var query = RedCoContext!.Comment
                .Where(c => c.Complaint_ID == idQueja);
            
            if(query.Count() > 0)
            {
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

                return PagedList<CommentResponseDTO>.Create(qComments, parameters.PageIndex, parameters.PageSize);
            }
            else
            {
                return null;
            }
        }
     
    }
}
