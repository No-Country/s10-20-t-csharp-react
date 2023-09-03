using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quejapp.Models;

public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int User_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ProfilePicAddress { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    public ICollection<Queja> Complaints { get; } = new List<Queja>();
}
