using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quejapp.Models;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Comment_ID { get; set; }
    public string Text { get; set; } = string.Empty;
    
    public string User_ID { get; set; }
    [ForeignKey(name: "User")]
    public string Id { get; set; }

    [ForeignKey(name: "Id")]
    public AppUser User { get; set; } = null!;
    [ForeignKey(name: "Complaint")]
    public int Complaint_ID { get; set; }
    
    [ForeignKey(name: "Complaint_ID")]
    public Queja Complaint { get; set; } = null!;
    public DateTime AddedAt { get; set; }
}
