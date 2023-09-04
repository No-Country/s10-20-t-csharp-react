
namespace s10.Back.DTO;

public class CommentHarcodedDTO
{
    public int Comment_ID { get; set; }
    public string Text { get; set; } = string.Empty;
    public int? User_ID { get; set; }
    public int Complaint_ID { get; set; }
    public DateTime AddedAt { get; set; }
}
