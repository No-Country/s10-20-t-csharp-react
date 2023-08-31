using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quejapp.Models;

public class Queja
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Complaint_ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? VideoAddress { get; set; }
    public string? PhotoAdress { get; set; }
    [ForeignKey(name: "District")]
    public int District_ID { get; set; }
    [ForeignKey(name: "Category")]
    public int Category_ID { get; set; }
    [ForeignKey(name: "User")]
    public int? User_ID { get; set; }
    public DateTime CreatedAt { get; set; }
    public District District { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public AppUser User { get; set; } = null!;
    public ICollection<Comment> Comments { get; } = new List<Comment>();

}
