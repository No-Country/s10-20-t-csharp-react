using System.Diagnostics.CodeAnalysis;

namespace s10.Back.DTO
{
    public class CommentResponseDTO
    {        
        public int Comment_ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Complaint_ID { get; set; }
        public string? UserName { get; set; }
        public string? UserPhoto { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
