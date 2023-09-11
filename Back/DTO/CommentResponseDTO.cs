using quejapp.Models;
using System.Diagnostics.CodeAnalysis;

namespace s10.Back.DTO
{
    public class CommentResponseDTO
    {        
        public int Comment_ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Complaint_ID { get; set; }
        //public int? User_ID { get; set; } // eliminable cuando haya login
        public string? UserName { get; set; }
        public string? UserPhoto { get; set; }
        public DateTime AddedAt { get; set; }

        //to refactor

        [Obsolete("comments must include User")]
        public static  List<CommentResponseDTO>? ToCommentResponseDTO (IEnumerable<Comment>?  comments)
        {
            if (comments == null) return null;
            return  comments.Select(c => new CommentResponseDTO
            {
                Comment_ID = c.Comment_ID,
                Text = c.Text,
                AddedAt = c.AddedAt,
                Complaint_ID = c.Complaint_ID,
                UserName = c.User.Name,
                UserPhoto = c.User.ProfilePicAddress
            }).ToList();
        }
    }
}
